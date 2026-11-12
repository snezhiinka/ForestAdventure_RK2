
using UnityEngine;

public class EnemyBulletA10 : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 1;

    public Sprite[] frames;
    public float frameTime = 0.1f;

    Rigidbody2D rb;
    SpriteRenderer sr;

    float timer;
    int frameIndex = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (frames.Length > 0)
            sr.sprite = frames[0];
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex = (frameIndex + 1) % frames.Length;
            sr.sprite = frames[frameIndex];
        }
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;

        float angle = Mathf.Atan2(
            direction.y, direction.x) * Mathf.Rad2Deg;

        rb.rotation = angle;

        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health playerHealth = other.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger &&
                 !other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
