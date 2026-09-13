using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

//Title: GameObject.activeSelf
//Author: Unity Documentation
//Date: 11 September 2026
//Code version: Unity 6000.5
//Accessibility: https://docs.unity3d.com/6000.5/Documentation/ScriptReference/GameObject-activeSelf.html 

//Title: Unity - Code Audio to Play When Entering an Area
//Author: Ryan Murray
//Date: 12 May 2022
//Code version: Unity 2020.3.32f1
//Availability: https://www.youtube.com/watch?v=x2qiWGcLku0


public class RedLetterUI : MonoBehaviour
{
    public GameObject buttonObject; // get the buttons
    
    public GameObject redLetter; //refers to the UI to summon

    public GameObject auggieTake; //Auggie's dialogue line

    private bool playerInBox = false; //condition related to box collider

    private bool hasPlayed = false; //to ensure this only happens once

    AudioSource source; 

    void Awake()
    {
        source = GetComponent<AudioSource>(); //get the woosh sound
    }

    void Start()
    {
        buttonObject.SetActive(false); //prompts start off invisible
        auggieTake.SetActive(false); //Auggie's dialogue is also invisible
    }

    private void OnTriggerEnter(Collider other) //when the player enters the box collider
    {
        if(other.CompareTag("Player")) //specifically the player, and if the letter exists
        {
            playerInBox = true; //condition is met
            if(redLetter != null && redLetter.activeSelf) //check if the "physical" letter is active
            {
                buttonObject.SetActive(true); //prompts appear

            }
            
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
           auggieTake.SetActive(false); //the dialogue disappears/will not work
        }
}

public void OnEButton(InputAction.CallbackContext context) //when the E button is pressed (keyboard)
{
    if(context.performed && playerInBox && !hasPlayed) //if the E button is pressed, and the player is in the box collider, and if the player has not performed this action, and if the letter exists
    {
        hasPlayed = true; //confirm player has done this action now
        auggieTake.SetActive(true); //Auggie's dialogue appears
        redLetter.SetActive(false); //letter disappears
        source.Play(); //play woosh sound
    }

}
 
}
