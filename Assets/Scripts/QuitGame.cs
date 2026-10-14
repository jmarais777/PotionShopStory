//Title: Application.Quit
//Author: Unity Documentation
//Date: 30 May 2026
//Code version: Unity 6000.4
//Availability: https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Application.Quit.html

using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Game is closed.");

        Application.Quit();
    }

}

