
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2D : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject bossBar;
    public GameObject gameHUD;
    public string nextLevelName;
    public GameObject pausePanel;

    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale = 1f;

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

    }

    public void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (gameHUD != null)
            gameHUD.SetActive(false);

        if (bossBar != null)
            bossBar.SetActive(false);

        if (!string.IsNullOrWhiteSpace(nextLevelName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (gameHUD != null)
            gameHUD.SetActive(false);

        if (bossBar != null)
            bossBar.SetActive(false);

        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }


    public void PauseGame()
    {
        if (gameEnded || pausePanel == null) return;

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }
}
