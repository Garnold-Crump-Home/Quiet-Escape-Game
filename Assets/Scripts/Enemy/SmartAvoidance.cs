using UnityEngine;
using UnityEngine.SceneManagement;

public class SmartAvoidance : MonoBehaviour
{
    public bool playScript = true;

    [Header("Target")]
    public Transform player;
    public GameObject playerObject;

    [Header("Detection")]
    public float detectionRange = 18f;
    public float hearingRange = 6f;
    public float loseRange = 22f;
    public float memoryDuration = 6f;

    [Header("Movement")]
    public float moveSpeed = 4.5f;
    public float turnSpeed = 14f;

    [Header("Wandering")]
    public float wanderSpeed = 2f;
    public float wanderTurnSpeed = 6f;
    public float wanderDirectionChangeInterval = 3f;

    [Header("Avoidance")]
    public float rayDistance = 4f;
    public float avoidStrength = 6f;
    public LayerMask obstacleMask;

    [Header("Horror Behavior")]
    public float searchRadius = 4f;
    public float fakeWanderChance = 0.15f;
    public float cornerCutStrength = 1.5f;

    [Header("Hands")]
    public bool leftHandHit;
    public bool rightHandHit;
    public bool frontHit;

    [Header("Animations")]
    public Animator rightLeg;
    public Animator leftLeg;
    public Animator leftArm;
    public Animator rightArm;
    public bool animationsEnabled = true;

    [Header("Startup")]
    public float activationDelay = 22f;
    private bool isActive = false;

    public WardrobeCollider wardrobeCollider;
    private enum AIState { Inactive, Wander, Search, Chase }
    private AIState state = AIState.Inactive;

    private Rigidbody rb;
    private Vector3 wanderDirection;
    private float wanderTimer;

    private Vector3 lastKnownPosition;
    private float memoryTimer;

    private Vector3 searchPoint;
    private bool hasSearchPoint;

    private Vector3 lastPosition;
    private float stuckTimer;
    void Update()
    {
        bool on = animationsEnabled;

        if (rightArm) rightArm.enabled = on;
        if (leftArm) leftArm.enabled = on;
        if (rightLeg) rightLeg.enabled = on;
        if (leftLeg) leftLeg.enabled = on;
        if (wardrobeCollider.isInWardrobe)
        {
            detectionRange = 0f;
        }
        if(wardrobeCollider.isInWardrobe == false)
        {
            detectionRange = 35f;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        state = AIState.Inactive;

        PickNewWanderDirection();

        if (playerObject != null)
            playerObject.SetActive(false);

        Invoke(nameof(Activate), activationDelay);
    }

    void Activate()
    {
        isActive = true;

        if (playerObject != null)
            playerObject.SetActive(true);

        state = AIState.Wander;
    }

    void FixedUpdate()
    {
        if (!isActive || !playScript || player == null) return;

        UpdateDetection();
        RunStateMachine();
        HandleStuck();
    }

    // ───────── DETECTION ─────────
    void UpdateDetection()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        bool canSee = dist <= detectionRange;
        bool canHear = dist <= hearingRange;

        if (canSee || canHear)
        {
            lastKnownPosition = player.position;
            memoryTimer = memoryDuration;
            state = AIState.Chase;
        }
        else if (state == AIState.Chase)
        {
            state = AIState.Search;
            hasSearchPoint = false;
        }
    }

    // ───────── STATES ─────────
    void RunStateMachine()
    {
        switch (state)
        {
            case AIState.Chase: Chase(); break;
            case AIState.Search: Search(); break;
            case AIState.Wander: Wander(); break;
        }
    }

    void Chase()
    {
        Vector3 predicted = player.position + player.forward * cornerCutStrength;
        Move(predicted, moveSpeed, turnSpeed);
    }

