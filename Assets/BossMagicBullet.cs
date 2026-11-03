
using UnityEngine;

public class BossMagicBullet : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            Collider2D playerCollider =
                player.GetComponent<Collider2D>();

            Vector2 target = playerCollider.bounds.center;

            Vector2 direction =
                (target - (Vector2)transform.position).normalized;

            GetComponent<Rigidbody2D>().linearVelocity =
                direction * speed;
        }

        Destroy(gameObject, 5f);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Health player = other.GetComponent<Health>();

        if (player != null)
        {
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }


}
