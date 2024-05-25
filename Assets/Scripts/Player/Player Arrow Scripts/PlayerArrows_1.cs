using System.Collections;
using UnityEngine;

public class PlayerArrow_1 : MonoBehaviour
{
    public float damageRate = 10f; // Rate at which damage is applied
    public float decreaseRate = 5f; // Rate at which HP decreases

    private bool isColliding = false; // Flag to track collision state
    private EnemyLife enemyLife; // Reference to the enemy's life script

    void Start()
    {
        // Find the target object with the EnemyLife script
        GameObject targetObject = GameObject.Find("Enemy_1");

        if (targetObject != null)
        {
            // Get reference to enemy's life script
            enemyLife = targetObject.GetComponent<EnemyLife>();
        }
        else
        {
            Debug.LogError("Target object not found!");
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
        if (other.gameObject == GameObject.Find("Enemy Collider Box"))
        {
            // Disable enemy regeneration temporarily
            if (enemyLife != null)
            {
                enemyLife.isRegenerating = false;
                StartCoroutine(EnableRegenerationAfterDelay(5f)); // Enable regeneration after 5 seconds
            }

            // Gradually decrease enemy's HP if colliding with target
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    System.Collections.IEnumerator ApplyDamageOverTime()
    {
        isColliding = true;

        float initialHp = enemyLife.enemyHp;
        float targetHp = initialHp - damageRate;

        while (enemyLife.enemyHp > targetHp)
        {
            // Decrease enemy's HP gradually
            enemyLife.enemyHp -= decreaseRate * Time.deltaTime;

            // Check if enemy's HP has reached 0
            if (enemyLife.enemyHp <= 0)
            {
                enemyLife.enemyHp = 0;
                break; // Exit the loop if HP reaches 0
            }

            yield return null;
        }

        // Disable the arrow after applying damage
        gameObject.SetActive(false);
    }

    IEnumerator EnableRegenerationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Re-enable enemy regeneration
        if (enemyLife != null)
        {
            enemyLife.isRegenerating = true;
        }
    }

    void SpecialAbility()
    {
        // Add special ability logic here
    }
}
