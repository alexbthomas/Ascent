using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentEventSystem : MonoBehaviour
{
    private static PersistentEventSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this event system persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate event systems
        }
    }
}
