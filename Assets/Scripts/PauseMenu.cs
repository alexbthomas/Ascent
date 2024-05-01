using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu Panel
    public GameObject audioMenuUI; // Reference to the Audio Menu Panel
    public GameObject instructionsMenuUI; // Reference to the Instructions Menu Panel
    public GameObject controlsMenuUI; // Reference to the Controls Menu Panel
    private bool isPaused = false; // Flag to track the pause state
    private BackgroundMusic backgroundMusic; // Reference to the BackgroundMusic script

    void Start()
    {
        pauseMenuUI.SetActive(false); // Ensure the pause menu is hidden at the start
        audioMenuUI.SetActive(false); // Hide Audio Menu
        instructionsMenuUI.SetActive(false); // Hide Instructions Menu
        controlsMenuUI.SetActive(false); // Hide Controls Menu

        backgroundMusic = FindObjectOfType<BackgroundMusic>(); // Find the BackgroundMusic instance
        if (!backgroundMusic)
        {
            Debug.LogWarning("BackgroundMusic instance not found. Make sure it's in the scene and has the BackgroundMusic script attached.");
        }
    }

    void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if any specific menus are active
            if (IsAnyMenuActive())
            {
                // If we are in any specific menus, go back to the main pause menu
                BackToPauseMenu();
            }
            else
            {
                // If no specific menus are active, toggle the pause state
                TogglePause();
            }
        }
    }

    // Method to check if any of the additional menus are active
    private bool IsAnyMenuActive()
    {
        return audioMenuUI.activeSelf || instructionsMenuUI.activeSelf || controlsMenuUI.activeSelf;
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        audioMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        Time.timeScale = isPaused ? 0f : 1f;

        // Manage cursor state and background music volume
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        // if (backgroundMusic && backgroundMusic.audioSource)
        // {
        //     backgroundMusic.audioSource.volume = isPaused ? 0.01f : 0.05f; // Adjust volume
        // }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume time before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current level
        TogglePause(); // Ensure the pause menu is hidden and game is unpaused
    }

    public void SelectLevel(string levelName)
    {
        Time.timeScale = 1f; // Resume time before changing levels
        SceneManager.LoadScene(levelName); // Load the specified level
        TogglePause(); // Ensure the pause menu is hidden and game is unpaused
    }

    // Method to show the Audio Menu
    public void ShowAudioMenu()
    {
        pauseMenuUI.SetActive(false);
        audioMenuUI.SetActive(true);
    }

    // Method to show the Instructions Menu
    public void ShowInstructionsMenu()
    {
        pauseMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(true);
    }

    // Method to show the Controls Menu
    public void ShowControlsMenu()
    {
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }

    // Method to go back to the Pause Menu from any of the additional menus
    public void BackToPauseMenu()
    {
        audioMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset Time.timeScale to normal
        SceneManager.LoadScene("MainScreen"); // Assuming your main menu scene is named "MainMenu"
    }
}
