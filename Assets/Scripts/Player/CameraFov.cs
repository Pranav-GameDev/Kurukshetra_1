using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraFov : MonoBehaviour
{
    public Transform camFocus; // Target transform when bow can shoot
    public Transform camDefault; // Target transform when bow cannot shoot
    public float moveSpeed = 5f; // Speed at which the camera moves

    public Player_Bow bow;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    void Start()
    {
        bow = FindObjectOfType<Player_Bow>(); // Find the Bow script in the scene

        // Get GraphicRaycaster and EventSystem components
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        // Move camera to camFocus when the bow can shoot and cursor is not over UI
        if (bow.canShoot && Input.GetMouseButton(0) && !IsPointerOverUI())
        {
            MoveCamera(camFocus);
        }

        // Move camera to camDefault when the bow cannot shoot or cursor is over UI
        if (!bow.canShoot || Input.GetMouseButtonUp(0) || IsPointerOverUI())
        {
            MoveCamera(camDefault);
        }
    }

    void MoveCamera(Transform target)
    {
        // Gradually move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    bool IsPointerOverUI()
    {
        if (eventSystem == null || raycaster == null)
            return false;

        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        return results.Count > 0;
    }
}