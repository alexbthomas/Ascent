using UnityEngine;
using UnityEngine.SceneManagement;  // Include the SceneManager namespace

public class MainMenuController : MonoBehaviour
{
    // Function to be called when the "Play" button is clicked
    public void PlayGame()
    {
        Debug.Log("Play button clicked.");
        SceneManager.LoadScene("Level1");  // Load the Level1 scene
    }

    // Function to be called when the "Exit" button is clicked
    public void ExitGame()
    {
        Debug.Log("Exit game requested"); // Console log to confirm the action
        Application.Quit();  // Quit the application
    }
}
