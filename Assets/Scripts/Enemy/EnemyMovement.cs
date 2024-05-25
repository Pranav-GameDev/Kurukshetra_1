using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isReached = false; // Indicates if the enemy has reached the desired distance from the player
    public float DistanceFromPlayer = 10f; // Distance the enemy should maintain from the player
    public Transform playerTransform; // Reference to the player's transform
    public float speed = 5f; // Speed of the enemy's movement
    private bool isInRange = false; // Indicates if the enemy is within the range of DistanceFromPlayer + 5
    public float targetXPosition;
    private Rigidbody rb; // Reference to the enemy's Rigidbody component

    private float randomForceMagnitude = 0f; // Magnitude of random force
    private float randomForceDirection = 0f; // Direction of random force

    private float randomForceTimer = 0f; // Timer for changing random force

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component of the enemy
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired X position for the enemy
        targetXPosition = playerTransform.position.x + DistanceFromPlayer;

        if (isReached)
        {
            // Apply random force to move the enemy within +10 to -10 units from targetXPosition
            if (Time.time > randomForceTimer)
            {
                randomForceMagnitude = Random.Range(5f, 12f);
                randomForceDirection = Random.Range(-1f, 1f);
                randomForceTimer = Time.time + Random.Range(1f, 3f); // Change random force after a random time interval
            }

            // Apply the random force to the enemy
            float randomForceX = randomForceDirection * randomForceMagnitude;
            rb.velocity = new Vector3(randomForceX, rb.velocity.y, 0f);

            // Check if the enemy moves beyond the desired range
            if (transform.position.x > targetXPosition + 10f || transform.position.x < targetXPosition - 10f)
            {
                // Reset isReached after a delay of 3 seconds
                Invoke("ResetIsReached", 3f);
            }
        }
        else
        {
            // Calculate the force direction based on the difference between current and target X position
            float forceDirection = Mathf.Sign(targetXPosition - transform.position.x);

            // Calculate the magnitude of the force based on the distance to the target
            float forceMagnitude = Mathf.Clamp01(Mathf.Abs(targetXPosition - transform.position.x) / 10f) * speed;

            // Apply the force to the enemy
            rb.velocity = new Vector3(forceDirection * forceMagnitude, rb.velocity.y, 0f);

            // Check if the enemy is within the range of DistanceFromPlayer + 5
            if (!isInRange && Mathf.Abs(transform.position.x - targetXPosition) <= 5f)
            {
                isInRange = true;
            }

            // Check if the enemy has reached the desired distance from the player
            if (!isReached && isInRange)
            {
                isReached = true;
            }
        }
    }

    // Reset isReached after a delay
    void ResetIsReached()
    {
        isReached = false;
    }
}
