
using UnityEngine;

public class PatrollerA10 : MonoBehaviour
{
    public Transform player;

    public float speed = 1.5f;
    public float chaseSpeed = 2.5f;
    public float moveDistance = 0.7f;

    public float visionRange = 4f;
    public float visionAngle = 120f;
    public LayerMask wallLayer;

    public float attackRange = 1.8f;
    public int damage = 1;
    public float windUp = 0.6f;
    public float cooldown = 1.5f;

    enum State { Patrol, Chase, Attack }
    State state = State.Patrol;

    float startX;
    float attackTimer;
    float nextAttackTime;
    int direction = 1;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Health playerHealth;
    Color originalColor;

    void Start()
    {
        startX = transform.position.x;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (player != null)
            playerHealth = player.GetComponent<Health>();
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            state = State.Patrol;
            Patrol();
            return;
        }

        if (state == State.Attack)
        {
            Attack();
            return;
        }

        if (CanSeePlayer())
        {
            float distance = Vector2.Distance(
                transform.position, player.position);

            if (distance <= attackRange)
            {
                StopMoving();

                if (Time.time >= nextAttackTime && IsOnScreen())
                {
                    state = State.Attack;
                    attackTimer = windUp;
                    spriteRenderer.color = Color.red;
                }
            }
            else
            {
                state = State.Chase;
                Chase();
            }
        }
        else
        {
            state = State.Patrol;
            Patrol();
        }
    }

    void Patrol()
    {
        if (transform.position.x >= startX + moveDistance)
            direction = -1;
        else if (transform.position.x <= startX - moveDistance)
            direction = 1;

        Move(direction, speed);
    }

    void Chase()
    {
        direction = player.position.x > transform.position.x
            ? 1 : -1;

        Move(direction, chaseSpeed);
    }

    void Move(int moveDirection, float moveSpeed)
    {
        rb.linearVelocity = new Vector2(
            moveDirection * moveSpeed,
            rb.linearVelocity.y
        );

        spriteRenderer.flipX = moveDirection > 0;
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(
            0f, rb.linearVelocity.y);
    }

    void Attack()
    {
        StopMoving();
        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer > 0f)
            return;

        float distance = Vector2.Distance(
            transform.position, player.position);

        if (distance <= attackRange &&
            CanSeePlayer() &&
            IsOnScreen() &&
            playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        spriteRenderer.color = originalColor;
        nextAttackTime = Time.time + cooldown;
        state = State.Chase;
    }

    bool CanSeePlayer()
    {
        Vector2 toPlayer =
            player.position - transform.position;

        if (toPlayer.magnitude > visionRange)
            return false;

        Vector2 facing = direction == 1
            ? Vector2.right : Vector2.left;

        if (Vector2.Angle(facing, toPlayer) > visionAngle / 2f)
            return false;

        RaycastHit2D wall = Physics2D.Raycast(
            transform.position,
            toPlayer.normalized,
            toPlayer.magnitude,
            wallLayer);

        return wall.collider == null;
    }

    bool IsOnScreen()
    {
        if (Camera.main == null)
            return false;

        Vector3 view = Camera.main.WorldToViewportPoint(
            transform.position);

        return view.z > 0f &&
               view.x >= 0f && view.x <= 1f &&
               view.y >= 0f && view.y <= 1f;
    }
}
