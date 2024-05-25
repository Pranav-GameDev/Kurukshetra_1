using UnityEngine;

public class EnemyStayAway : MonoBehaviour
{
    public Transform enemyTransform; // Reference to the enemy's transform
    public Transform playerTransform; // Reference to the player's transform
    public float stayAwayDistance = 20f; // Distance to maintain between player and enemy
    public float forceMagnitude = 20f; // Magnitude of the force to apply
    private Rigidbody enemyRigidbody; // Reference to the enemy's Rigidbody component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component of the enemy
        enemyRigidbody = enemyTransform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between player and enemy
        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);

        // Check if the enemy is too close to the player
        if (distanceToPlayer < stayAwayDistance)
        {
            // Calculate the force direction towards the opposite direction of the player
            float forceDirection = Mathf.Sign(enemyTransform.position.x - playerTransform.position.x);

            // Apply the force to the enemy to stay away from the player
            enemyRigidbody.velocity = new Vector3(forceDirection * forceMagnitude, enemyRigidbody.velocity.y, 0f);
        }
        else
        {
            // If the enemy is not too close to the player, reset its velocity
            enemyRigidbody.velocity = Vector3.zero;
        }
    }
}