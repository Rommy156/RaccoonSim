using UnityEngine;


//not sure check again
//

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //movement settings
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 6f;
    public float gravity = -9.81f;

    public Transform cameraTransform;
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();
        HandleJump();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0f, z);

        if (inputDirection.magnitude >= 0.1f)
        {
            //Normalize input
            inputDirection.Normalize();

            //calculate direction relative to camera
            float targetAngle = 
                Mathf.Atan2(inputDirection.x, inputDirection.z)*Mathf.Rad2Deg
                +cameraTransform.eulerAngles.y;
            //smooth rotation 
            float smoothAngle = Mathf.LerpAngle(
                transform.eulerAngles.y,
                targetAngle,
                10f * Time.deltaTime
                );
            transform.rotation = Quaternion.Euler(0f,smoothAngle,0f);
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float speed = isRunning ? runSpeed : moveSpeed;

            controller.Move(transform.forward * speed * Time.deltaTime);
        }
    }

    void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = jumpForce;

        }
    }

}