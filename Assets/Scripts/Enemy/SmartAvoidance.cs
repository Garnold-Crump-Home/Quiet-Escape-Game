using UnityEngine;

public class SmartAvoidance : MonoBehaviour
{
    public bool playScript = true;

    [Header("Target")]
    public Transform player;

    [Header("Detection")]
    public float detectionRange = 12f;
    public float loseRange = 16f;
    public bool requireLineOfSight = true;
    public LayerMask obstacleMask;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float turnSpeed = 6f;

    [Header("Wandering")]
    public float wanderSpeed = 2f;
    public float wanderTurnSpeed = 3f;
    public float wanderDirectionChangeInterval = 3f;

    [Header("Avoidance")]
    public float rayDistance = 2f;
    public float sideOffset = 0.6f;
    public float avoidStrength = 2f;

    private Rigidbody rb;
    private bool isChasing = false;
    private Vector3 wanderDirection;
    private float wanderTimer = 0f;

    [Header("Animations")]
    public Animator rightLeg;
    public Animator leftLeg;
    public Animator leftArm;
    public Animator rightArm;

    public WardrobeCollider wardrobeCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        PickNewWanderDirection();
    }

    void FixedUpdate()
    {
        if (!playScript || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // --- Detection ---
        if (!isChasing && distance <= detectionRange)
        {
            if (!requireLineOfSight || HasLineOfSight())
                isChasing = true;
        }

        if (isChasing && distance > loseRange)
            isChasing = false;

        // --- Behavior ---
        if (isChasing)
            ChasePlayer();
        else
            Wander();

        // Keep upright
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    bool HasLineOfSight()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, detectionRange))
            return hit.transform == player;

        return false;
    }

    // -------------------------
    //      AVOIDANCE LOGIC
    // -------------------------
    Vector3 GetAvoidanceVector()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        bool front = Physics.Raycast(transform.position + Vector3.up, forward, out RaycastHit hitF, rayDistance, obstacleMask);
        bool left = Physics.Raycast(transform.position - right * sideOffset + Vector3.up, forward, rayDistance, obstacleMask);
        bool rightSide = Physics.Raycast(transform.position + right * sideOffset + Vector3.up, forward, rayDistance, obstacleMask);

        Vector3 avoid = Vector3.zero;

        if (front)
            avoid += hitF.normal * avoidStrength;

        if (left)
            avoid += transform.right * avoidStrength;

        if (rightSide)
            avoid -= transform.right * avoidStrength;

        return avoid;
    }

    void ChasePlayer()
    {
        if (wardrobeCollider.isInWardrobe)
        {
            isChasing = false;
            return;
        }

        SetChasingAnimations(true);

        Vector3 direction = (player.position - transform.position).normalized;

        // Add avoidance
        Vector3 avoid = GetAvoidanceVector();
        if (avoid != Vector3.zero)
            direction = (direction + avoid).normalized;

        // Rotate
        Quaternion targetRot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRot, Time.deltaTime * turnSpeed));

        // Move
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    void Wander()
    {
        SetChasingAnimations(false);

        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f)
            PickNewWanderDirection();

        Vector3 avoid = GetAvoidanceVector();
        Vector3 direction = wanderDirection;

        if (avoid != Vector3.zero)
            direction = (wanderDirection + avoid).normalized;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRot, Time.deltaTime * wanderTurnSpeed));

        rb.MovePosition(rb.position + transform.forward * wanderSpeed * Time.deltaTime);
    }

    void PickNewWanderDirection()
    {
        wanderTimer = wanderDirectionChangeInterval;
        Vector2 random = Random.insideUnitCircle.normalized;
        wanderDirection = new Vector3(random.x, 0f, random.y);
    }

    void SetChasingAnimations(bool chasing)
    {
        rightLeg.SetBool("Chasing", chasing);
        leftLeg.SetBool("Chasing", chasing);
        leftArm.SetBool("Chasing", chasing);
        rightArm.SetBool("Chasing", chasing);
    }
}