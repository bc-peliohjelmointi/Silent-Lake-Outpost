using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    // Can exit back to main menu - Aleksi

    public int mainMenuSceneBuildIndex = 0;

    public void QuitGame()
    {
        SceneManager.LoadScene(mainMenuSceneBuildIndex);
    }

 
}
