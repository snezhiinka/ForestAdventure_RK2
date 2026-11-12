
using UnityEngine;

public class ChargerA10 : MonoBehaviour
{
    public Transform player;

    public Sprite idleSprite;
    public Sprite windUpSprite;
    public Sprite dashSprite1;
    public Sprite dashSprite2;
    public Sprite dashSprite3;
    public Sprite recoverySprite;

    public float detectionRange = 4f;
    public float windUp = 0.5f;
    public float dashSpeed = 8f;
    public float dashDuration = 0.5f;
    public float recoveryTime = 1.5f;
    public int damage = 1;

    enum State { Idle, WindUp, Dash, Recovery }
    State state = State.Idle;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Color originalColor;

    float timer;
    float frameTimer;
    int frameIndex;
    int direction;
    bool hasHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        originalColor = sr.color;
        SetSprite(idleSprite);
    }

    void FixedUpdate()
    {
        if (state == State.Idle)
        {
            StopMoving();
            SetSprite(idleSprite);

            if (player != null && IsOnScreen() &&
                Vector2.Distance(transform.position,
                player.position) <= detectionRange &&
                Mathf.Abs(player.position.y -
                transform.position.y) < 1.5f)
            {
                direction = player.position.x >
                    transform.position.x ? 1 : -1;

                sr.flipX = direction < 0;
                sr.color = Color.red;

                timer = windUp;
                state = State.WindUp;
            }
        }
        else if (state == State.WindUp)
        {
            StopMoving();
            SetSprite(windUpSprite);

            timer -= Time.fixedDeltaTime;

            if (timer <= 0f)
            {
                sr.color = originalColor;
                hasHit = false;

                timer = dashDuration;
                frameTimer = 0f;
                frameIndex = 0;

                state = State.Dash;
            }
        }
        else if (state == State.Dash)
        {
            if (!IsOnScreen())
            {
                FinishDash();
                return;
            }

            rb.linearVelocity = new Vector2(
                direction * dashSpeed,
                rb.linearVelocity.y
            );

            frameTimer += Time.fixedDeltaTime;

            if (frameTimer >= 0.08f)
            {
                frameTimer = 0f;
                frameIndex = (frameIndex + 1) % 3;
            }

            if (frameIndex == 0)
                SetSprite(dashSprite1);
            else if (frameIndex == 1)
                SetSprite(dashSprite2);
            else
                SetSprite(dashSprite3);

            timer -= Time.fixedDeltaTime;

            if (timer <= 0f)
                FinishDash();
        }
        else if (state == State.Recovery)
        {
            StopMoving();
            SetSprite(recoverySprite);

            timer -= Time.fixedDeltaTime;

            if (timer <= 0f)
                state = State.Idle;
        }
    }

    void FinishDash()
    {
        StopMoving();
        timer = recoveryTime;
        state = State.Recovery;
    }

    void SetSprite(Sprite newSprite)
    {
        if (newSprite != null)
            sr.sprite = newSprite;
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(
            0f, rb.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        TryDamage(other.collider);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        TryDamage(other.collider);
    }

    void TryDamage(Collider2D other)
    {
        if (state != State.Dash || hasHit ||
            !IsOnScreen()) return;

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
            hasHit = true;
        }
    }

    bool IsOnScreen()
    {
        if (Camera.main == null) return false;

        Vector3 view = Camera.main.WorldToViewportPoint(
            transform.position
        );

        return view.z > 0f &&
               view.x >= 0f && view.x <= 1f &&
               view.y >= 0f && view.y <= 1f;
    }
}
