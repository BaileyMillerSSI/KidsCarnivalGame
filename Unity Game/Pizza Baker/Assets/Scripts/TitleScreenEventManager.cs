using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenEventManager: MonoBehaviour
{

    private void Start()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void StartNewGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
