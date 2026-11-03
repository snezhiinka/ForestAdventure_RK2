
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int hp = 3;

    public AudioClip hurtClip;
    public AudioClip healClip;

    public GameManager2D gameManager;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private AudioSource audioSource;
    private SpriteRenderer sr;
    private bool invincible = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        UpdateHearts();
    }

    public void TakeDamage(int dmg)
    {
        if (invincible || hp <= 0) return;

        hp -= dmg;
        hp = Mathf.Max(hp, 0);

        UpdateHearts();

        if (audioSource != null && hurtClip != null)
            audioSource.PlayOneShot(hurtClip);

        Debug.Log("Player HP: " + hp);

        if (hp <= 0)
        {
            if (gameManager != null)
                gameManager.LoseGame();
            else
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().name
                );

            return;
        }

        StartCoroutine(Invincibility());
    }

    IEnumerator Invincibility()
    {
        invincible = true;

        for (int i = 0; i < 10; i++)
        {
            if (sr != null)
                sr.enabled = !sr.enabled;

            yield return new WaitForSeconds(0.2f);
        }

        if (sr != null)
            sr.enabled = true;

        invincible = false;
    }

    public void Heal(int amount)
    {
        if (hp <= 0) return;

        hp = Mathf.Min(hp + amount, 3);

        UpdateHearts();

        if (audioSource != null && healClip != null)
            audioSource.PlayOneShot(healClip);
    }

    void UpdateHearts()
    {
        if (heart1 != null)
            heart1.SetActive(hp >= 1);

        if (heart2 != null)
            heart2.SetActive(hp >= 2);

        if (heart3 != null)
            heart3.SetActive(hp >= 3);
    }
}
