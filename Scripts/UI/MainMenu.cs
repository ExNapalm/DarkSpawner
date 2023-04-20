using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour//Contains all shortcuts to new scenes for buttons (attatch to buttons for scene changes)
{

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleSC");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
