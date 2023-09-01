using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform ball;

    void Update()
    {
        Debug.Log(ball.position.x);
        if (ball.position.x > 15f || ball.position.x < -15f)
        {
            ball.GetComponent<SpriteRenderer>().enabled = false;
            Reset();
            Debug.Log("Ball is inactive");
        }
        else
        {
            if (!ball.GetComponent<SpriteRenderer>().enabled)
            {
                ball.GetComponent<SpriteRenderer>().enabled = true;
                Debug.Log("Ball is active");
            }
        }
    }

    private void Reset()
    {
        ball.position = new Vector3(0, 0, 0);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-0.3f, 0.3f),0);
        Debug.Log(ball.GetComponent<Rigidbody2D>().velocity);
        Debug.Log("Ball is reset");
    }
}
