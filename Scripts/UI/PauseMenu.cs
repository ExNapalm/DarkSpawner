using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Pauses the game, either by the Gear Icon or [Escape]
    public static bool gameisPaused = false;
    [SerializeField] GameObject EvManager;

    private ChangeUIscreen UIscreen;

    private void Start()
    {
        UIscreen = EvManager.GetComponent<ChangeUIscreen>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }   
    }

     void Resume()
    {
        Time.timeScale = 1f;
        UIscreen.disableMenu();
        gameisPaused = false;
        Debug.Log("Unpaused!");
    }

     void Pause()
    {
        Time.timeScale = 0f;
        gameisPaused = true;
        UIscreen.enableMenu();
        Debug.Log("Paused!");
    }

    public void OpenPause()
    {
        Time.timeScale = 0f;
        gameisPaused = true;
        UIscreen.enableMenu();
        Debug.Log("Paused!(BTN)");
    }
    public void ClosePause()
    {
        Time.timeScale = 1f;
        gameisPaused = false;
        UIscreen.disableMenu();
        Debug.Log("Unpaused!(BTN)");
    }
}
