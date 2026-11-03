
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool survivalMode = false;

    private Animator animator;
    private Player2D player;
    private bool survived = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = FindFirstObjectByType<Player2D>();
        animator.SetBool("Open", false);
    }

    void Update()
    {
        if (survivalMode)
        {
            animator.SetBool("Open", survived);
        }
        else
        {
            if (player == null)
                return;

            bool shouldOpen = player.HasAllCoins();
            animator.SetBool("Open", shouldOpen);
        }
    }

    public void OpenFromTimer()
    {
        survived = true;
        animator.SetBool("Open", true);
    }
}
