using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Loads main scene - Aleksi
    public void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    // Quits Game - aleksi
    public void QuitGame()
    {
        Application.Quit();
    }
}
