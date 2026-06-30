using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void OpenLevel(int levelId)
    {
        String levelName = "Level_" + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1.2f;
        SceneManager.LoadScene("MainMenu");
    }
}
