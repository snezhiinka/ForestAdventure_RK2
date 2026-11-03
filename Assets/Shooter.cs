
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float cooldown = 0.3f;
    public float attackDelay = 0.12f;
    public AudioClip shootClip;

    private AudioSource audioSource;
    private SpriteRenderer sr;
    private Animator animator;
    private PlayerInput playerInput;
    private InputAction fireAction;

    private float nextShot;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            fireAction = playerInput.actions.FindAction("Fire");
        }
    }

    void Update()
    {
        if (fireAction != null &&
            fireAction.IsPressed() &&
            Time.time >= nextShot)
        {
            nextShot = Time.time + cooldown;

            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            StartCoroutine(ShootAfterAnimation());
        }
    }

    IEnumerator ShootAfterAnimation()
    {
        yield return new WaitForSeconds(attackDelay);

        bool facingLeft = sr.flipX;

        Vector3 offset = facingLeft
            ? Vector3.left * 0.6f
            : Vector3.right * 0.6f;

        Quaternion rotation = facingLeft
            ? Quaternion.Euler(0, 0, 180)
            : Quaternion.identity;

        if (bulletPrefab != null)
        {
            Instantiate(
                bulletPrefab,
                transform.position + offset,
                rotation
            );
        }

        if (audioSource != null && shootClip != null)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }
}
