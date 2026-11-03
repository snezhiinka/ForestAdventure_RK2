
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Physics Game");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2_Survival");
    }
}
