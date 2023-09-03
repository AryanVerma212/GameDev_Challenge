using UnityEngine;

public class Companion : MonoBehaviour
{
    public Transform puck;
    public Transform player;
    public Vector2 goalPosition;
    public float playerSpeed = 5.0f; // Adjust this value as needed
    private Vector2 targetPosition;
    private Rigidbody2D rb;
    public GameObject difficultyObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-7.5f,0 , 0);
        int difficultyLevel = difficultyObject.GetComponent<Difficulty>().difficultyLevel;
        if (difficultyLevel == 0)
        {
            playerSpeed = 5.5f;
        }
        else if (difficultyLevel == 1)
        {
            playerSpeed = 4f;
        }
        else if (difficultyLevel == 2)
        {
            playerSpeed = 4.5f;
        }
    }
    private void FixedUpdate()
    {
        // Calculate the AI's target position based on the predicted puck trajectory
        if ((new Vector3(goalPosition.x,goalPosition.y,0)-player.position).magnitude > 3)
        {
            targetPosition = goalPosition + (new Vector2(puck.position.x,puck.position.y) - goalPosition).normalized * 2f;
            //targetPosition=new Vector2(Mathf.Clamp(targetPosition.x,8f,9f),Mathf.Clamp(targetPosition.x,-2f,2f));
        }
        else
        {
            targetPosition = PredictPuckPosition();
            targetPosition = targetPosition + (goalPosition - targetPosition).normalized * 0.4f;
            if (rb.position.x > puck.position.x)
            {
                Vector2 closestPointAbove = new Vector2(-8.5f, 5.0f);
                Vector2 closestPointBelow = new Vector2(-8.5f, -5.0f);

                if (Vector2.Distance(puck.position, closestPointAbove) < Vector2.Distance(puck.position, closestPointBelow))
                {
                    targetPosition = closestPointAbove;
                }
                else
                {
                    targetPosition = closestPointBelow;
                }
            }
            Mathf.Clamp(targetPosition.x, -6f, 0);
            // Move towards the target position
        }
        Vector3 moveDirection = (targetPosition - rb.position).normalized;
        Vector3 moveVelocity = moveDirection * playerSpeed;
        rb.velocity = moveVelocity;

    }


    private Vector2 PredictPuckPosition()
    {
        Vector2 puckDirection = (Vector2)puck.position - rb.position;
        float timeToReachPuck = 0.5f * puckDirection.magnitude / playerSpeed;
        Vector2 predictedPuckPosition = (Vector2)puck.position + puck.GetComponent<Rigidbody2D>().velocity * timeToReachPuck;

        // If the puck's velocity is negative and its x coordinate is less than 0, move to the fixed position
        if (puck.GetComponent<Rigidbody2D>().velocity.x < 0 && puck.position.x < 0)
        {
            //return new Vector2(Random.Range(2f,3f),Random.Range(2f,3f));
        }
        if (predictedPuckPosition.x < -9f)
        {
            // Find the intersection point with the x=-8.5 line
            float t = (-9f - puck.position.x) / puck.GetComponent<Rigidbody2D>().velocity.x;
            float yIntersection = puck.position.y + puck.GetComponent<Rigidbody2D>().velocity.y * t;

            // Set the predictedPuckPosition to the intersection point
            predictedPuckPosition = new Vector2(-8.5f, yIntersection);
        }
        if (predictedPuckPosition.x > 0)
        {
            // Find the intersection point with the x=-1 line
            float t = (-1f - puck.position.x) / puck.GetComponent<Rigidbody2D>().velocity.x;
            float yIntersection = puck.position.y + puck.GetComponent<Rigidbody2D>().velocity.y * t;

            // Set the predictedPuckPosition to the intersection point
            predictedPuckPosition = new Vector2(-1f, yIntersection);
        }
        // Default behavior: try to intercept the puck
        return predictedPuckPosition;
    }
}
