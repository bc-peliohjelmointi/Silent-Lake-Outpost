using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    public int mainMenuSceneBuildIndex = 0;

    public void QuitGame()
    {
        SceneManager.LoadScene(mainMenuSceneBuildIndex);
    }

 
}
