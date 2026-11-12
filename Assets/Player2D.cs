
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : MonoBehaviour
{
    public float jumpForce = 12f;
    public float speed = 7f;
    public float fallLimit = -6f;

    public AudioSource audioSource;
    public AudioClip coinSound;
    public GameManager2D gameManager;

    public bool canJump = true;
    public bool useThreeLives = false;

    private bool onGround;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private int coins;
    private int coinsTotal;
    private Health health;
    private Vector3 startPosition;


    public bool HasAllCoins()
    {
        return coins >= coinsTotal;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            moveAction = playerInput.actions.FindAction("Move");
            jumpAction = playerInput.actions.FindAction("Jump");
        }
    }

    void Start()
    {
        // Count all coins in the scene
        coinsTotal = GameObject.FindGameObjectsWithTag("Coin").Length;
        health = GetComponent<Health>();
        startPosition = transform.position;
    
    }

    void FixedUpdate()
    {
        float x = 0f;

        if (moveAction != null)
        {
            x = moveAction.ReadValue<Vector2>().x;
        }

        animator.SetFloat("Speed", Mathf.Abs(x));

        if (x != 0)
        {
            spriteRenderer.flipX = x < 0;
        }

        rb.linearVelocity = new Vector2(
            x * speed,
            rb.linearVelocity.y
        );
    }

    void Update()
    {
        Bounds bounds = playerCollider.bounds;

        onGround = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            new Vector2(bounds.center.x, bounds.min.y - 0.05f),
            new Vector2(bounds.size.x * 0.7f, 0.1f),
            0f
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ground"))
            {
                onGround = true;
                break;
            }
        }

        animator.SetBool("Grounded", onGround);

        if (canJump && jumpAction != null &&
            jumpAction.WasPressedThisFrame() && onGround)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }


        if (transform.position.y < fallLimit)
        {
            if (useThreeLives && health != null)
            {
                rb.linearVelocity = Vector2.zero;
                transform.position = startPosition;
                health.TakeDamage(1);
            }
            else
            {
                gameManager.LoseGame();
            }
        }

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!useThreeLives)
            {
                Debug.Log("Game over");
                gameManager.LoseGame();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;

            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            Debug.Log("Coins: " + coins + " / " + coinsTotal);
        }

        if (other.CompareTag("Exit"))
        {
            if (coins >= coinsTotal)
            {
                Debug.Log("You win!");
                gameManager.WinGame();
            }
            else
            {
                Debug.Log(
                    "Collect all coins first! " +
                    coins + " / " + coinsTotal
                );
            }
        }
    }
}
