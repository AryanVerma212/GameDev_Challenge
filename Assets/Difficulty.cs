using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public int difficultyLevel;
    private void Start()
    {
        difficultyLevel=IDKWhy.difficulty;
    }
}
