//Title: GameObject.activeSelf
//Author: Unity Documentation
//Date: 10 September 2026
//Code version: Unity 6000.5
//Availability: https://docs.unity3d.com/6000.5/Documentation/ScriptReference/GameObject-activeSelf.html 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteractionPrompts : MonoBehaviour
{
    [Header("UI Objects")] //I added Headers to clear things up
    public GameObject buttonObject; // get the buttons
    public GameObject letterOne; //the letter UI

    [Header("Physical Object")] // refers to the "physical" letter
    public GameObject realFirstLetter;

    private bool playerInBox = false; //condition related to box collider

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonObject.SetActive(false); //prompts start off invisible
        letterOne.SetActive(false); //the letter UI is invisible
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
            letterOne.SetActive(false); //letter UI disappears/will not work
        }
}

public void OnEButton(InputAction.CallbackContext context) //when the E button is pressed (keyboard)
{
    if(context.performed && playerInBox && realFirstLetter != null && realFirstLetter.activeSelf) //if the E button is pressed, and the player is in the box collider, and if the "physical letter" exists
    {
        letterOne.SetActive(!letterOne.activeSelf); //the letter UI is activated
    }

}

}
