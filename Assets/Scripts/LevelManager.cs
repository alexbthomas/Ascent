using UnityEngine;
using UnityEngine.SceneManagement; // To manage scene transitions

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1; // Track the current level

    void Start()
    {
        // Depending on the current level, grant abilities
        UnlockAbilitiesBasedOnLevel();
    }

    void UnlockAbilitiesBasedOnLevel()
    {
        // Access the player's ability manager
        PlayerAbilityManager abilityManager = FindObjectOfType<PlayerAbilityManager>();

        if (abilityManager == null)
        {
            Debug.LogError("PlayerAbilityManager not found.");
            return;
        }

        // Grant abilities based on the current level
        switch (currentLevel)
        {
            case 2:
                abilityManager.UnlockSilentTakedown();
                break;
            case 3:
                abilityManager.UnlockCloak();
                break;
            case 4:
                abilityManager.UnlockEMP();
                break;
            default:
                break;
        }
    }

    public void LoadNextLevel()
    {
        currentLevel++; // Increment the current level
        SceneManager.LoadScene(currentLevel); // Load the next level
    }
}
