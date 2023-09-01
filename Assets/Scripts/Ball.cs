using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform ball;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(ball.position.x>9f || ball.position.x<-9f)
        {
            ball.gameObject.SetActive(false);
        }
        else
        {
            if(!ball.gameObject.activeSelf)
            {
                ball.gameObject.SetActive(true);
            }
        }
    }
}
