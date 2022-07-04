using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    /**
     * Play the game
     * (Load the MainScene)
     */
    public void PlayGame() {
        SceneManager.LoadScene("MainScene");
    }

    /**
     * Quit the Application appropriately
     */
    public void QuitGame() {
        Application.Quit();
    }
}