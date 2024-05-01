using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance; // Singleton pattern to prevent multiple instances
    [HideInInspector]
    public AudioSource audioSource; // Public for access from PauseMenu but hidden in inspector

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the instance if not already set
            DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed on load
            audioSource = GetComponent<AudioSource>(); // Ensure AudioSource component is attached
        }
        else
        {
            Destroy(gameObject); // If there's already an instance, destroy this one to avoid duplicates
        }
    }
}
