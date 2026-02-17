using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 7f;
    public float stamina = 100f;
    public bool isSprinting = false;

    [Header("Crouch")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeedMultiplier = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private bool isGrounded;
    private bool isCrouching;

    [Header("Flashlight")]
    public Light flashlight;
    public Animator flashlightAnimation;


    [Header("Idle Animation")]
    public Animator IdleLeft;
    public Animator IdleRight;

    public bool HoldingObj = false;
    public Camera Camera;

    public Slider staminaSlider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;
        standHeight = capsule.height;
    }

    void Update()
    {
        staminaSlider.value = stamina;
        staminaSlider.maxValue = 100f;
        staminaSlider.minValue = 0f;
        FlashLight();
        HandleJump();
        HandleCrouch();
        IsSprinting();
        IdleAnimation();



        if (isCrouching == true)
        {
            moveSpeed = 3f;
        }
        else if(isCrouching == false && isSprinting == false) 
        {
            moveSpeed = 8f;
        }

    }

    void FixedUpdate()
    {
        HandleMovement();
        CheckGround();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = isCrouching ? moveSpeed * crouchSpeedMultiplier : moveSpeed;

        rb.velocity = new Vector3(move.x * currentSpeed, rb.velocity.y, move.z * currentSpeed);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            capsule.height = crouchHeight;
        }
        else
        {
            isCrouching = false;
            capsule.height = standHeight;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void FlashLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.enabled)
            {
               
                flashlight.enabled = false;
                flashlightAnimation.SetTrigger("ToggleLight");
                flashlightAnimation.Play("FlashlightToggle", 0, 0f);

            }
            else
            {
                flashlight.enabled = true;
                flashlightAnimation.SetTrigger("ToggleLight");
                flashlightAnimation.Play("FlashlightToggle", 0, 0f);
            }


        }
    }

    void IsSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            stamina -= 10f * Time.deltaTime;
            if(stamina > 0f)
            {
               moveSpeed = 12f;
                isSprinting = true;
                IdleRight.SetBool("isSprinting", true);
                IdleLeft.SetBool("isSprinting", true);
                Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, 75f, Time.deltaTime * 5f);
            }

           
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
          isSprinting = false;
        }
        if(stamina <= 0f)
        {
            stamina = 0f;
            moveSpeed = 8f;
            isSprinting = false;
        }
        if (isSprinting != true)
        {
            IdleLeft.SetBool("isSprinting", false);
            IdleRight.SetBool("isSprinting", false);
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, 60f, Time.deltaTime * 5f);
            stamina += 5f * Time.deltaTime;
            if(stamina > 100f)
            {
                stamina = 100f;
                moveSpeed = 8f;

            }
        }
    }

    void IdleAnimation()
    {
        if(rb.velocity.magnitude < 1f && isGrounded && !Input.anyKey && !Input.anyKeyDown)
        {
           Invoke("StartAnimation", 1f);
        }
        else
        {
            IdleLeft.SetBool("isIdle", false);
            IdleRight.SetBool("isIdle", false);
        }
    }

    void StartAnimation()
    {
        IdleLeft.SetBool("isIdle", true);
        IdleRight.SetBool("isIdle", true);
    }

    
}
       

