using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    public static void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
    }

    public void LoadCurrentScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void LoadSecondScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);

    }
    public void LoadDeathScene()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void loadRightLevel()
    {
        FindObjectOfType<GameManager>().loadLevel();
    }

    public static void LoadWinScene()
    {
        SceneManager.LoadScene("Lose Screen");
    }

}
