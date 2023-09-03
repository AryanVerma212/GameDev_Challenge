using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreen : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Game is starting");
        SceneManager.LoadScene("LevelChooser");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
