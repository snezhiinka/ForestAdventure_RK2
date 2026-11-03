using UnityEngine;

// Lesson 6: base class, virtual behavior, one collision handler.
public class Hazard : MonoBehaviour
{
    private bool landed;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Health player = collision.gameObject.GetComponent<Health>();
        if (player != null)
        {
            OnHitPlayer(player);
            Destroy(gameObject); // disappear immediately on Player contact
            return;
        }

        // Existing patrolling enemies must not be blocked by falling items.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Ground") && !landed)
        {
            landed = true;
            OnLanded();
        }
    }

    protected virtual void OnHitPlayer(Health player)
    {
        player.TakeDamage(1);
    }

    // Rock/Heart: remove after resting on the ground.
    // BombHazard overrides this for a 2-second fuse.
    protected virtual void OnLanded()
    {
        Destroy(gameObject, 0.5f);
    }
}
