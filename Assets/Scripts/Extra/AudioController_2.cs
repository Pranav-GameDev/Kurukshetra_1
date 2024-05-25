using System.Collections;
using UnityEngine;

public class AudioController_2 : MonoBehaviour
{
    // Audio sources for audio1, audio2, running effect, arrow shoot effect, and jumping effect
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource runningEffectAudio;
    public AudioSource arrowShootEffectAudio;
    public AudioSource jumpingEffectAudio;

    // Reference to the EnemyMovement script
    public EnemyMovement enemyMovement;

    // Reference to the Player_Bow script
    public Player_Bow playerBow;

    // Reference to the PlayerJump script
    public PlayerJump playerJump;

    // Reference to the player GameObject
    public GameObject player;

    // Threshold speed for running effect audio
    public float runningEffectSpeedThreshold = 10f;

    private Vector3 previousPlayerPosition;

    // Update is called once per frame
    void Update()
    {
        // Check player's speed
        if (player != null)
        {
            // Calculate player's speed based on position change
            float playerSpeed = (player.transform.position - previousPlayerPosition).magnitude / Time.deltaTime;

            // Check if the player's speed is greater than the threshold for running effect audio
            if (playerSpeed > runningEffectSpeedThreshold)
            {
                // Start playing running effect audio gradually
                StartRunningEffectAudio();
            }
            else
            {
                // Stop playing running effect audio gradually
                StopRunningEffectAudio();
            }

            // Update previous player position for next frame
            previousPlayerPosition = player.transform.position;
        }

        // Check if isReached variable is true in EnemyMovement script
        if (enemyMovement != null && enemyMovement.isReached)
        {
            // Stop playing audio1 and start playing audio2
            StopAudio1();
            PlayAudio2();
        }
        else
        {
            // Stop playing audio2 and start playing audio1
            StopAudio2();
            PlayAudio1();
        }

        // Check if mouse click is released, canShoot is true, and cursor is not over any UI element
        if (Input.GetMouseButtonUp(0) && playerBow.canShoot && !IsPointerOverUI())
        {
            // Play arrow shoot effect audio
            PlayArrowShootEffectAudio();
        }

        // Check if isGrounded variable changes in PlayerJump script
        if (playerJump != null)
        {
            if (!playerJump.isGrounded)
            {
                // Start playing jumping effect audio
                StartJumpingEffectAudio();
            }
            else
            {
                // Stop playing jumping effect audio
                StopJumpingEffectAudio();
            }
        }
    }

    // Function to play audio1
    void PlayAudio1()
    {
        if (audio1 != null && !audio1.isPlaying)
        {
            audio1.Play();
        }
    }

    // Function to stop audio1
    void StopAudio1()
    {
        if (audio1 != null && audio1.isPlaying)
        {
            audio1.Stop();
        }
    }

    // Function to play audio2
    void PlayAudio2()
    {
        if (audio2 != null && !audio2.isPlaying)
        {
            audio2.Play();
        }
    }

    // Function to stop audio2
    void StopAudio2()
    {
        if (audio2 != null && audio2.isPlaying)
        {
            audio2.Stop();
        }
    }

    // Function to start playing running effect audio gradually
    void StartRunningEffectAudio()
    {
        if (runningEffectAudio != null && !runningEffectAudio.isPlaying)
        {
            runningEffectAudio.volume = 0f; // Start with zero volume
            runningEffectAudio.Play();
            StartCoroutine(FadeInAudio(runningEffectAudio, 1f)); // Gradually increase volume to 1
        }
    }

    // Function to stop playing running effect audio gradually
    void StopRunningEffectAudio()
    {
        if (runningEffectAudio != null && runningEffectAudio.isPlaying)
        {
            StartCoroutine(FadeOutAudio(runningEffectAudio, 1f)); // Gradually decrease volume to 0
        }
    }

    // Function to play arrow shoot effect audio
    void PlayArrowShootEffectAudio()
    {
        if (arrowShootEffectAudio != null)
        {
            arrowShootEffectAudio.Play();
        }
    }

    // Function to start playing jumping effect audio
    void StartJumpingEffectAudio()
    {
        if (jumpingEffectAudio != null && !jumpingEffectAudio.isPlaying)
        {
            jumpingEffectAudio.Play();
        }
    }

    // Function to stop playing jumping effect audio
    void StopJumpingEffectAudio()
    {
        if (jumpingEffectAudio != null && jumpingEffectAudio.isPlaying)
        {
            jumpingEffectAudio.Stop();
        }
    }

    // Coroutine to gradually fade in audio volume
    IEnumerator FadeInAudio(AudioSource audioSource, float fadeDuration)
    {
        float currentTime = 0f;
        float startVolume = audioSource.volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, currentTime / fadeDuration);
            yield return null;
        }
    }

    // Coroutine to gradually fade out audio volume
    IEnumerator FadeOutAudio(AudioSource audioSource, float fadeDuration)
    {
        float currentTime = 0f;
        float startVolume = audioSource.volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
    }

    // Function to check if the mouse cursor is over any UI element
    bool IsPointerOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
