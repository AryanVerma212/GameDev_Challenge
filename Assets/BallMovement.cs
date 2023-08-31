using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool isCapturedByLeftPlayer = false;
    private bool isCapturedByRightPlayer = false;
    public Transform leftPlayer;
    public Transform rightPlayer;
    private Vector3 leftGoalVector=new Vector3(-9f,0,0);
    private Vector3 rightGoalVector = new Vector3(9f, 0, 0);
    private Vector3 goalPointer;
    private Rigidbody2D rb;
    private float ballSpeed = 10f;
    public AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isCapturedByLeftPlayer)
        {
            goalPointer = rightGoalVector-leftPlayer.position;
            goalPointer.Normalize();
            transform.position = leftPlayer.position + goalPointer*0.76f;
        }
        if (isCapturedByRightPlayer)
        {
            goalPointer = leftGoalVector-rightPlayer.position;
            goalPointer.Normalize();
            transform.position = rightPlayer.position + goalPointer*0.76f;
        }
        if(Input.GetKey(KeyCode.Space) && isCapturedByLeftPlayer)
        {
            isCapturedByLeftPlayer = false;
            rb.velocity=goalPointer*ballSpeed;
            audioSource.clip=audioClip;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Keypad0) && isCapturedByRightPlayer)
        {
            isCapturedByRightPlayer = false;
            rb.velocity = goalPointer*ballSpeed;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="LeftPlayer")
        {
           isCapturedByLeftPlayer = true;
        }
        if(collision.gameObject.tag=="RightPlayer")
        {
          isCapturedByRightPlayer = true;
        }
    }
}
