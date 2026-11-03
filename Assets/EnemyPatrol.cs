using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1.5f;
    public float moveDistance = 1.5f;

    private float startX;
    private int direction = 1;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startX = transform.position.x;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (transform.position.x >= startX + moveDistance)
        {
            direction = -1;
        }
        else if (transform.position.x <= startX - moveDistance)
        {
            direction = 1;
        }

        spriteRenderer.flipX = direction > 0;

    }
}