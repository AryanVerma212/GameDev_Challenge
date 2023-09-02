using UnityEngine;

 
public class Ball : MonoBehaviour
{
    public Transform ball;
    public AudioSource audioPlay;

    void Update()
    {
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
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CollisionTag")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
 