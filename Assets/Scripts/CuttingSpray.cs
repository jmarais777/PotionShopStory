//Title: Triggering Impact Particle - Unity
//Author: ACDev
//Date: 15 September 2019
//Code version: Unity 2019.2.0f1
//Availability: https://www.youtube.com/watch?v=BXh6LC1H5S0 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CuttingSpray : MonoBehaviour
{
    [SerializeField] ParticleSystem yellowSpray = null; //get the yellow spray effect
    [SerializeField] ParticleSystem blueSpray = null; // get the blue spray effect
    private bool ingredientAOnBoard = false;
    private bool ingredientBOnBoard = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IngredientA")) //if the yellow ingredient is on the cutting board
        {
            ingredientAOnBoard = true;
            ingredientBOnBoard = false;
        }
        else if(other.CompareTag("IngredientB")) //if the blue ingredient is on the cutting board
        {
            ingredientBOnBoard = true;
            ingredientAOnBoard = false;
        }
    }

    public void OnTriggerExit(Collider other) //the particle system won't activate if the items leave the box collider on teh cutting board
    {
        if(other.CompareTag("IngredientA"))
        {
            ingredientAOnBoard = false;
        }
        else if(other.CompareTag("IngredientB"))
        {
            ingredientBOnBoard = false;
        }
    }

    public void OnEButton(InputAction.CallbackContext context) //when the E button is pressed (keyboard)
{
    if(!context.performed) //safety check
    {
        return;
    }
    if(context.performed && ingredientAOnBoard) //if the button is pressed and the yellow object is on the cutting board
    {
        yellowSpray.Play(); //play the yellow particle effect
        blueSpray.Stop(); //ensure the blue effect won't interfere
    }
    else if(context.performed && ingredientBOnBoard) //if the button is pressed and the blue object is on the board
    {
        blueSpray.Play(); //play the blue particle effect
        yellowSpray.Stop(); //ensure the yellow effect won't interfere
    }


}


}
