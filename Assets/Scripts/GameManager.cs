using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void Win()
    {
        Debug.Log("You Win!");
        audioManager.playSFX(audioManager.win);

    }

    public void Lose()
    {
        Debug.Log("Game Over!");

    }
}