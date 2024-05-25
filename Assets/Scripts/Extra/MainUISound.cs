using UnityEngine;
using UnityEngine.UI;

public class MainUISound : MonoBehaviour
{
    // Audio source for UI sound effect
    public AudioSource uiAudioSource;

    // 15 button references
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;

    // Start is called before the first frame update
    void Start()
    {
        // Add button click listeners
        AddButtonClickListeners();
    }

    // Add click listeners to all buttons
    void AddButtonClickListeners()
    {
        button1.onClick.AddListener(() => PlayUISound());
        button2.onClick.AddListener(() => PlayUISound());
        button3.onClick.AddListener(() => PlayUISound());
        button4.onClick.AddListener(() => PlayUISound());
        button5.onClick.AddListener(() => PlayUISound());
        button6.onClick.AddListener(() => PlayUISound());
    }

    // Play UI sound effect
    void PlayUISound()
    {
        if (uiAudioSource != null)
        {
            uiAudioSource.Play();
        }
    }
}