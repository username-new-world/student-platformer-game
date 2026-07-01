using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;
    AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void Win()
    {
        audioManager.playSFX(audioManager.win);
        Time.timeScale = 0f;
        winScreen.SetActive(true);

    }

    public void Lose()
    {

        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);

    }
}