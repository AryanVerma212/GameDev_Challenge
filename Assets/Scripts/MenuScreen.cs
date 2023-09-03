using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Game is starting");
    }

    public void ExitGame()
    {
        //Application.Quit();
        Debug.Log("Game is exiting");
    }
}
