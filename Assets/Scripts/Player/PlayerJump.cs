using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // Force applied when jumping
    public Transform groundCheck; // Ground check object
    public float groundDistance = 0.2f; // Distance to check for ground
    public LayerMask groundMask; // Layer mask for ground

    private Rigidbody rb;
    private PlayerMovement playerMovement;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {   
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Jump only if the player is grounded
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Disable PlayerMovement script if the player is not grounded
        if (!isGrounded)
        {
            playerMovement.enabled = false;
        }
        else
        {
            // Enable PlayerMovement script when the player is grounded
            playerMovement.enabled = true;
        }
    }
}