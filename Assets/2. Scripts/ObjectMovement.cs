using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the object's movement
    public float leftDistance = 5f; // Distance the object should move to the left
    public float rightDistance = 5f; // Distance the object should move to the right
    private bool movingRight = true; // Flag to track the direction of movement
    private Vector3 initialPosition; // Initial position of the object

    private void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the target position based on the direction
        Vector3 targetPosition = movingRight ? initialPosition + new Vector3(rightDistance, 0f, 0f) : initialPosition - new Vector3(leftDistance, 0f, 0f);

        // Move the object towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the object has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Reverse the direction when reaching the target position
            movingRight = !movingRight;
        }
    }
}
