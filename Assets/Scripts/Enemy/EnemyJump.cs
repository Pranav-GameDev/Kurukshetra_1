using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public Rigidbody enemyRigidbody; // Reference to the enemy's Rigidbody component
    public float minJumpForce = 10f; // Minimum jump force
    public float maxJumpForce = 15f; // Maximum jump force
    public float minJumpDelay = 3f; // Minimum delay before next jump
    public float maxJumpDelay = 5f; // Maximum delay before next jump

    private float nextJumpTime; // Time for the next jump
    private bool isJumping = false; // Flag to indicate if enemy is currently jumping

    // Update is called once per frame
    void Update()
    {
        // Check if it's time for the enemy to jump again
        if (!isJumping && Time.time > nextJumpTime)
        {
            // Check if the enemy is stuck by comparing its current velocity on X
            if (Mathf.Abs(enemyRigidbody.velocity.x) < 5f)
            {
                // Perform the jump
                Jump();
            }

            // Set the time for the next jump with random delay
            nextJumpTime = Time.time + Random.Range(minJumpDelay, maxJumpDelay);
        }
    }

    // Perform the jump
    void Jump()
    {
        // Set the flag to indicate that enemy is jumping
        isJumping = true;

        // Calculate the jump force
        float jumpForce = Random.Range(minJumpForce, maxJumpForce);

        // Apply the jump force in Y direction
        enemyRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Set a timer to reset the jumping flag after a short delay
        Invoke("ResetJumping", 0.5f);
    }

    // Reset the jumping flag
    void ResetJumping()
    {
        isJumping = false;
    }
}