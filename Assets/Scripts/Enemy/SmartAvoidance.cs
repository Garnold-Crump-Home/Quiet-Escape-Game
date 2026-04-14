using UnityEngine;
using UnityEngine.SceneManagement;


public class SmartAvoidance : MonoBehaviour
{
    
    public bool playScript = true;

    [Header("Target")]
    public Transform player;
    public GameObject playerObject;

    [Header("Detection – Sight")]
    public float detectionRange = 12f;
    public float loseRange = 16f;
    public bool requireLineOfSight = true;
    public LayerMask obstacleMask;

    [Header("Detection – Sound")]
    [Tooltip("Radius inside which the AI hears the player regardless of LOS")]
    public float hearingRange = 5f;

    [Header("Detection – Memory")]
    [Tooltip("How long the AI investigates the player's last known position before giving up")]
    public float memoryDuration = 5f;

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

    [Header("Animations")]
    public Animator rightLeg;
    public Animator leftLeg;
    public Animator leftArm;
    public Animator rightArm;
    public bool animationsEnabled = true;

    [Header("Wardrobe")]
    public WardrobeCollider wardrobeCollider;

    [Header("Startup")]
    [Tooltip("Delay (seconds) before the AI activates on non-Tutorial scenes")]
    public float activationDelay = 22f;

    // ── State machine ────────────────────────────────────────────────────────
    private enum AIState { Inactive, Wander, Investigate, Chase }
    private AIState state = AIState.Inactive;

    // ── Private fields ───────────────────────────────────────────────────────
    private Rigidbody rb;
    private Vector3 wanderDirection;
    private float wanderTimer;
    private Vector3 lastKnownPosition;
    private float memoryTimer;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Always pick a wander direction immediately so it's ready when we activate
        PickNewWanderDirection();

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Tutorial")
        {
            Activate();
        }
        else
        {
            if (playerObject != null) playerObject.SetActive(false);
            Invoke(nameof(Activate), activationDelay);
        }
    }

    void Update()
    {
        if (state == AIState.Inactive) return;

        // Toggle animations at runtime
        bool on = animationsEnabled;
        if (rightArm) rightArm.enabled = on;
        if (leftArm) leftArm.enabled = on;
        if (rightLeg) rightLeg.enabled = on;
        if (leftLeg) leftLeg.enabled = on;
    }

    void FixedUpdate()
    {
        if (state == AIState.Inactive || !playScript || player == null) return;

        UpdateDetection();
        RunStateMachine();

        // Keep upright
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    // ── Activation ───────────────────────────────────────────────────────────
    void Activate()
    {
        if (playerObject != null) playerObject.SetActive(true);
        state = AIState.Wander;
    }

    // ── Detection logic ──────────────────────────────────────────────────────
    void UpdateDetection()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        bool canSee = dist <= detectionRange && (!requireLineOfSight || HasLineOfSight());
        bool canHear = dist <= hearingRange;

        if (canSee || canHear)
        {
            lastKnownPosition = player.position;
            memoryTimer = memoryDuration;
            state = AIState.Chase;
        }
        else if (state == AIState.Chase)
        {
            // Lost visual/sound – investigate last known position
            state = AIState.Investigate;
        }
    }

    bool HasLineOfSight()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 dir = (player.position - transform.position).normalized;

        if (Physics.Raycast(origin, dir, out RaycastHit hit, detectionRange, obstacleMask | (1 << player.gameObject.layer)))
            return hit.transform == player;

        // Raycast hit nothing blocked – check plain distance
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    // ── State machine ─────────────────────────────────────────────────────────
    void RunStateMachine()
    {
        switch (state)
        {
            case AIState.Chase: ChasePlayer(); break;
            case AIState.Investigate: Investigate(); break;
            case AIState.Wander: Wander(); break;
        }
    }

    // ── Behaviours ────────────────────────────────────────────────────────────
    void ChasePlayer()
    {
        if (wardrobeCollider != null && wardrobeCollider.isInWardrobe)
        {
            state = AIState.Wander;
            return;
        }

        UpdateAnimations();
        MoveToward(player.position, moveSpeed, turnSpeed);
    }

    void Investigate()
    {
        UpdateAnimations();

        memoryTimer -= Time.deltaTime;
        if (memoryTimer <= 0f)
        {
            state = AIState.Wander;
            return;
        }

        float distToLKP = Vector3.Distance(transform.position, lastKnownPosition);
        if (distToLKP < 1f)
        {
            // Arrived at last known position – look around before giving up
            memoryTimer -= Time.deltaTime * 2f;
        }
        else
        {
            MoveToward(lastKnownPosition, wanderSpeed * 1.5f, turnSpeed);
        }
    }

    void Wander()
    {
        UpdateAnimations();

        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f)
            PickNewWanderDirection();

        Vector3 avoid = GetAvoidanceVector();
        Vector3 direction = avoid != Vector3.zero
            ? (wanderDirection + avoid).normalized
            : wanderDirection;

        RotateToward(direction, wanderTurnSpeed);
        rb.MovePosition(rb.position + transform.forward * wanderSpeed * Time.deltaTime);
    }

    // ── Shared movement helpers ───────────────────────────────────────────────
    void MoveToward(Vector3 target, float speed, float rotSpeed)
    {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 avoid = GetAvoidanceVector();
        if (avoid != Vector3.zero)
            direction = (direction + avoid).normalized;

        RotateToward(direction, rotSpeed);
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

    void RotateToward(Vector3 direction, float rotSpeed)
    {
        if (direction == Vector3.zero) return;
        Quaternion targetRot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRot, Time.deltaTime * rotSpeed));
    }

    void PickNewWanderDirection()
    {
        wanderTimer = wanderDirectionChangeInterval;
        Vector2 random = Random.insideUnitCircle.normalized;
        wanderDirection = new Vector3(random.x, 0f, random.y);
    }

    // ── Avoidance ─────────────────────────────────────────────────────────────
    Vector3 GetAvoidanceVector()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 origin = transform.position + Vector3.up;

        bool frontHit = Physics.Raycast(origin, forward, out RaycastHit hitF, rayDistance, obstacleMask);
        bool leftHit = Physics.Raycast(origin - right * sideOffset, forward, rayDistance, obstacleMask);
        bool rightHit = Physics.Raycast(origin + right * sideOffset, forward, rayDistance, obstacleMask);

        Vector3 avoid = Vector3.zero;
        if (frontHit) avoid += hitF.normal * avoidStrength;
        if (leftHit) avoid += transform.right * avoidStrength;
        if (rightHit) avoid -= transform.right * avoidStrength;

        return avoid;
    }

    // ── Animations ────────────────────────────────────────────────────────────
    void UpdateAnimations()
    {
        if (!animationsEnabled) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Thresholds are relative to detection/lose ranges for meaningful transitions
        bool idle = state == AIState.Wander && dist > loseRange;
        bool walking = !idle && state != AIState.Chase;
        bool chasing = state == AIState.Chase;

        SetAnimBool("Idle", idle);
        SetAnimBool("Walking", walking);
        SetAnimBool("Chasing", chasing);
    }

    void SetAnimBool(string param, bool value)
    {
        if (rightLeg) rightLeg.SetBool(param, value);
        if (leftLeg) leftLeg.SetBool(param, value);
        if (leftArm) leftArm.SetBool(param, value);
        if (rightArm) rightArm.SetBool(param, value);
    }
}