//Allen Adepoju
//000948096
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float rotateSpeed = 10f;
    public float jumpHeight = 5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private ClimbControllerRB climb;
    private Transform camTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        climb = GetComponent<ClimbControllerRB>();
        camTransform = Camera.main.transform;
        rb.freezeRotation = true; 
    }

    private void FixedUpdate()
    {
        if (climb != null && climb.IsClimbing) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotateSpeed, 0.1f);
            rb.MoveRotation(Quaternion.Euler(0f, targetAngle, 0f));
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}