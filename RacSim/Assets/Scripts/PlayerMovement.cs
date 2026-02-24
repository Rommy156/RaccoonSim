using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 6f;
    public float gravity = -20f;

    [Header("References")]
    public Transform cameraTransform; // Reference to the Camera's transform

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure the player sticks to the ground
        }

        MovePlayer(); // Handle the movement
        Jump(); // Handle the jump
        ApplyGravity(); // Apply gravity over time
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal"); // Get input for horizontal movement (A/D, Left/Right)
        float z = Input.GetAxis("Vertical"); // Get input for vertical movement (W/S, Up/Down)

        Vector3 direction = new Vector3(x, 0f, z); // Movement direction

        if (direction.magnitude > 1f)
            direction.Normalize(); // Normalize if input magnitude is > 1

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y; // Calculate the direction relative to the camera

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // Rotate direction based on camera angle

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // Rotate player to face movement direction

        bool isRunning = Input.GetKey(KeyCode.LeftShift); // Check if Left Shift is held to run
        float speed = isRunning ? runSpeed : moveSpeed; // Set speed based on whether the player is running or not

        controller.Move(moveDir * speed * Time.deltaTime); // Move player
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) // Jump if on the ground
        {
            velocity.y = jumpForce;
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime; // Apply gravity over time
        controller.Move(velocity * Time.deltaTime); // Move the player based on gravity
    }
}