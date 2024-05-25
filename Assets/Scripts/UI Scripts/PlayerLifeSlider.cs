using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSlider : MonoBehaviour
{
    public Slider slider;
    public PlayerLife playerLife;

    private void Start()
    {
        // Link the slider component
        slider = GetComponent<Slider>();

        // Find the playerLife script in the scene
        playerLife = FindObjectOfType<PlayerLife>();
    }

    private void Update()
    {
        // Update the slider value based on the player's life percentage
        slider.value = playerLife.playerHp / 2000f; // Assuming playerHp ranges from 0 to 2000
    }
}