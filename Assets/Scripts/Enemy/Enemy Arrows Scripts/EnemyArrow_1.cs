using UnityEngine;

public class EnemyArrow_1 : MonoBehaviour
{
    public float damageRate = 10f; // Rate at which damage is applied
    public float decreaseRate = 5f; // Rate at which HP decreases
    public string collisionObjectName; // Name of the collision object

    private bool isColliding = false; // Flag to track collision state
    private PlayerLife playerLife; // Reference to the player's life script
    private GameObject collisionObject; // Reference to the collision object

    void Start()
    {
        // Find the target object with the PlayerLife script
        GameObject targetObject = GameObject.Find("Player");

        if (targetObject != null)
        {
            // Get reference to player's life script
            playerLife = targetObject.GetComponent<PlayerLife>();
        }
        else
        {
            Debug.LogError("Target object not found!");
        }

        // Find the collision object by name
        collisionObject = GameObject.Find("Player Collider Box");

        if (collisionObject == null)
        {
            Debug.LogError("Collision object not found!");
        }
    }

    void Update()
    {
        if (!isColliding)
        {
            // Perform special ability if not colliding
            SpecialAbility();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if colliding with target object
        if (other.gameObject == collisionObject)
        {
            // Gradually decrease player's HP if colliding with target
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    System.Collections.IEnumerator ApplyDamageOverTime()
    {
        isColliding = true;

        float initialHp = playerLife.playerHp;
        float targetHp = Mathf.Max(initialHp - damageRate, 0); // Ensure targetHp doesn't go below 0

        while (playerLife.playerHp > targetHp)
        {
            // Decrease player's HP gradually
            playerLife.playerHp -= decreaseRate * Time.deltaTime;
            
            // Check if player's HP has reached 0
            if (playerLife.playerHp <= 0)
            {
                playerLife.playerHp = 0;
                break; // Exit the loop if HP reaches 0
            }
            
            yield return null;
        }

        // Disable the arrow after applying damage
        gameObject.SetActive(false);
    }

    void SpecialAbility()
    {
        // Add special ability logic here
    }
}
