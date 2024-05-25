using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initialSpeed = 2f; // Initial movement speed
    public float maxSpeed = 6f; // Maximum movement speed
    public float accelerationTime = 2f; // Time taken to reach max speed
    public float decelerationTime = 2f; // Time taken to stop completely

    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float targetSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            targetSpeed = maxSpeed;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            targetSpeed = -maxSpeed;
        }
        else
        {
            targetSpeed = 0;
        }
    }

    void FixedUpdate()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, maxSpeed * Time.fixedDeltaTime * (targetSpeed == 0 ? 1 : accelerationTime));

        rb.velocity = new Vector3(currentSpeed, rb.velocity.y, 0);
    }
}