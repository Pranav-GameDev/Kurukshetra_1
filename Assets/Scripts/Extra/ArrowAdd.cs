using UnityEngine;

public class ArrowAdd : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject floatingArrow; // Reference to the floating arrow GameObject

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the floating arrow
        if (other.gameObject == player)
        {
            // Add arrows to the player's arrow count
            Player_Bow playerBow = player.GetComponent<Player_Bow>();
            if (playerBow != null)
            {
                playerBow.currentArrows += 3;
            }

            // Disable the floating arrow
            floatingArrow.SetActive(false);
        }
    }
}