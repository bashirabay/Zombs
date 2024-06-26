using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    // Movement Variables
    public float walkSpeed = 5f;
    public float jumpHeight = 5f;
    public float gravity = 9.81f;

    // Camera Variables
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    // Public keycode for unlocking the mouse
    public KeyCode unlockMouse = KeyCode.Delete;

    // Stamina Variables
    public Image StaminaBar;
    public float Stamina, MaxStamina;
    public float RunCost;
    public float StaminaReplenishRate = 5f;

    // Outline component for glow effect
    private Outline staminaBarOutline;

    private bool isShiftKeyDown = false;
    private bool isSprintPowerUpActive = false;

    // Private Variables
    private CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float speed;
    private float runSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Find the character controller on the gameObject
        _characterController = GetComponent<CharacterController>();

        // Lock the cursor to the center of the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        runSpeed = walkSpeed * 2f;
        speed = walkSpeed;

        // Get the Outline component from StaminaBar
        staminaBarOutline = StaminaBar.GetComponent<Outline>();
        if (staminaBarOutline == null)
        {
            staminaBarOutline = StaminaBar.gameObject.AddComponent<Outline>();
        }

        // Initially disable the glow effect
        staminaBarOutline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Let players get their mouse back if they want
        if (Input.GetKeyDown(unlockMouse))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShiftKeyDown = true;
            walkSpeed = runSpeed;

            if (!isSprintPowerUpActive)
            {
                Stamina -= RunCost * Time.deltaTime;
                if (Stamina < 0) Stamina = 0;
                StaminaBar.fillAmount = Stamina / MaxStamina;

                // Enable the glow effect
                staminaBarOutline.enabled = true;
                staminaBarOutline.effectColor = Color.yellow; // Set the glow color
                staminaBarOutline.effectDistance = new Vector2(20, 20); // Increase the glow distance
            }
        }
        else
        {
            // If the shift key is released, reset the flag
            isShiftKeyDown = false;

            // Disable the glow effect
            staminaBarOutline.enabled = false;
        }

        // If the shift key is not being held down, but stamina is not full, gradually replenish it
        if (!isShiftKeyDown && Stamina < MaxStamina)
        {
            Stamina += StaminaReplenishRate * Time.deltaTime;
            Stamina = Mathf.Clamp(Stamina, 0, MaxStamina);
            StaminaBar.fillAmount = Stamina / MaxStamina;
        }

        // Update walk speed based on whether shift key is held down
        if (!isShiftKeyDown)
        {
            walkSpeed = speed;
        }
        if (Stamina <= 0)
        {
            walkSpeed = speed;
        }

        // Local Vector Variables used to store movement directions
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Local float Variables to calculate movement speed
        float currentSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float currentSpeedY = walkSpeed * Input.GetAxis("Horizontal");

        // Local float variable to store the current vertical direction
        float jumpDirection = _moveDirection.y;

        // Calculate movement vector
        _moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        // Add vertical movement if jumping
        if (Input.GetButton("Jump") && _characterController.isGrounded)
        {
            _moveDirection.y = jumpHeight;
        }
        else
        {
            _moveDirection.y = jumpDirection;
        }

        // Apply gravity if not grounded
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }
        else if (_characterController.isGrounded && _moveDirection.y < 0)
        {
            _moveDirection.y = 0;
        }

        // Apply movement to the player
        _characterController.Move(_moveDirection * Time.deltaTime);

        // Calculate camera rotation based on mouse input
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;  // Inverted here
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        // Rotate the camera
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        this.transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X") * lookSpeed, 0);
    }

    public void ActivateSprintPowerUp(float duration)
    {
        if (!isSprintPowerUpActive)
        {
            StartCoroutine(SprintPowerUpCoroutine(duration));
        }
    }

    private IEnumerator SprintPowerUpCoroutine(float duration)
    {
        isSprintPowerUpActive = true;

        // Temporarily allow sprinting without stamina cost
        float originalRunCost = RunCost;
        RunCost = 0;

        yield return new WaitForSeconds(duration);

        // Revert to normal behavior
        RunCost = originalRunCost;
        isSprintPowerUpActive = false;
    }
}
