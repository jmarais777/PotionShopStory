//Title: How to Create a Start Menu in Unity
//Author: Zenva
//Date: 8 January 2023
//Code version: Unity 2021.3.1f1
//Availability: https://www.youtube.com/watch?v=8kVeDbuqokU 

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnNewGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("New game begins.");
    }

    public void OnCharacter()
    {
        SceneManager.LoadScene("Characters");
        Debug.Log("Character bios");
    }

}

