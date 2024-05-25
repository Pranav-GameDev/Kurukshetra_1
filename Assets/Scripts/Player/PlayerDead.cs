using UnityEngine;
using UnityEngine.UI;

public class PlayerDead : MonoBehaviour
{
    public PlayerLife playerLife; // Reference to the PlayerLife script
    public GameObject player; // Reference to the player GameObject
    public GameObject enemy; // Reference to the enemy GameObject
    public GameObject buttonsUI; // Reference to the Buttons UI GameObject
    public GameObject playerDeadUI; // Reference to the PlayerDeadUI GameObject

    void Update()
    {
        // Check if playerHp is below 5
        if (playerLife.playerHp < 5)
        {
            // Disable player and enemy GameObjects
            player.SetActive(false);
            enemy.SetActive(false);

            // Disable Buttons UI and enable PlayerDeadUI
            buttonsUI.SetActive(false);
            playerDeadUI.SetActive(true);
        }
    }
}