using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    void Start()
    {
        // Hide the default cursor
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the current mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert screen coordinates to world coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the position of the cursor GameObject
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}