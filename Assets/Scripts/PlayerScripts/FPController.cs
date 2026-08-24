using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 5f;
    public float maxSpeed = 10f;
    public float acceleration = 2f;
    public float gravity = -9.81f;
    public float currentSpeed;
    public float antiBump = 4f; // Force applied downward when grounded, to handle stairs / keep player stuck to ground

    [Header("Velocity Smoothing")] // settings for horizontal velocity of the player when starting and stopping
    public float moveAcceleration = 20f; 
    public float moveDeceleration = 12f;
    private Vector3 horizontalVelocity; // the smoothened out horizontal velocity

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    public float lookSmoothTime = 0.05f; // The value for making the camera smoother, higher number is smoother but laggier
    private float verticalRotation = 0f;
    private Vector2 currentLookInput;
    private Vector2 lookInputVelocity;

    [Header("Head Bob")] // Adding a head bob to make camera movement more natural
    public bool enableHeadBob = true;
    public float bobFrequency = 1.8f; // how fast it cycles
    public float bobAmplitude = 0.05f; // how far the camera moves up n down
    public float bobSideAmplitude = 0.03f; // for a horizontal sway
    public float bobSmoothTime = 0.1f; // smoothing for starting and stopping so it isn't abrupt
    private float bobTimer;
    private Vector3 cameraNeutralLocalPos;
    private Vector3 bobVelocity;

    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickUpObject heldObject;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity; // the vertical velocity
    

    private Vector3 moveDirection; // Vector for forces 

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = baseSpeed;

        if (cameraTransform != null)
        {
            cameraNeutralLocalPos = cameraTransform.localPosition;
        }
    }

    private void Update()
    {
        HandleMovement();
        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }
    }

    private void LateUpdate()
    {
        HandleLook();
        HandleHeadBob();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    lookInput = context.ReadValue<Vector2>();
    }

    public void HandleMovement()
    {

        if (Keyboard.current.wKey.isPressed) // The player gets faster the longer they move forward.
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        Vector3 inputDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);
        Vector3 targetVelocity = inputDirection * currentSpeed;

        bool hasInput = moveInput.sqrMagnitude > 0.0001f;
        float rate = hasInput ? moveAcceleration : moveDeceleration;
        horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, targetVelocity, rate * Time.deltaTime); // easing the horizontal velocity towards target

        controller.Move(horizontalVelocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0) // Ensuring player sticks to ground
        {
            velocity.y = -antiBump; 
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // Applying gravity while airborne
        }

        controller.Move(velocity * Time.deltaTime);  

    }

    public void HandleLook()
    {

        currentLookInput = Vector2.SmoothDamp(currentLookInput, lookInput, ref lookInputVelocity, lookSmoothTime); // Smoothing the input from the player

        float mouseX = currentLookInput.x * lookSensitivity;
        float mouseY = currentLookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -
        verticalLookLimit, verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation,
        0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleHeadBob() // This is for the camera sway, though it doesn't seem to be working yet.
    {
        if (cameraTransform == null) return;

        Vector3 targetLocalPos = cameraNeutralLocalPos;

        bool isMoving = controller.isGrounded && new Vector3(horizontalVelocity.x, 0, horizontalVelocity.z).sqrMagnitude > 0.1f; // ?

        if (enableHeadBob && isMoving)
        {
            float speedFactor = horizontalVelocity.magnitude / Mathf.Max(baseSpeed, 0.01f); // this scales the bob speed with movement speed
            bobTimer += Time.deltaTime * bobFrequency * speedFactor;

            float bobY = Mathf.Sin(bobTimer * 2f) * bobAmplitude; // using a sin wave for the up and down movement of the camera
            float bobX = Mathf.Cos(bobTimer) * bobSideAmplitude; // using a cos wave for the horizontal movement of the camera

            targetLocalPos = cameraNeutralLocalPos + new Vector3(bobX, bobY, 0f);
        }

        else
        {
            bobTimer = 0f; // resetting the cycle
        }

        cameraTransform.localPosition = Vector3.SmoothDamp(cameraTransform.localPosition, targetLocalPos, ref bobVelocity, bobSmoothTime);
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position,
            cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, ~0, QueryTriggerInteraction.Ignore))
            {
                PickUpObject pickUp =
                hit.collider.GetComponentInParent<PickUpObject>();
                if (pickUp != null)
                {
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;
                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

}
    