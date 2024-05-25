using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioController : MonoBehaviour
{
    private bool isMuted = false;

    // Button references
    public Button muteButton;
    public Button unmuteButton;

    // Slider reference for volume control
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to buttons
        muteButton.onClick.AddListener(MuteAudio);
        unmuteButton.onClick.AddListener(UnmuteAudio);

        // Add listener to volume slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Function to mute all audio
    public void MuteAudio()
    {
        isMuted = true;
        SetGlobalVolume(0f); // Mute all audio
        muteButton.interactable = false; // Disable mute button
        unmuteButton.interactable = true; // Enable unmute button
    }

    // Function to unmute all audio
    public void UnmuteAudio()
    {
        isMuted = false;
        SetGlobalVolume(volumeSlider.value); // Set volume to slider value
        muteButton.interactable = true; // Enable mute button
        unmuteButton.interactable = false; // Disable unmute button
    }

    // Function to set volume based on slider value
    public void SetVolume(float volume)
    {
        if (!isMuted)
        {
            SetGlobalVolume(volume); // Set global volume based on slider value
        }
    }

    // Function to set volume of all audio sources
    private void SetGlobalVolume(float volume)
    {
        AudioListener.volume = volume; // Set global volume
        VideoPlayer[] videoPlayers = FindObjectsOfType<VideoPlayer>(); // Find all video players
        foreach (VideoPlayer player in videoPlayers)
        {
            player.SetDirectAudioVolume(0, volume); // Set volume for each video player
        }

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>(); // Find all audio sources
        foreach (AudioSource source in audioSources)
        {
            source.volume = volume; // Set volume for each audio source
        }
    }
}