    void Search()
    {
        memoryTimer -= Time.deltaTime;

        if (memoryTimer <= 0f)
        {
            state = AIState.Wander;
            return;
        }

        if (!hasSearchPoint || Vector3.Distance(transform.position, searchPoint) < 1f)
        {
            searchPoint = lastKnownPosition + Random.insideUnitSphere * searchRadius;
            searchPoint.y = transform.position.y;
            hasSearchPoint = true;
        }

        Move(searchPoint, wanderSpeed * 1.5f, turnSpeed);
        UpdateAnimations();
        // Random fake wander to feel creepy
        if (Random.value < fakeWanderChance * Time.deltaTime)
        {
            state = AIState.Wander;
            Invoke(nameof(ReturnToSearch), 2f);
        }
    }

    void ReturnToSearch()
    {
        if (state == AIState.Wander)
            state = AIState.Search;
    }

    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
            PickNewWanderDirection();

        Move(transform.position + wanderDirection, wanderSpeed, wanderTurnSpeed);
    }

    // ───────── MOVEMENT ─────────
    void Move(Vector3 target, float speed, float rotSpeed)
    {
        Vector3 direction = (target - transform.position).normalized;

        Vector3 avoid = GetAvoidanceVector();
        Vector3 handAvoid = GetHandAvoidance();

        direction = (direction + avoid + handAvoid).normalized;

        Quaternion rot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, rot, Time.deltaTime * rotSpeed));
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    // ───────── SMART HAND LOGIC ─────────
    Vector3 GetHandAvoidance()
    {
        Vector3 avoid = Vector3.zero;

        if (frontHit)
        {
            avoid -= transform.forward * 2f;
            avoid += transform.right * Random.Range(-1f, 1f);
        }
        else if (leftHandHit && !rightHandHit)
        {
            avoid += transform.right * 2f;
        }
        else if (rightHandHit && !leftHandHit)
        {
            avoid -= transform.right * 2f;
        }
        else if (leftHandHit && rightHandHit)
        {
            avoid -= transform.forward * 2f;
            avoid += transform.right * Random.Range(-2f, 2f);
        }

        return avoid;
    }

    // ───────── RAYCAST AVOIDANCE ─────────
    Vector3 GetAvoidanceVector()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 forward = transform.forward;

        Vector3 left = Quaternion.AngleAxis(-30, Vector3.up) * forward;
        Vector3 right = Quaternion.AngleAxis(30, Vector3.up) * forward;

        bool f = Physics.Raycast(origin, forward, rayDistance, obstacleMask);
        bool l = Physics.Raycast(origin, left, rayDistance, obstacleMask);
        bool r = Physics.Raycast(origin, right, rayDistance, obstacleMask);

        Vector3 avoid = Vector3.zero;

        if (f) avoid -= forward * avoidStrength;
        if (l) avoid += transform.right * avoidStrength;
        if (r) avoid -= transform.right * avoidStrength;

        return avoid;
    }

    // ───────── UNSTUCK ─────────
    void HandleStuck()
    {
        float moved = Vector3.Distance(transform.position, lastPosition);

        if (moved < 0.05f)
            stuckTimer += Time.deltaTime;
        else
            stuckTimer = 0f;

        lastPosition = transform.position;

        if (stuckTimer > 1.5f)
        {
            PickNewWanderDirection();
            stuckTimer = 0f;
        }
    }

    void PickNewWanderDirection()
    {
        wanderTimer = wanderDirectionChangeInterval;
        Vector2 rand = Random.insideUnitCircle.normalized;
        wanderDirection = new Vector3(rand.x, 0, rand.y);
    }
    void UpdateAnimations()
    {
        if (!animationsEnabled) return;

        float dist = Vector3.Distance(transform.position, player.position);

        bool idle = state == AIState.Wander && dist > loseRange;
        bool walking = state == AIState.Search || (state == AIState.Wander && !idle);
        bool chasing = state == AIState.Chase;

        SetAnim("Idle", idle);
        SetAnim("Walking", walking);
        SetAnim("Chasing", chasing);
    }

    void SetAnim(string param, bool value)
    {
        if (rightLeg) rightLeg.SetBool(param, value);
        if (leftLeg) leftLeg.SetBool(param, value);
        if (leftArm) leftArm.SetBool(param, value);
        if (rightArm) rightArm.SetBool(param, value);
    }
}