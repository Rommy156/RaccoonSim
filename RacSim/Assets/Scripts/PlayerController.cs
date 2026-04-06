//Allen Adepoju
//000948096
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   //variable to hold movement variables
    public float moveSpeed = 7f;
    public float strafeSpeed = 4f;

    //create a vector3 variable to store movement calculations
    private Vector3 movement;
    //refrence to player rigid body
    private Rigidbody rb;

    //vector3 to store jump direction
    private Vector3 jumpDirection;
    //float variable to store jump height (y)
    public float jumpHeight = 4f;
    //bool variable to check if it's grounded
    public bool isGrounded;
    //reference to groundLayer mask
    public LayerMask groundLayer;

    //store animator component
    private Animator anim;

    //reference to climb controller
    private ClimbControllerRB climb;


    // Start is called before the first frame update
    void Start()
    {
        //initialize rigidBody component
        rb = GetComponent<Rigidbody>();

        //initialize animator
        anim = GetComponent<Animator>();

        //set initial jump direction
        jumpDirection = Vector3.up;

        //initialize climb controller
        climb = GetComponent<ClimbControllerRB>();
    }

    // Update is called once per frame
    void Update()
    {
        //if climbing, stop normal player update movement
        if (climb != null && climb.IsClimbing)
        {
            if (anim != null)
            {
                anim.SetBool("isMoving", false);
            }
            return;
        }

        //check the ground using a function
        isGrounded = CheckGround();
        //now that we know the answer to isGrounded, call the Jump() function.
        if (isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //add force to rigidbody in the direction of jumpDirection and multiply by height
            //ForceMode.Impulse means "the frame it is called" or immediately
            rb.AddForce(jumpDirection * jumpHeight, ForceMode.Impulse);

        }
    }

    //FixedUpdate is called per frame at a set interval
    private void FixedUpdate()
    {
        //if climbing, stop normal rigidbody movement
        if (climb != null && climb.IsClimbing)
            return;

        //create temporary floates to store Horizontal and Vertical input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement = (transform.forward * v * moveSpeed) + (transform.right * h * strafeSpeed);
        movement = Vector3.Normalize(movement);

        //move with the RigidBody
        //your current position + answer to the calculation above and muliplied with Time.DeltaTime
        rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);

        //check if player is moving
        if (movement.magnitude > 0.01f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    bool CheckGround()
    {
        //raycastHit is a variable that represents the actual collision
        RaycastHit hit;
        //checking if the raycast hits the ground layer
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, groundLayer))
        {
            return true;
        }
        return false;
    }

}