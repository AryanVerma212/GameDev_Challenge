using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadGame(int input)
    {
        IDKWhy.difficulty = input;
        SceneManager.LoadScene("StartCounter");
    }
}
