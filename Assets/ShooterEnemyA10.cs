
using UnityEngine;

public class ShooterEnemyA10 : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;

    public Sprite idleSprite;
    public Sprite aimSprite;
    public Sprite chargeSprite;
    public Sprite shootSprite;

    public float visionRange = 7f;
    public float windUp = 0.6f;
    public float cooldown = 2f;
    public LayerMask wallLayer;

    enum State { Idle, Aim, Shoot, Cooldown }
    State state = State.Idle;

    float timer;
    float nextShotTime;

    SpriteRenderer sr;
    Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        SetSprite(idleSprite);
    }

    void Update()
    {
        if (player == null) return;

        // Face the player
        sr.flipX = player.position.x < transform.position.x;

        if (state == State.Idle)
        {
            SetSprite(idleSprite);

            if (CanSeePlayer() && Time.time >= nextShotTime)
            {
                state = State.Aim;
                timer = windUp;
                SetSprite(aimSprite);
                sr.color = Color.red;
            }
        }
        else if (state == State.Aim)
        {
            if (!CanSeePlayer())
            {
                sr.color = originalColor;
                SetSprite(idleSprite);
                state = State.Idle;
                return;
            }

            timer -= Time.deltaTime;

            if (timer <= windUp / 2f)
                SetSprite(chargeSprite);

            if (timer <= 0f)
                state = State.Shoot;
        }
        else if (state == State.Shoot)
        {
            SetSprite(shootSprite);
            sr.color = originalColor;

            Shoot();

            nextShotTime = Time.time + cooldown;
            timer = 0.15f;
            state = State.Cooldown;
        }
        else if (state == State.Cooldown)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
                SetSprite(idleSprite);

            if (Time.time >= nextShotTime)
                state = State.Idle;
        }
    }

    void SetSprite(Sprite newSprite)
    {
        if (newSprite != null)
            sr.sprite = newSprite;
    }

    bool CanSeePlayer()
    {
        Vector2 direction =
            player.position - transform.position;

        if (direction.magnitude > visionRange)
            return false;

        if (Camera.main == null)
            return false;

        Vector3 view = Camera.main.WorldToViewportPoint(
            transform.position);

        if (view.z <= 0f ||
            view.x < 0f || view.x > 1f ||
            view.y < 0f || view.y > 1f)
            return false;

        RaycastHit2D wall = Physics2D.Raycast(
            transform.position,
            direction.normalized,
            direction.magnitude,
            wallLayer);

        return wall.collider == null;
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        bool facingLeft =
            player.position.x < transform.position.x;

        Vector3 spawnPosition = transform.position +
            new Vector3(
                facingLeft ? -0.75f : 0.75f,
                0f,
                0f
            );

        Vector2 direction =
            player.position - spawnPosition;

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPosition,
            Quaternion.identity);

        EnemyBulletA10 projectile =
            bullet.GetComponent<EnemyBulletA10>();

        if (projectile != null)
            projectile.Launch(direction);
    }
}
