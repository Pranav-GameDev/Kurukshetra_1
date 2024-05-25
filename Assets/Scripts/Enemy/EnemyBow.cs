using UnityEngine;

public class EnemyBow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shotPoint;
    public float maxForce = 300f;
    public float minForce = 100f;
    private Vector3 playerPosition;
    private float launchForce;
    private float rotationAngle;

    // Reference to the EnemyMovement script
    public EnemyMovement enemyMovement;

    // Reference to the player
    public Transform player;

    // Reference to the Pause_Menu GameObject
    public GameObject pauseMenu;

    // Reference to other UI elements
    public GameObject uiElement1;
    public GameObject uiElement2;

    // Variable to control shooting
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        // Update player position
        if (player != null)
        {
            playerPosition = player.position;
        }

        // Check if any UI elements are active
        if ((pauseMenu != null && pauseMenu.activeSelf) || (uiElement1 != null && uiElement1.activeSelf) || (uiElement2 != null && uiElement2.activeSelf))
        {
            // If any UI elements are active, stop shooting
            canShoot = false;
        }
        else
        {
            // If no UI elements are active, allow shooting
            canShoot = true;

            // Start shooting if not already shooting and allowed to shoot
            if (canShoot && enemyMovement.isReached && player != null && !IsInvoking("ShootArrow"))
            {
                InvokeRepeating("ShootArrow", 0f, 2f); // Shoot an arrow every 2 seconds
            }
        }
    }

    void ShootArrow()
    {
        if (canShoot && enemyMovement.isReached && player != null)
        {
            // Fetch launch force and add/subtract random value
            launchForce = Random.Range(minForce, maxForce);
            float forceVariation = Random.Range(-5f, 5f);
            launchForce += forceVariation;

            // Fetch rotation angle towards player and add/subtract random value
            rotationAngle = Mathf.Atan2(playerPosition.y - transform.position.y, playerPosition.x - transform.position.x) * Mathf.Rad2Deg;
            float angleVariation = Random.Range(-6f, 6f);
            rotationAngle += angleVariation;

            // Gradually rotate the bow
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0f, 0f, rotationAngle);
            float rotationSpeed = 10f;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            }

            // Spawn arrow and shoot
            GameObject newArrow = Instantiate(arrowPrefab, shotPoint.position, Quaternion.identity);
            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.velocity = (playerPosition - shotPoint.position).normalized * launchForce;
        }
    }
}
