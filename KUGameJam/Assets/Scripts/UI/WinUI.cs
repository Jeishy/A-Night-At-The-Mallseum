using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        // Load the main menu
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }
}
