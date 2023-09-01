using UnityEngine;



public class Ball : MonoBehaviour
{
    public Transform ball;
    public GameObject AIBot1;
    public GameObject AIBot2;
    void Update()
    {
        Debug.Log(ball.position.x);
        if (ball.position.x > 15f || ball.position.x < -15f)
        {
            ball.GetComponent<SpriteRenderer>().enabled = false;
            Reset();
        }
        else
        {
            if (!ball.GetComponent<SpriteRenderer>().enabled)
            {
                ball.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void Reset()
    {
        ball.position = new Vector3(0, 0, 0);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-0.3f, 0.3f),0);
        Debug.Log(ball.GetComponent<Rigidbody2D>().velocity);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        AIBot1.GetComponent<AIBot1>().CalculateTarget();
        AIBot2.GetComponent<AIBot2>().CalculateTarget();
    }
}
