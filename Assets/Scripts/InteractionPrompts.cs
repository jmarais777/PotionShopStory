using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteractionPrompts : MonoBehaviour
{
    public GameObject buttonObject; // get the buttons
    
    public GameObject recipePages; //refers to the UI to summon

    private bool playerInBox = false; //condition related to box collider

    public static bool GameIsPaused = false;

    void Start()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        buttonObject.SetActive(false); //prompts start off invisible
        recipePages.SetActive(false); //the recipe UI is invisible
    }

    private void OnTriggerEnter(Collider other) //when the player enters the box collider
    {
        if(other.CompareTag("Player")) //specifically the player
        {
            playerInBox = true; //condition is met
            buttonObject.SetActive(true); //prompts appear
        }
        else
        {
            return; //safety measure
        }
    }
    
    private void OnTriggerExit(Collider other) //for when the player exits the collider
    {
        if(other.CompareTag("Player")) //specifically the playeer
        {
           playerInBox = false; //condition is not met
            buttonObject.SetActive(false); //prompts disappear
            recipePages.SetActive(false); //recipe UI disappears/will not work
        }
}

public void OnEButton(InputAction.CallbackContext context) //when the E button is pressed (keyboard)
{
    if(context.performed && playerInBox) //if the E button is pressed, and the player is in the box collider, and if the "physical letter" exists
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

public void Pause()
{
    recipePages.SetActive(!recipePages.activeSelf);
    Time.timeScale = 0f;
    GameIsPaused = true;

}

public void Resume()
{
    recipePages.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;
}

}
