using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public Transform ball;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    private int leftScore;
    private int rightScore;
    public GameObject panel;
    bool scoreChanged = false;
    void Start()
    {
        leftScore = 0;
        rightScore = 0;
        scoreText.text = leftScore + " - " + rightScore;
    }
    void Update()
    {
        if(ball.position.x < -10)
        {
            rightScore++;
            scoreChanged = true;
        }
        else if(ball.position.x > 10)
        {

            leftScore++;
            scoreChanged = true;
        }
        if(scoreChanged)
        {
            scoreText.text = leftScore + " - " + rightScore;
            scoreChanged = false;
            if(leftScore == 5)
            {
                Debug.Log("LEFT PLAYER WINS!");
                panel.SetActive(true);
                winText.text = "Left Player Wins!";
            }
            else if(rightScore == 5)
            {
                Debug.Log("RIGHT PLAYER WINS!");
                panel.SetActive(true);
                winText.text = "Right Player Wins!";
            }
            Reset();
        }
    }
    private void Reset()
    {
        ball.position = new Vector3(0, 0, 0);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(0.1f, 0.2f), Random.Range(0.3f, 0.4f), 0);
    }
}
