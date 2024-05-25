using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player_Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shotPoint;
    private Vector3 direction;
    private Vector3 initialDirection; // Initial direction of the bow

    public GameObject point;
    private GameObject[] points;
    public int numberofpoints;
    public float spacebetpoints;

    public float shootDelay = 1f; // Delay in seconds before shooting again
    public int maxArrows = 20; // Maximum number of arrows
    public int currentArrows; // Current number of arrows
    public bool canShoot = true; // Flag to control shooting

    private bool isRotating = true; // Flag to control bow rotation
    public float ForceAfterClick; // Distance mouse travels after clicking to releasing the click
    private float launchForce; // Launch force of the arrow

    public float maxRotationZ = 90f; // Maximum rotation angle on Z-axis
    public float minRotationZ = -55f; // Minimum rotation angle on Z-axis

    public float rotationSpeed = 100f; // Speed of rotation when cursor is over UI

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    private bool recordClicksAndSpawnArrows = true; // Flag to control recording mouse clicks and arrow spawning

    private void Start()
    {
        points = new GameObject[numberofpoints];
        for (int i = 0; i < numberofpoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
            points[i].SetActive(false); // Deactivate points initially
        }

        currentArrows = maxArrows; // Set initial number of arrows

        // Get GraphicRaycaster and EventSystem components
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (isRotating)
        {
            if (!IsPointerOverUI())
            {
                // Calculate direction from bow to mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.forward, 0f);
                float distance;
                Vector3 bowPosition = transform.position;

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 mousePosition = ray.GetPoint(distance);
                    direction = mousePosition - bowPosition;
                    direction.z = 0f; // Ensure the direction is only in the X-Y plane

                    // Rotate the bow to point towards the calculated direction
                    Vector3 targetDir = direction.normalized;
                    float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                    angle = Mathf.Clamp(angle, minRotationZ, maxRotationZ); // Clamp rotation angle
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else
            {
                // Gradually rotate the bow back to default rotation when cursor is over UI
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Stop recording mouse clicks and arrow spawning
                recordClicksAndSpawnArrows = false;
            }
        }
        else
        {
            // Stop recording mouse clicks and arrow spawning
            recordClicksAndSpawnArrows = false;
        }

        // Shoot arrow when mouse button is clicked and shooting is allowed
        if (canShoot && Input.GetMouseButtonDown(0) && currentArrows > 0 && recordClicksAndSpawnArrows)
        {
            // Stop bow rotation and record initial direction
            isRotating = false;
            initialDirection = transform.right;

            // Start recording mouse movement after click
            StartCoroutine(RecordMouseMovement());

            currentArrows--; // Decrease arrow count
        }

        // Update curve visualization after clicking mouse
        if (!canShoot && recordClicksAndSpawnArrows)
        {
            for (int i = 0; i < numberofpoints; i++)
            {
                points[i].transform.position = PointPosition(i * spacebetpoints, launchForce); // Fixed: Added launchForce parameter
            }
        }

        // Check if no more arrows left
        if (currentArrows <= 0)
        {
            canShoot = false; // Prevent shooting
            isRotating = false; // Stop bow rotation
        }

        // Reset recordClicksAndSpawnArrows flag when cursor is out of the UI image
        if (!IsPointerOverUI() && !recordClicksAndSpawnArrows)
        {
            recordClicksAndSpawnArrows = true;
        }
    }

    IEnumerator RecordMouseMovement()
    {
        // Activate point visualizations
        for (int i = 0; i < numberofpoints; i++)
        {
            points[i].SetActive(true);
        }

        Vector3 initialMousePosition = Input.mousePosition;

        while (!Input.GetMouseButtonUp(0) && recordClicksAndSpawnArrows)
        {
            // Calculate distance mouse travels after clicking
            ForceAfterClick = Vector3.Distance(initialMousePosition, Input.mousePosition);

            // Update curve visualization with current launchForce value
            launchForce = Mathf.Max(0, ForceAfterClick / 10f + 10f);
            for (int i = 0; i < numberofpoints; i++)
            {
                points[i].transform.position = PointPosition(i * spacebetpoints, launchForce); // Fixed: Added launchForce parameter
            }

            yield return null;
        }

        // Prevent shooting while waiting
        canShoot = false;

        // Shoot arrow
        Shoot();

        // Wait for the specified delay
        yield return new WaitForSeconds(shootDelay);

        // Deactivate point visualizations
        for (int i = 0; i < numberofpoints; i++)
        {
            points[i].SetActive(false);
        }

        // Allow shooting again
        canShoot = true;

        // Start rotating the bow again
        isRotating = true;
    }

    void Shoot()
    {
        // Instantiate arrow at shot point
        GameObject newArrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);

        // Get Rigidbody component of the arrow
        Rigidbody rb = newArrow.GetComponent<Rigidbody>();

        // Calculate launch velocity
        Vector3 launchVelocity = initialDirection * launchForce;

        // Set velocity to launch the arrow
        rb.velocity = launchVelocity;
    }

    Vector3 PointPosition(float t, float launchForce)
    {
        Vector3 position = shotPoint.position + initialDirection.normalized * launchForce * t + 0.5f * Physics.gravity * (t * t);
        return position;
    }

    bool IsPointerOverUI()
    {
        if (eventSystem == null || raycaster == null)
            return false;

        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        return results.Count > 0;
    }
}
