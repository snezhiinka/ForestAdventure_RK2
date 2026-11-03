using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    // Скорость движения шара
    public float speed = 5f;

    // Сила прыжка
    public float jumpForce = 300f;

    // Насколько далеко вниз проверяем землю
    public float groundCheckDistance = 0.6f;

    // Звук монеты
    public AudioSource audioSource;
    public AudioClip coinSound;

    // Текст количества монет
    public TextMeshProUGUI coinText;

    // Rigidbody шара
    private Rigidbody rb;

    // Количество собранных монет
    private int coins = 0;

    public int Coins
    {
        get { return coins; }
    }


    void Start()
    {
        // Находим Rigidbody на Sphere один раз
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // -------------------------
        // ДВИЖЕНИЕ
        // -------------------------

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Translate(
            x * speed * Time.deltaTime,
            0,
            z * speed * Time.deltaTime
        );


        // -------------------------
        // ПРОВЕРКА ЗЕМЛИ
        // -------------------------

        bool isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance
        );


        // -------------------------
        // ПРЫЖОК
        // -------------------------

        // Прыгать можно только тогда,
        // когда под шаром есть поверхность
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(
                Vector3.up * jumpForce
            );
        }
    }


    void OnTriggerEnter(Collider coin)
    {
        // -------------------------
        // ОБЫЧНАЯ МОНЕТА = +1
        // -------------------------

        if (coin.CompareTag("Coin"))
        {
            coins += 1;

            coinText.text = "Монеты: " + coins;

            audioSource.PlayOneShot(coinSound);

            Destroy(coin.gameObject);
        }


        // -------------------------
        // БОЛЬШАЯ МОНЕТА = +3
        // -------------------------

        else if (coin.CompareTag("Coin3"))
        {
            coins += 3;

            coinText.text = "Монеты: " + coins;

            audioSource.PlayOneShot(coinSound);

            Destroy(coin.gameObject);
        }
    }
}