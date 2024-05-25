using UnityEngine;

public class CameraMovementY : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float cameraSpeed = 5f; // Speed at which the camera follows the player
    public float jumpHeight = 7f; // Height to move the camera when the player jumps
    public float jumpDuration = 0.5f; // Duration of the jump movement

    private Vector3 targetPosition; // Target position for the camera to follow the player
    private bool isGrounded = true; // Flag to indicate if the player is grounded
    private float jumpTimer = 0f; // Timer for the jump movement

    void Update()
    {
        // Follow player on Y-axis
        if (!isGrounded)
        {
            // Move the camera up gradually
            if (transform.position.y < jumpHeight)
            {
                float newY = Mathf.Lerp(transform.position.y, jumpHeight, jumpTimer / jumpDuration);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isGrounded = true; // Set isGrounded to true once the camera reaches the jump height
                jumpTimer = 0f; // Reset jump timer
            }
        }
        else
        {
            // Follow player on Y-axis even when not jumping
            float newY = Mathf.Lerp(transform.position.y, playerTransform.position.y, cameraSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    // Method to trigger the jump movement
    public void TriggerJumpMovement()
    {
        isGrounded = false;
        jumpTimer = 0f; // Reset the jump timer
    }
}