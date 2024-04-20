using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSController : MonoBehaviour

    /*
    * Author: Vanessa Cunha
    * Purpose: Movement Code for the FPS Game and Camera Controls
    * Class: GMAP 231
    * Assignment 6: NavMesh
    * Due Date: August 22nd, 2023
    */

{
    //movement Variables
    public float walkSpeed = 5f;
    public float jumpHeight = 5f;
    public float gravity = 9.81f;

    //camera Variables
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    //Public keycode for unlocking the mouse
    public KeyCode unlockMouse = KeyCode.Delete;

    //Private Variables
    private CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    private float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        //find the character controller on the gameObject, our movement script here will use functionality from the character controller component to move
        _characterController = GetComponent<CharacterController>();

        //locks the cursor to the center of the game window and hides it so it looks more like an fps
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //let players get their mouse back if they want
        if (Input.GetKeyDown(unlockMouse))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Local Vector Variables used to store 
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Local float Variables to calculate how fast we should move both forward and side to side based on player input
        float currentSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float currentSpeedY = walkSpeed * Input.GetAxis("Horizontal");

        //local float variable to store the current vertical direction of our player
        float jumpDirection = _moveDirection.y;

        //calculate movement vector based on our speed variables for moving forward and side to side 
        _moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        //adds vertical movement to our player if the player is on the ground and pressed the jump button
        if (Input.GetButton("Jump") && _characterController.isGrounded)
        {
            _moveDirection.y = jumpHeight;
        }

        //stops adding vertical movement while the player is not jumping
        else
        {
            _moveDirection.y = jumpDirection;
        }

        //if the player is not on the ground subtracts our gravity force from vertical movement. This will allows for the player jump to slowly reach a peak and then return to the ground over time
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }
        //resets vertical movement to 0 after landing
        else if (_characterController.isGrounded && _moveDirection.y < 0)
        {
            _moveDirection.y = 0;
        }
        //apply our final move direction to the player in game using the built in characterController move funciton
        _characterController.Move(_moveDirection * Time.deltaTime);

        //calculate where our camera should rotate based on mouse input
        rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
        //restrict how high or low the camera can rotate
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        //rotate the camera to match vertical mouse input, when using Quaternion.Euler the rotation is applied around the given axis not to it, this is why x and y appear to be flipped
        //hand pneumonic device
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        /*rotate our character to match horizontal mouse input, we use multiply here because of the nature of quaternions. You can't use addition to add one to the other. 
        to combine 2 quaternions like we want here (our original quaternion rotation + the rotation we want to turn to based on mouse input) we multiply the original by the second*/
        this.transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X") * lookSpeed, 0);

    }
}
