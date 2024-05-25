using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float enemyHp = 500;
    public float enemyHpPercentage;
    public float regenerateHp = 10;
    public float regenerateDelayTime = 5f;

    private float hpToAdd;
    public bool isRegenerating = false;

    private void Start()
    {
        InvokeRepeating("RegenerateEnemyHp", regenerateDelayTime, 3f);
    }

    private void Update()
    {
        enemyHpPercentage = Mathf.RoundToInt((enemyHp / 500f) * 100f);

        if (enemyHp < 500 && !isRegenerating)
        {
            StartRegeneration();
        }
    }

    private void RegenerateEnemyHp()
    {
        if (enemyHp < 500)
        {
            enemyHp += hpToAdd;
            if (enemyHp > 500)
            {
                enemyHp = 500;
            }
        }
    }

    private void StartRegeneration()
    {
        isRegenerating = true;
        hpToAdd = regenerateHp / 10;
        InvokeRepeating("GraduallyRegenerateEnemyHp", 0f, 0.3f);
    }

    private void GraduallyRegenerateEnemyHp()
    {
        enemyHp += hpToAdd;
        if (enemyHp >= 500)
        {
            enemyHp = 500;
            isRegenerating = false;
            CancelInvoke("GraduallyRegenerateEnemyHp");
        }
        
        if (enemyHp == 0)
        {
            enemyHp = 0;
            isRegenerating = false;
            CancelInvoke("GraduallyRegenerateEnemyHp");
        }
    }
}