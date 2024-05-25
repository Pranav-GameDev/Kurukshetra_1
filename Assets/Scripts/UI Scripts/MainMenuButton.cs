using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public Button mainMenuButton; // Reference to the Unity UI button

    void Start()
    {
        // Add a listener to the button click event
        mainMenuButton.onClick.AddListener(LoadMainUI);
    }

    // Method to load the "Main UI" scene
    void LoadMainUI()
    {
        SceneManager.LoadScene("Main UI");
    }
}