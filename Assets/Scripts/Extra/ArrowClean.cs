using System.Collections;
using UnityEngine;

public class ArrowClean : MonoBehaviour
{
    public float cleanupDelay = 7f; // Delay before cleaning up arrows

    void Start()
    {
        // Start the cleanup coroutine
        StartCoroutine(CleanupArrows());
    }

    IEnumerator CleanupArrows()
    {
        while (true)
        {
            yield return new WaitForSeconds(cleanupDelay);

            // Find all arrow clones in the scene
            GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");

            // Destroy each arrow clone
            foreach (GameObject arrow in arrows)
            {
                Destroy(arrow);
            }
        }
    }
}