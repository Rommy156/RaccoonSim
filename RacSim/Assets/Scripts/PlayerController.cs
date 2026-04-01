//Allen Adepoju
//000948096using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float turnSpeed = 360f;
    public float smoothMoveTime = 0.1f;

    private Rigidbody rb;
    private Animator anim;

    public Transform orientation; 

    float smoothInputMagnitude;
    float smoothMoveVelocity;
    float angle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //  Camera-based direction
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 inputDir = forward * v + right * h;
        float inputMagnitude = Mathf.Clamp01(inputDir.magnitude);

        //  Smooth acceleration
        smoothInputMagnitude = Mathf.SmoothDamp(
            smoothInputMagnitude,
            inputMagnitude,
            ref smoothMoveVelocity,
            smoothMoveTime
        );

        if (inputMagnitude > 0.01f)
        {
            inputDir.Normalize();

            //  Get angle FROM CAMERA DIRECTION
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;

            // Smooth turning (force-like)
            angle = Mathf.MoveTowardsAngle(
                angle,
                targetAngle,
                turnSpeed * Time.fixedDeltaTime
            );

            rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
        }

        // Move forward in facing direction
        Vector3 moveDir = transform.forward * smoothInputMagnitude * moveSpeed;
        rb.MovePosition(rb.position + moveDir * Time.fixedDeltaTime);

        // Animation
        if (anim != null)
            anim.SetBool("isMoving", smoothInputMagnitude > 0.01f);
    }
}
