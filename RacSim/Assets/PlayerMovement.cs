using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 6f;
    public float gravity = -20f;

    [Header("References")]
    public Transform cameraTransform;

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
            velocity.y = -2f;
        }

        MovePlayer();
        Jump();
        ApplyGravity();
    }
    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");

        float z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0f, z);

        if (direction.magnitude > 1f)
            direction.Normalize();

        float targetAngle =
            Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
            + cameraTransform.eulerAngles.y;

        Vector3 moveDir =
            Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : moveSpeed;

        controller.Move(moveDir * speed * Time.deltaTime);

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpForce;
        }
    }
    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
