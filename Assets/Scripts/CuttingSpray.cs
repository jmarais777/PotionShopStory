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

    private GameObject ingredientAObject;
    private GameObject ingredientBObject;

    //rivate bool ingredientAOnBoard = false;
    //private bool ingredientBOnBoard = false;
    private bool playerOnBoard = false;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IngredientA")) //if the yellow ingredient is on the cutting board
        {
            ingredientAObject = other.gameObject;
            //ingredientAOnBoard = true;
           // ingredientBOnBoard = false;

            Debug.Log("yellow");
        }
        else if(other.CompareTag("IngredientB")) //if the blue ingredient is on the cutting board
        {
            ingredientBObject = other.gameObject;
            //ingredientBOnBoard = true;
            //ingredientAOnBoard = false;

            Debug.Log("blue");
        }
        else if(other.CompareTag("Player"))
        {
            playerOnBoard = true;
            Debug.Log("Player");

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == ingredientAObject)
        {
            ingredientAObject = null;
            if (yellowSpray != null)
            {
                yellowSpray.Stop();
            }
        }
        if(other.gameObject == ingredientBObject)
        {
            ingredientBObject = null;
            if (blueSpray != null)
            {
                blueSpray.Stop();
            }
        }
        if(other.CompareTag("Player"))
        {
            playerOnBoard = false;
            if(yellowSpray != null)
            {
                yellowSpray.Stop();
            }
            if(blueSpray != null)
            {
                blueSpray.Stop();
            }
        }
    }

    public void OnEButton(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if(!playerOnBoard)
        {
            return;
        }

        bool yellowIngredientPresent =
            ingredientAObject != null &&
            ingredientAObject.activeInHierarchy;

        bool blueIngredientPresent =
            ingredientBObject != null &&
            ingredientBObject.activeInHierarchy;

        if(yellowIngredientPresent)
        {
            if(yellowSpray != null)
            {
                yellowSpray.Play();
            }
            if(blueSpray != null)
            {
                blueSpray.Stop();
            }
        }
        else if (blueIngredientPresent)
        {
            if(blueSpray != null)
            {
                blueSpray.Play();
            }
            if(yellowSpray != null)
            {
                yellowSpray.Stop();
            }
        }
        else
        {
            if(yellowSpray != null)
            {
                yellowSpray.Stop();
            }
            if(blueSpray != null)
            {
                blueSpray.Stop();
            }

            Debug.Log("No active object in station.");
        }
    }

   // public void OnTriggerExit(Collider other) //the particle system won't activate if the items leave the box collider on teh cutting board
   // {
       // if(other.CompareTag("IngredientA"))
        //{
            //ingredientAOnBoard = false;
            //yellowSpray.Stop();

       // }
       // else if(other.CompareTag("IngredientB"))
       // {
         //   ingredientBOnBoard = false;
//blueSpray.Stop();

      //  }
       // else if(other.CompareTag("Player"))
       // {
       //     playerOnBoard = false;
       //     yellowSpray.Stop();
       //     blueSpray.Stop();
       // }
   // }



    //public void OnEButton(InputAction.CallbackContext context) //when the E button is pressed (keyboard)
//{
    //if(!context.performed) //safety check
    //{
       // return;
    //}
    //if(context.performed && ingredientAOnBoard && playerOnBoard) //if the button is pressed and the yellow object is on the cutting board
    //{
      //  yellowSpray.Play(); //play the yellow particle effect
       // blueSpray.Stop(); //ensure the blue effect won't interfere
   // }
    //else if(context.performed && ingredientBOnBoard && playerOnBoard) //if the button is pressed and the blue object is on the board
    //{
     //   blueSpray.Play(); //play the blue particle effect
     //   yellowSpray.Stop(); //ensure the yellow effect won't interfere
   // }




}
