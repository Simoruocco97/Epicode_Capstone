using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private MovementController movementController;
    [SerializeField] private PlayerAnimationHandler animator;
    [SerializeField] private Rigidbody2D rb;

    [Header("Speed Settings")]
    [SerializeField] private float speed = 2f;

    [Header("Jump Attributes")]
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private float jumpForce = 4f;
    private bool canJump = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (movementController == null)
            movementController = GetComponent<MovementController>();

        if (animator == null)
            animator = GetComponent<PlayerAnimationHandler>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (groundChecker == null)
            groundChecker = GetComponentInChildren<GroundChecker>();
    }

    private void Update()
    {
        if (groundChecker == null || rb == null)
            return;

        if (animator != null)
            animator.SetGrounded(rb.velocity.y <= -0.1f && groundChecker.CheckIsGrounded());

        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.CheckIsGrounded())
            canJump = true;
    }

    private void FixedUpdate()
    {
        if (rb == null || animator == null)
            return;

        rb.velocity = new Vector2(movementController.MoveX * speed, rb.velocity.y);
        animator.MovementAnimation(new Vector2(rb.velocity.x, 0f));

        if (canJump && animator != null)
        {
            animator.JumpAnimation();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    public void DisableMovement()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        enabled = false;
    }

    public void EnableMovement()
    {
        rb.isKinematic = false;
        enabled = true;
    }
}