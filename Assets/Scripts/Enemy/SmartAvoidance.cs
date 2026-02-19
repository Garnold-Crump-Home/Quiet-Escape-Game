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

    private Rigidbody rb;
    public bool isChasing = false;
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
        if (playScript)
        {


            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);


            if (!isChasing && distance <= detectionRange)
            {
                if (requireLineOfSight)
                {
                    if (HasLineOfSight())
                        isChasing = true;
                }
                else
                {
                    isChasing = true;
                }
            }

            // Lose target if too far
            if (isChasing && distance > loseRange)
                isChasing = false;

            // --- Behavior ---
            if (isChasing)
                ChasePlayer();
            else
                Wander();
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    bool HasLineOfSight()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position + Vector3.up * 1f, dir, out RaycastHit hit, detectionRange, ~0))
        {
            return hit.transform == player;
        }

        return false;
    }

    void ChasePlayer()
    {
        if(wardrobeCollider.isInWardrobe)
        {
            isChasing = false;
            return;
        }
        if (!wardrobeCollider.isInWardrobe)
        {
           
           

            rightLeg.SetBool("Chasing", true);
            leftLeg.SetBool("Chasing", true);
            leftArm.SetBool("Chasing", true);
            rightArm.SetBool("Chasing", true);
            Vector3 direction = (player.position - transform.position).normalized;

            // Smooth rotation
            Quaternion targetRot = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRot, Time.deltaTime * turnSpeed));

            // Move forward
            rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.deltaTime);
        }
    }

    void Wander()
    {
        rightLeg.SetBool("Chasing", false);
        leftLeg.SetBool("Chasing", false);
        leftArm.SetBool("Chasing", false);
        rightArm.SetBool("Chasing", false);
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
            PickNewWanderDirection();

        // Smooth rotation toward wander direction
        Quaternion targetRot = Quaternion.LookRotation(wanderDirection);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRot, Time.deltaTime * wanderTurnSpeed));

        // Move forward
        rb.MovePosition(rb.position + transform.forward * wanderSpeed * Time.deltaTime);
    }

    void PickNewWanderDirection()
    {
        wanderTimer = wanderDirectionChangeInterval;

        // Random horizontal direction
        Vector2 random = Random.insideUnitCircle.normalized;
        wanderDirection = new Vector3(random.x, 0f, random.y);
    }
}
