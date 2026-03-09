//Allen Adepoju
//000948096
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float strafeSpeed = 4f;

    private Vector3 movement;
    private Rigidbody rb;

    private Vector3 jumpDirection;
    public float jumpHeight = 4f;

    public bool isGrounded;
    public LayerMask groundLayer;

    private ClimbControllerRB climb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpDirection = Vector3.up;
        climb = GetComponent<ClimbControllerRB>();
    }

    void Update()
    {
        if (climb != null && climb.IsClimbing) return;

        isGrounded = CheckGround();

        if (isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpDirection * jumpHeight, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (climb != null && climb.IsClimbing) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forwardMove = transform.forward * v * moveSpeed;
        Vector3 strafeMove = transform.right * h * strafeSpeed;

        movement = forwardMove + strafeMove;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    bool CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, groundLayer))
        {
            return true;
        }
        return false;
    }
}