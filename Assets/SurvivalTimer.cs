
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    public float goal = 60f;
    public DoorController door;

    float t;
    bool won;

    void Update()
    {
        if (won) return;

        t += Time.deltaTime;

        if (t >= goal)
        {
            won = true;
            Debug.Log("YOU WIN!");

            if (door != null)
            {
                door.OpenFromTimer();
            }
        }
    }
}
