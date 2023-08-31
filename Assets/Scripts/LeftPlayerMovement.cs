using UnityEngine;
public class LeftPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    public float minX;
    public float maxX;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // Read input for movement
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if(Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        if(Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }

        Vector2 movementInput = new Vector2(horizontalInput, verticalInput);
        MovePlayer(movementInput);
    }

    private void MovePlayer(Vector2 input)
    {
        Vector2 movementVelocity = input.normalized * moveSpeed;
        rb.velocity = movementVelocity;

        // Apply boundary constraints
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }
}
