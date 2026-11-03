 using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Берём сохранённую громкость
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        audioSource.volume = savedVolume;

        // Проверяем, включена ли музыка
        bool musicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        audioSource.mute = !musicOn;
    }
}