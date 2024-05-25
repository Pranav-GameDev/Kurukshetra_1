using UnityEngine;

public class Score : MonoBehaviour
{
    public Transform startPoint; // Reference to the start point
    public Transform endPoint; // Reference to the end point
    public Transform player; // Reference to the player

    public float scorePercentage; // Percentage of distance covered by the player

    private Vector3 playerStartPosition; // Initial position of the player
    private float totalDistance; // Total distance between start and end points

    void Start()
    {
        // Calculate the total distance between start and end points
        totalDistance = Vector3.Distance(startPoint.position, endPoint.position);

        // Store the initial position of the player
        playerStartPosition = player.position;
    }

    void Update()
    {
        // Calculate the distance covered by the player
        float distanceCovered = Vector3.Distance(playerStartPosition, player.position);

        // Calculate the percentage of distance covered
        scorePercentage = (distanceCovered / totalDistance) * 100f;
    }
}