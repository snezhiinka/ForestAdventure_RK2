
using UnityEngine;

public class BossMagicShoot : MonoBehaviour
{
    public GameObject magicPrefab;
    public AudioClip magicSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        InvokeRepeating(nameof(Shoot), 1.5f, 2f);
    }

    void Shoot()
    {
        Vector3 position = transform.position +
            new Vector3(-0.8f, 0.5f, 0f);

        Instantiate(magicPrefab, position, Quaternion.identity);

        if (audioSource != null && magicSound != null)
        {
            audioSource.PlayOneShot(magicSound);
        }
    }
}
