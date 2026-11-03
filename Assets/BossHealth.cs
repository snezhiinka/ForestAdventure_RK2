
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int hp = 5;
    public Slider bossHPBar;
    public GameManager2D gameManager;
    public AudioClip hurtClip;

    private SpriteRenderer sr;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        bossHPBar.maxValue = hp;
        bossHPBar.value = hp;
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;
        hp = Mathf.Max(hp, 0);

        bossHPBar.value = hp;

        if (hurtClip != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(
                hurtClip,
                Camera.main.transform.position
            );
        }

        if (hp <= 0)
        {
            gameManager.WinGame();
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        sr.color = originalColor;
    }
}
