//Author: Brackeys
//Title: PAUSE MENU in Unity
//Date: 20 December 2017
//Code version: Unity 2017.2.0f3
//Availability: https://www.youtube.com/watch?v=JivuXdrIHK0 

//Author: Unity Documentation
//Title: Cursor.lockState
//Date: 14 October 2026
//Code version: Unity 6000.6
//AAvailability: https://docs.unity3d.com/6000.6/Documentation/ScriptReference/Cursor-lockState.html 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnEscapeButton(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void Exit()
    {
        SceneManager.LoadScene("Start Menu");
    }



}
