using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public Button restartButton; // Reference to the restart button

    public GameObject player; // Reference to the player GameObject
    public GameObject enemy; // Reference to the enemy GameObject
    public GameObject enemySpawn; // Reference to the enemy spawn point GameObject
    public GameObject startPoint; // Reference to the start point GameObject
    public float defaultPlayerHp = 2000; // Default player HP value
    public float defaultEnemyHp = 500; // Default enemy HP value
    public int defaultArrowCount = 20; // Default arrow count

    private void Start()
    {
        // Add listener to the restart button
        restartButton.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        // Teleport the player to the start point
        player.transform.position = startPoint.transform.position;

        // Teleport the enemy to the enemy spawn point
        enemy.transform.position = enemySpawn.transform.position;

        // Reset player and enemy HP to default values
        PlayerLife playerLife = player.GetComponent<PlayerLife>();
        if (playerLife != null)
        {
            playerLife.playerHp = defaultPlayerHp;
        }

        EnemyLife enemyLife = enemy.GetComponent<EnemyLife>();
        if (enemyLife != null)
        {
            enemyLife.enemyHp = defaultEnemyHp;
        }

        // Reset arrow count to default value
        Player_Bow playerBow = player.GetComponent<Player_Bow>();
        if (playerBow != null)
        {
            playerBow.currentArrows = defaultArrowCount;
        }
        
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
