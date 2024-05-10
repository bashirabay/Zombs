using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    // Movement Variables
    public float walkSpeed = 5f;
    public float runSpeed = 10f; // Separate variable for run speed
    public float jumpHeight = 5f;
    public float gravity = 9.81f;
    public float interactionDistance = 5f;

    // Camera Variables
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    // Public keycode for unlocking the mouse
    public KeyCode unlockMouse = KeyCode.Delete;

    public Image staminaBar;
    public float stamina;
    public float maxStamina;
    public float runCost;
    public float staminaReplenishRate = 5f;
    public float sprintCooldown = 1f; // Cooldown time after stamina reaches zero

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isSprinting = false;
    private float sprintCooldownTimer = 0f;

    private bool isCursorLocked = true; // Track if the cursor is locked

    private bool isShootingEnabled = true; // Track if shooting is enabled

    // Start is called before the first frame update
    void Start()
    {
        // Find the character controller on the gameObject
        characterController = GetComponent<CharacterController>();

        // Lock the cursor to the center of the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // Let players get their mouse back if they want
        if (Input.GetKeyDown(unlockMouse))
        {
            ToggleCursorLock(); // Toggle cursor lock state
        }

        if (!PauseMenu.GamePaused)
        {
            HandleMovement();
            if (isCursorLocked)
            {
                HandleCameraRotation();
            }
        }
    }

    void HandleMovement()
    {
        // Sprint mechanic
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && sprintCooldownTimer <= 0f)
        {
            isSprinting = true;
            walkSpeed = runSpeed; // Set walk speed to run speed
            stamina -= runCost * Time.deltaTime;
            staminaBar.fillAmount = stamina / maxStamina;
        }
        else
        {
            isSprinting = false;
            walkSpeed = 5f; // Reset walk speed
        }

        // Replenish stamina gradually
        if (!isSprinting && stamina < maxStamina)
        {
            stamina += staminaReplenishRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            staminaBar.fillAmount = stamina / maxStamina;
        }

        // Check if stamina is empty and trigger cooldown
        if (stamina <= 0)
        {
            // Set walk speed immediately when stamina reaches zero
            walkSpeed = 5f;

            // Start the sprint cooldown timer
            sprintCooldownTimer = sprintCooldown;
        }

        // Update sprint cooldown timer
        if (sprintCooldownTimer > 0f)
        {
            sprintCooldownTimer -= Time.deltaTime;
        }

        // Handle movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float currentSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float currentSpeedY = walkSpeed * Input.GetAxis("Horizontal");
        float jumpDirection = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        // Handle jump
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = jumpDirection;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Apply movement
        characterController.Move(moveDirection * Time.deltaTime);

        // Handle shooting
        if (isShootingEnabled && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void HandleCameraRotation()
    {
        // Handle camera rotation
        rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotate the character
        transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X") * lookSpeed, 0);
    }

    public void ToggleCursorLock()
    {
        if (!PauseMenu.GamePaused)
        {
            // Toggle cursor lock state
            isCursorLocked = !isCursorLocked;

            // Lock or unlock cursor based on the state
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLocked;
        }
    }

    public void EnableShooting()
    {
        isShootingEnabled = true;
    }

    public void DisableShooting()
    {
        isShootingEnabled = false;
    }

    void Shoot()
    {
        // Shooting logic here
    }

    public void ResetGame()
    {
        // Reset game state to initial state
        stamina = maxStamina;
        isShootingEnabled = true;
    }
}
