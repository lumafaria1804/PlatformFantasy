using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rig;
    private Animator animator;

    [Header("Stats")]
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float initialspeed;
    [SerializeField] private float speedRun;
    [SerializeField] private float jumpForce;
    [SerializeField] private float horizontalJump;
    [Header("Jump Settings")]
    [SerializeField] private int maxJumpCount = 2;
    private int jumpCount;
    private bool isMovement;
    private bool isJumping;
    private bool hit;
    private bool isAttacking;
    private bool _isRunning;

    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    public bool Hit
    {
        get { return hit; }
        set { hit = value; }
    }


    public bool IsMovement
    {
        get { return isMovement; }
        set { isMovement = value; }
    }

    public bool IsJumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool IsClimbing
    {
        get { return isClimbing; }
    }

    private bool isClimbing;
    private bool isNearClimbable;
    private float climbSpeed = 4f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialspeed = speed;
        jumpCount = 0;
    }

    void Update()
    {
        if (!isClimbing)
        {
            HandleRun();
            OnMove();
            OnJump();
            OnAttack();
        }
        HandleClimbingInput();
    }

    void OnMove()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.linearVelocity = new Vector2(movement * speed, rig.linearVelocity.y);

        if (movement == 0)
        {
            isMovement = false;
        }
        else
        {
            isMovement = true;
            transform.eulerAngles = movement < 0 ? new Vector3(0, 180, 0) : Vector3.zero;
        }
    }

    void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            float movement = Input.GetAxis("Horizontal");
            Vector2 jumpDirection = new Vector2(movement * horizontalJump, jumpForce);

            rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0f);
            rig.AddForce(jumpDirection, ForceMode2D.Impulse);

            isJumping = true;
            jumpCount++;
        }
    }

    void HandleRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedRun;
        }
        else
        {
            speed = initialspeed;
        }
    }
    void OnAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
           speed = speedRun;
           isAttacking = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            speed = initialspeed;
            isAttacking = false;
        }
    }

    void HandleClimbingInput()
    {
        if (isNearClimbable && Input.GetKeyDown(KeyCode.E))
        {
            isClimbing = !isClimbing;

            if (isClimbing)
            {
                rig.gravityScale = 0f;
                rig.linearVelocity = Vector2.zero;

                if (animator != null)
                {
                    animator.SetBool("isClimbing", true);
                }
            }
            else
            {
                rig.gravityScale = 1f;

                if (animator != null)
                {
                    animator.SetBool("isClimbing", false);
                    animator.SetFloat("ClimbSpeed", 0f);
                }
            }
        }

        if (isClimbing)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            rig.linearVelocity = new Vector2(0f, vertical * climbSpeed);

            if (animator != null)
            {
                animator.SetFloat("ClimbSpeed", Mathf.Abs(vertical));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isJumping = false;
                    jumpCount = 0;
                    break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isNearClimbable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isNearClimbable = false;

            if (isClimbing)
            {
                isClimbing = false;
                rig.gravityScale = 1f;

                if (animator != null)
                {
                    animator.SetBool("isClimbing", false);
                    animator.SetFloat("ClimbSpeed", 0f);
                }
            }
        }
    }
}
