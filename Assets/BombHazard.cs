using UnityEngine;
using TMPro;

// Lessons 3 and 6: Bomb has overridden damage and Invoke explosion.
public class BombHazard : Hazard
{
    public GameObject explosionPrefab;
    public TMP_Text timerText;

    private bool fuseRunning;
    private float remaining;

    void Start()
    {
        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!fuseRunning) return;
        remaining = Mathf.Max(0f, remaining - Time.deltaTime);
        if (timerText != null)
            timerText.text = remaining.ToString("F1");
    }

    protected override void OnHitPlayer(Health player)
    {
        player.TakeDamage(2);
    }

    protected override void OnLanded()
    {
        if (fuseRunning) return;
        fuseRunning = true;
        remaining = 2f;
        if (timerText != null)
        {
            timerText.text = "2.0";
            timerText.gameObject.SetActive(true);
        }
        Invoke(nameof(Explode), 2f);
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(fx, 0.4f);
        }
        Destroy(gameObject);
    }
}
