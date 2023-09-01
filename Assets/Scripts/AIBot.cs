using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform puck;
    public Transform player;
    public Vector2 goalPosition;
    public Vector2 fixedPosition;  // The fixed position for the bot

    public float maxSpeed = 5.0f;  // Adjust this value as needed
    public float reactionTime = 0.2f;  // Adjust this value as needed

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Calculate the AI's target position based on the predicted puck trajectory
        Vector2 targetPosition = PredictPuckPosition();

        // Apply boundary constraints to the target position
        targetPosition.x = Mathf.Clamp(targetPosition.x, 0.0f, 8.8f);

        if (rb.position.x < puck.position.x)
        {
            Vector2 closestPointAbove = new Vector2(8.5f, 5.0f);
            Vector2 closestPointBelow = new Vector2(8.5f, -5.0f);

            if (Vector2.Distance(rb.position, closestPointAbove) < Vector2.Distance(rb.position, closestPointBelow))
            {
                targetPosition = closestPointAbove;
            }
            else
            {
                targetPosition = closestPointBelow;
            }
        }

        // Move towards the target position
        
            Vector2 moveDirection = (targetPosition - rb.position).normalized;
            Vector2 moveVelocity = moveDirection * maxSpeed;
            rb.velocity = moveVelocity;
        
    }


    private Vector2 PredictPuckPosition()
    {
        Vector2 puckDirection = (Vector2)puck.position - rb.position;
        Vector2 playerDirection = (Vector2)player.position - (Vector2)puck.position;
        float timeToReachPuck = puckDirection.magnitude / maxSpeed;
        Vector2 predictedPuckPosition = (Vector2)puck.position + puck.GetComponent<Rigidbody2D>().velocity * timeToReachPuck;

        if (playerDirection.magnitude < puckDirection.magnitude)
        {
            float timeToReachPlayer = 0.5f*playerDirection.magnitude / maxSpeed;
            Vector2 playerFuturePosition = (Vector2)player.position + player.GetComponent<Rigidbody2D>().velocity * timeToReachPlayer;
            float distanceToGoal = Vector2.Distance(predictedPuckPosition, goalPosition);

            // If the puck is heading towards the player and player is closer to the goal, defend the goal
            if (distanceToGoal < 1.5f && playerFuturePosition.y > predictedPuckPosition.y)
            {
                return new Vector2(goalPosition.x, predictedPuckPosition.y);
            }
        }

        // If the puck's velocity is negative and its x coordinate is less than 0, move to the fixed position
        if (puck.GetComponent<Rigidbody2D>().velocity.x < 0 && puck.position.x < 0)
        {
            return fixedPosition;
        }

        // Default behavior: try to intercept the puck
        return predictedPuckPosition;
    }
}
