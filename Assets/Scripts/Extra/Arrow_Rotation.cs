using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    private bool hashit;

    public float tipOffsetDistance = 25f; // Adjust this value as needed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!hashit)
        {
            // Calculate the angle based on the arrow's velocity
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            // Apply rotation to the arrow based on the calculated angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hashit)
        {
            hashit = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            // Move the arrow to the collision point
            transform.position = collision.contacts[0].point;

            // Offset the collision point to match the tip of the arrow
            Vector3 tipOffset = transform.forward * tipOffsetDistance;
            transform.position -= tipOffset;
        }
    }
}