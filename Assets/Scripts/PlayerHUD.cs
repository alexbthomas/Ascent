using UnityEngine;
using TMPro; // Import TextMeshPro
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI sparkCountText; // TextMeshPro for spark count
    public Image sparkIcon; // Reference to the spark icon
    public Image cloakIcon; // Reference to the cloak icon
    public Image empIcon; // Reference to the EMP icon
    
    private PlayerResources playerResources; // Reference to player's resources
    private PlayerAbilityManager abilityManager; // Reference to player's ability manager

    void Start()
    {
        // Get references to required components
        playerResources = FindObjectOfType<PlayerResources>();
        abilityManager = FindObjectOfType<PlayerAbilityManager>();
    }

    void Update()
    {
        // Update the spark count display
        int currentSpark = playerResources.currentSpark; // Assuming GetSpark() returns the spark count
        sparkCountText.text = currentSpark.ToString(); // Display the spark count as text with TextMeshPro

        // Change spark icon's color to indicate usability (e.g., light it up)
        sparkIcon.color = currentSpark > 0 ? Color.white : Color.gray; // If player has spark, light up the icon

        // Update cloak icon based on usability
        if (abilityManager.cloakUnlocked && currentSpark >= 20 && cloakIcon != null) // If 20 spark is needed
        {
            cloakIcon.color = Color.white; // Indicate it's usable
        }
        else
        {
            cloakIcon.color = Color.gray; // Not usable
        }

        // Update EMP icon based on usability
        if (abilityManager.empUnlocked && currentSpark >= 30 && empIcon != null) // If 30 spark is needed
        {
            empIcon.color = Color.white; // Indicate it's usable
        }
        else
        {
            empIcon.color = Color.gray; // Not usable
        }
    }
}
