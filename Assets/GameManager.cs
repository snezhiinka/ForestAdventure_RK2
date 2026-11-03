using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Сколько времени даётся игроку
    public float timeLeft = 30f;

    // Сколько монет нужно собрать для победы
    public int targetScore = 15;

    // Номер текущего уровня
    public int currentLevel = 1;

    // UI
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;

    public GameObject startPanel;
    public GameObject endPanel;

    // Игрок
    public GameObject player;

    private Movement movement;

    // Чтобы EndGame не запускался несколько раз
    private bool gameEnded = false;


    void Start()
    {
        // Получаем Movement с нашего шара
        movement = player.GetComponent<Movement>();

        // Игра начинается сразу
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        // В начале окно победы/проигрыша скрыто
        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        // Запускаем время
        Time.timeScale = 1f;

        // Сразу показываем начальное время
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
    }


    void Update()
    {
        // Если игра уже закончилась — ничего дальше не проверяем
        if (gameEnded)
        {
            return;
        }

        // Уменьшаем таймер
        timeLeft -= Time.deltaTime;

        // Показываем оставшееся время
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);


        // ПОБЕДА:
        // игрок собрал нужное количество монет
        if (movement.Coins >= targetScore)
        {
            EndGame(true);
            return;
        }


        // ПРОИГРЫШ:
        // время закончилось
        if (timeLeft <= 0f)
        {
            EndGame(false);
            return;
        }


        // ПРОИГРЫШ:
        // шар упал с платформы
        if (player.transform.position.y < -2f)
        {
            EndGame(false);
        }
    }


    // Оставляем эту функцию,
    // если где-нибудь ещё есть старая кнопка START
    public void StartGame()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }


    public void EndGame(bool win)
    {
        // Защита от повторного запуска EndGame
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;


        if (win)
        {
            resultText.text = "YOU WIN!";

            // Если это не последний уровень,
            // открываем следующий
            if (currentLevel < 5)
            {
                int nextLevel = currentLevel + 1;

                int levelReached =
                    PlayerPrefs.GetInt("LevelReached", 1);

                // Не уменьшаем уже открытый прогресс
                if (nextLevel > levelReached)
                {
                    PlayerPrefs.SetInt(
                        "LevelReached",
                        nextLevel
                    );

                    PlayerPrefs.Save();
                }
            }
        }
        else
        {
            resultText.text = "YOU LOSE!";
        }


        // Показываем End Panel
        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        // Останавливаем игру
        Time.timeScale = 0f;
    }


    // PLAY AGAIN
    public void PlayAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }


    // MAIN MENU
    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main menu");
    }
}