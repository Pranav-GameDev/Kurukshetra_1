using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float playerHp = 2000;
    public float playerHpPercentage;
    public float regenerateHp = 10;
    public float regenerateDelayTime = 5f;

    private float hpToAdd;
    private bool isRegenerating = false;

    private void Start()
    {
        InvokeRepeating("RegeneratePlayerHp", regenerateDelayTime, 3f);
    }

    private void Update()
    {
        playerHpPercentage = Mathf.RoundToInt((playerHp / 2000f) * 100f);

        if (playerHp < 2000 && !isRegenerating)
        {
            StartRegeneration();
        }
    }

    private void RegeneratePlayerHp()
    {
        if (playerHp < 2000)
        {
            playerHp += hpToAdd;
            if (playerHp > 2000)
            {
                playerHp = 2000;
            }
        }
    }

    private void StartRegeneration()
    {
        isRegenerating = true;
        hpToAdd = regenerateHp / 10;
        InvokeRepeating("GraduallyRegeneratePlayerHp", 0f, 0.3f);
    }

    private void GraduallyRegeneratePlayerHp()
    {
        playerHp += hpToAdd;
        if (playerHp >= 2000)
        {
            playerHp = 2000;
            isRegenerating = false;
            CancelInvoke("GraduallyRegeneratePlayerHp");
        }
        
        if (playerHp <= 0)
        {
            playerHp = 0;
            isRegenerating = false;
            CancelInvoke("GraduallyRegeneratePlayerHp");
        }
    }
}