
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject settingsPanel;
    public AudioSource bgSound;
    public GameObject levelPanel;

    public void OpenLevels()
    {
        levelPanel.SetActive(true);
    }

    public void CloseLevels()
    {
        levelPanel.SetActive(false);
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Forest_Level1");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SetVolume(float value)
    {
        bgSound.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
