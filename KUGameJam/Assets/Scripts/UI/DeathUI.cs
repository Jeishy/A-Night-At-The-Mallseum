using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restart game");
        Time.timeScale = 1;
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        // Load the main menu
        SceneManager.LoadScene(0);
    }
}
