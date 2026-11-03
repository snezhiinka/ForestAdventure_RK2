
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * 12f;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BossHealth boss = other.GetComponent<BossHealth>();

        if (boss != null)
        {
            boss.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
