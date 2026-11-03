using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ProfileSettings : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_Dropdown colorDropdown;
    public Toggle fullscreenToggle;
    public Toggle musicToggle;
    public Slider volumeSlider;
    public AudioSource bgSound;

    // pull everything saved back out on start
    void Start()
    {
        nameField.text = PlayerPrefs.GetString("PlayerName", "");
        colorDropdown.value = PlayerPrefs.GetInt("PlayerColor", 0);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("Music", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }

    // the name input field
    public void SaveName(string value)
    {
        PlayerPrefs.SetString("PlayerName", value);
        PlayerPrefs.Save();
    }

    // the colour dropdown
    public void SaveColor(int index)
    {
        PlayerPrefs.SetInt("PlayerColor", index);
    }

    // the Fullscreen checkbox
    public void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
    }

    // the Music checkbox
    public void SetMusic(bool isOn)
    {
        bgSound.mute = !isOn;
        PlayerPrefs.SetInt("Music", isOn ? 1 : 0);
    }

    // the Reset button
    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main menu");
    }
    public void SetVolume(float value)
    {
        bgSound.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

}