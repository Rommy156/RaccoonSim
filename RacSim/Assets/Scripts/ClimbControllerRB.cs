using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ClimbControllerRB : MonoBehaviour
{
    public LayerMask climbableMask;
    public LayerMask groundLayer;

    public float detectDistance = 2.2f;
    public float wallSnapDistance = 0.25f;
    public float climbSpeed = 3.5f;
    public float strafeSpeed = 2f;
    public float rotateSpeed = 8f;

    public float topCheckHeight = 1.4f;
    public float topForwardCheck = 0.5f;
    public float topDownCheck = 1.5f;
    public float topDetachForward = 0.2f;
    public float topDetachUp = 0.05f;

    public float dropAwayForce = 4f;
    public float reattachCooldown = 0.6f;

    public KeyCode climbToggleKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public Transform detectOrigin;
    public Transform body;
    public float bodyTiltDegrees = 12f;
    public float bodyTiltSpeed = 10f;

    public bool IsClimbing { get; private set; }

    private Rigidbody rb;
    private Vector3 wallNormal;

    private bool savedUseGravity;
    private RigidbodyConstraints savedConstraints;
    private Quaternion bodyStartLocalRotation;

    private float reattachTimer;

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
        if (reattachTimer > 0f)
            reattachTimer -= Time.deltaTime;

        bool hasWall = DetectWall(out RaycastHit hit);

        if (IsClimbing && (Input.GetKeyDown(dropKey) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            DropFromWall();
            return;
        }

        if (Input.GetKeyDown(climbToggleKey) && reattachTimer <= 0f)
        {
            if (!IsClimbing && hasWall)
                StartClimb(hit);
            else if (IsClimbing)
                StopClimb(false, true);
        }

        if (!IsClimbing)
        {
            TiltBody(false, false);
            return;
        }

        if (CanClimbOntoTop(out RaycastHit topHit))
        {
            DetachOntoTop(topHit);
            return;
        }

        if (!hasWall)
        {
            if (Input.GetAxisRaw("Vertical") < 0f)
            {
                rb.velocity = Vector3.down * climbSpeed;
                return;
            }

            StopClimb(true, true);
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

        Vector3 upMove = Vector3.up * (v * climbSpeed);
        Vector3 wallRight = Vector3.Cross(Vector3.up, wallNormal).normalized;
        Vector3 sideMove = wallRight * (h * strafeSpeed);

        rb.velocity = upMove + sideMove;

        if (v >= 0f)
            SnapToWall();
    }

    bool DetectWall(out RaycastHit hit)
    {
        Vector3 origin = (detectOrigin ? detectOrigin.position : transform.position) + Vector3.up * 1.0f;

        Vector3 dir = Camera.main ? Camera.main.transform.forward : transform.forward;
        dir.y = 0f;
        dir.Normalize();

        Debug.DrawRay(origin, dir * detectDistance, Color.red);

        return Physics.SphereCast(origin, 0.25f, dir, out hit, detectDistance, climbableMask, QueryTriggerInteraction.Ignore);
    }

    void StartClimb(RaycastHit hit)
    {
        IsClimbing = true;
        wallNormal = hit.normal;

        savedUseGravity = rb.useGravity;
        savedConstraints = rb.constraints;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        SnapToWall(hit);
    }

    void StopClimb(bool nudgeDown, bool restoreRotation)
    {
        IsClimbing = false;

        rb.useGravity = savedUseGravity;
        rb.constraints = savedConstraints;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (restoreRotation)
        {
            Vector3 euler = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
        }

        if (nudgeDown)
            rb.position += Vector3.down * 0.2f;

        TiltBody(false, true);
    }

    void DropFromWall()
    {
        Vector3 awayFromWall = wallNormal;
        awayFromWall.y = 0f;

        if (awayFromWall.sqrMagnitude < 0.001f)
            awayFromWall = -transform.forward;

        awayFromWall.Normalize();

        IsClimbing = false;

        rb.useGravity = savedUseGravity;
        rb.constraints = savedConstraints;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        TiltBody(false, true);

        rb.position += awayFromWall * 0.3f + Vector3.down * 0.15f;
        reattachTimer = reattachCooldown;

        rb.AddForce(awayFromWall * dropAwayForce, ForceMode.VelocityChange);
    }

    void SnapToWall()
    {
        Vector3 origin = (detectOrigin ? detectOrigin.position : transform.position) + Vector3.up * 1.0f;

        Vector3 dir = Camera.main ? Camera.main.transform.forward : transform.forward;
        dir.y = 0f;
        dir.Normalize();

        if (Physics.SphereCast(origin, 0.25f, dir, out RaycastHit hit, detectDistance, climbableMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 desiredPos = hit.point + hit.normal * wallSnapDistance;
            Vector3 offset = desiredPos - rb.position;
            Vector3 normalOnly = Vector3.Project(offset, hit.normal);

            rb.position = Vector3.Lerp(rb.position, rb.position + normalOnly, 0.35f);
            wallNormal = hit.normal;
        }
    }

    void SnapToWall(RaycastHit hit)
    {
        Vector3 desiredPos = hit.point + hit.normal * wallSnapDistance;
        Vector3 offset = desiredPos - rb.position;
        Vector3 normalOnly = Vector3.Project(offset, hit.normal);
        rb.position += normalOnly;
    }

    void RotateToWall()
    {
        Vector3 lookDir = -wallNormal;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
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

    bool CanClimbOntoTop(out RaycastHit topHit)
    {
        Vector3 chestPos = transform.position + Vector3.up * topCheckHeight;
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        if (Physics.Raycast(chestPos, forward, topForwardCheck, climbableMask, QueryTriggerInteraction.Ignore))
        {
            topHit = default;
            return false;
        }

        Vector3 topCheckOrigin = chestPos + forward * topForwardCheck;
        return Physics.Raycast(topCheckOrigin, Vector3.down, out topHit, topDownCheck, groundLayer, QueryTriggerInteraction.Ignore);
    }

    void DetachOntoTop(RaycastHit topHit)
    {
        Vector3 flatForward = transform.forward;
        flatForward.y = 0f;
        flatForward.Normalize();

        Vector3 targetPos = topHit.point + flatForward * topDetachForward + Vector3.up * topDetachUp;

        IsClimbing = false;

        rb.useGravity = savedUseGravity;
        rb.constraints = savedConstraints;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        TiltBody(false, true);

        rb.position = targetPos;
        reattachTimer = reattachCooldown;
    }
}