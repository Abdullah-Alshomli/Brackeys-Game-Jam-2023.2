using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyUI : MonoBehaviour
{
    public void DoExitGame() {
        Application.Quit();
    }

    public void GoToPlay()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void GoToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Main Menu");
    }
    
}
