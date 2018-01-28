using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(5);
    }

    public void LoadStartingScreen(string name)
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTutorial(string name)
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame(string name)
    {
        Application.Quit();
    }
}
