using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public void TryAgainButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
