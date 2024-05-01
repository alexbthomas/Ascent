using UnityEngine;
using UnityEngine.UI; // Required for interacting with UI elements

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the volume slider
    public Toggle muteToggle; // Reference to the mute toggle
    private AudioSource backgroundMusic; // Reference to the background music AudioSource
    private AudioSource detectedSound;
    private AudioSource takedownSound;
    private AudioSource cloakSound;
    private AudioSource EMPSound;

    void Start()
    {
        // Find the BackgroundMusic component in the scene
        BackgroundMusic musicComponent = FindObjectOfType<BackgroundMusic>();
        if (musicComponent != null)
        {
            backgroundMusic = musicComponent.audioSource;
        }

        // Initialize slider value and toggle state
        if (backgroundMusic != null)
        {
            volumeSlider.value = backgroundMusic.volume;
            muteToggle.isOn = backgroundMusic.volume == 0;
        }

        GameObject spottedObject = GameObject.Find("SpottedPlayer");
        if (spottedObject != null)
        {
            detectedSound = spottedObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }

        GameObject takedownObject = GameObject.Find("TakedownPlayer");
        if (takedownObject != null)
        {
            takedownSound = takedownObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }

        GameObject cloakObject = GameObject.Find("CloakPlayer");
        if (cloakObject != null)
        {
            cloakSound = cloakObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }

        GameObject EMPObject = GameObject.Find("EMPPlayer");
        if (EMPObject != null)
        {
            EMPSound = EMPObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }

        // Add listeners to UI components
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
        muteToggle.onValueChanged.AddListener(HandleMuteToggle);
    }

    private void HandleVolumeChange(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
            if (volume == 0)
            {
                muteToggle.isOn = true;
            }
            else if (muteToggle.isOn)
            {
                muteToggle.isOn = false;
            }
        }
    }

    private void HandleMuteToggle(bool isMuted)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = isMuted ? 0 : volumeSlider.value;
            volumeSlider.interactable = !isMuted; // Disable slider interaction when muted
        }
        if (detectedSound != null)
        {
            detectedSound.volume = isMuted ? 0 : volumeSlider.value;
            volumeSlider.interactable = !isMuted; // Disable slider interaction when muted
        }

        if (cloakSound != null)
        {
            cloakSound.volume = isMuted ? 0 : volumeSlider.value;
            volumeSlider.interactable = !isMuted; // Disable slider interaction when muted
        }

        if (takedownSound != null)
        {
            takedownSound.volume = isMuted ? 0 : volumeSlider.value;
            volumeSlider.interactable = !isMuted; // Disable slider interaction when muted
        }

        if (EMPSound != null)
        {
            EMPSound.volume = isMuted ? 0 : volumeSlider.value;
            volumeSlider.interactable = !isMuted; // Disable slider interaction when muted
        }


    }

    void OnDestroy()
    {
        // Remove listeners to avoid memory leaks
        volumeSlider.onValueChanged.RemoveListener(HandleVolumeChange);
        muteToggle.onValueChanged.RemoveListener(HandleMuteToggle);
    }
}
