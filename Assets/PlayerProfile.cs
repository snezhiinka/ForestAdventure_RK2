using UnityEngine;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public Color[] colors = { Color.red, Color.blue, Color.green };

    void Start()
    {
        // Показываем имя игрока
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        nameText.text = playerName;

        // Получаем сохранённый цвет
        int colorIndex = PlayerPrefs.GetInt("PlayerColor", 0);

        // Красим шар
        GetComponent<Renderer>().material.color = colors[colorIndex];
    }
}