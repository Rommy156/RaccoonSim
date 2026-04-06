using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ClimbControllerRB : MonoBehaviour
{
    public LayerMask climbableMask;
    public LayerMask groundLayer;

    public float detectDistance = 2f;
    public float wallSnapDistance = 0.05f;
    public float climbSpeed = 3.5f;
    public float strafeSpeed = 2f;
    public float rotateSpeed = 8f;

    public KeyCode climbToggleKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public Transform detectOrigin;
    public Transform body;
    public float bodyTiltDegrees = 12f;
    public float bodyTiltSpeed = 10f;

    public bool IsClimbing { get; private set; }

    private Rigidbody rb;
    private Vector3 wallNormal;
    private Quaternion bodyStartLocalRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (detectOrigin == null)
            detectOrigin = transform;

        if (body == null)
            body = transform.Find("RS_Raccoon_V2");

        if (body != null)
            bodyStartLocalRotation = body.localRotation;
    }

    void Update()
    {
        bool hasWall = DetectWall(out RaycastHit hit);

        if (Input.GetKeyDown(climbToggleKey))
        {
            if (!IsClimbing && hasWall)
            {
                StartClimb(hit);
            }
        }

        if (IsClimbing && Input.GetKeyDown(dropKey))
        {
            StopClimb();
            rb.velocity = -transform.forward * 3f + Vector3.down * 4f;
            return;
        }

        if (!IsClimbing)
        {
            TiltBody(false, false);
            return;
        }

        if (!hasWall)
        {
            StopClimb();
            return;
        }

        wallNormal = hit.normal;
        RotateToWall();
        TiltBody(true, false);
    }

    void FixedUpdate()
    {
        if (!IsClimbing) return;

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 verticalMove = Vector3.up * (v * climbSpeed);
        Vector3 wallRight = Vector3.Cross(Vector3.up, wallNormal).normalized;
        Vector3 sideMove = wallRight * (h * strafeSpeed);

        rb.velocity = verticalMove + sideMove;

        HardSnapToWall();
    }

    bool DetectWall(out RaycastHit hit)
    {
        Vector3 origin = detectOrigin.position + Vector3.up * 1f;
        Vector3 dir = Camera.main ? Camera.main.transform.forward : transform.forward;

        dir.y = 0f;
        dir.Normalize();

        return Physics.Raycast(origin, dir, out hit, detectDistance, climbableMask);
    }

    void StartClimb(RaycastHit hit)
    {
        IsClimbing = true;
        wallNormal = hit.normal;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        HardSnapToWall(hit);
    }

    void StopClimb()
    {
        IsClimbing = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        TiltBody(false, true);
    }

    void HardSnapToWall()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectDistance, climbableMask))
        {
            Vector3 target = hit.point + hit.normal * wallSnapDistance;
            rb.position = target;
            wallNormal = hit.normal;
        }
    }

    void HardSnapToWall(RaycastHit hit)
    {
        Vector3 target = hit.point + hit.normal * wallSnapDistance;
        rb.position = target;
    }

    void RotateToWall()
    {
        Vector3 lookDir = -wallNormal;
        lookDir.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(lookDir);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotateSpeed * Time.deltaTime));
    }

    void TiltBody(bool tiltOn, bool instant)
    {
        if (body == null) return;

        Quaternion target = tiltOn
            ? Quaternion.Euler(bodyTiltDegrees, 0f, 0f) * bodyStartLocalRotation
            : bodyStartLocalRotation;

        if (instant)
            body.localRotation = target;
        else
            body.localRotation = Quaternion.Slerp(body.localRotation, target, bodyTiltSpeed * Time.deltaTime);
    }
}