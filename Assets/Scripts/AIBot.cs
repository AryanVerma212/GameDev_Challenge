using UnityEngine;

public class AIBot : MonoBehaviour
{
    public Transform puck;
    public Transform otherAIBot;
    public Vector2 goalPosition;
    public float playerSpeed = 5.0f; // Adjust this value as needed
    private Vector2 targetPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(Random.Range(8f, 9f), Random.Range(-2f, 2f), 0f);
    }
    private void FixedUpdate()
    {
        // Calculate the AI's target position based on the predicted puck trajectory
        if ((puck.position - transform.position).magnitude > (puck.position - otherAIBot.position).magnitude)
        {
            targetPosition = goalPosition+(new Vector2(puck.position.x,puck.position.y)-goalPosition).normalized*2f;
            //targetPosition=new Vector2(Mathf.Clamp(targetPosition.x,8f,9f),Mathf.Clamp(targetPosition.x,-2f,2f));
        }
        else
        {
            targetPosition = PredictPuckPosition();
            targetPosition = targetPosition + (goalPosition - targetPosition).normalized * 0.4f;
            if (rb.position.x < puck.position.x)
            {
                Vector2 closestPointAbove = new Vector2(8.5f, 5.0f);
                Vector2 closestPointBelow = new Vector2(8.5f, -5.0f);

                if (Vector2.Distance(puck.position, closestPointAbove) < Vector2.Distance(puck.position, closestPointBelow))
                {
                    targetPosition = closestPointAbove;
                }
                else
                {
                    targetPosition = closestPointBelow;
                }
            }
            Mathf.Clamp(targetPosition.x, 0, 6f);
            // Move towards the target position
        }
            Vector2 moveDirection = (targetPosition - rb.position).normalized;
            Vector2 moveVelocity = moveDirection * playerSpeed;
            rb.velocity = moveVelocity;
        
    }


    private Vector2 PredictPuckPosition()
    {
        Vector2 puckDirection = (Vector2)puck.position - rb.position;
        float timeToReachPuck = 0.5f*puckDirection.magnitude / playerSpeed;
        Vector2 predictedPuckPosition = (Vector2)puck.position + puck.GetComponent<Rigidbody2D>().velocity * timeToReachPuck;

        // If the puck's velocity is negative and its x coordinate is less than 0, move to the fixed position
        if (puck.GetComponent<Rigidbody2D>().velocity.x < 0 && puck.position.x < 0)
        {
            //return new Vector2(Random.Range(2f,3f),Random.Range(2f,3f));
        }
        if (predictedPuckPosition.x > 9f)
        {
            // Find the intersection point with the x=8.5 line
            float t = (9f - puck.position.x) / puck.GetComponent<Rigidbody2D>().velocity.x;
            float yIntersection = puck.position.y + puck.GetComponent<Rigidbody2D>().velocity.y * t;

            // Set the predictedPuckPosition to the intersection point
            predictedPuckPosition = new Vector2(8.5f, yIntersection);
        }
        if (predictedPuckPosition.x < 0)
        {
            // Find the intersection point with the x=1 line
            float t = (1f - puck.position.x) / puck.GetComponent<Rigidbody2D>().velocity.x;
            float yIntersection = puck.position.y + puck.GetComponent<Rigidbody2D>().velocity.y * t;

            // Set the predictedPuckPosition to the intersection point
            predictedPuckPosition = new Vector2(1f, yIntersection);
        }
        // Default behavior: try to intercept the puck
        return predictedPuckPosition;
    }
}
