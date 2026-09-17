using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CuttingSpray : MonoBehaviour
{
    [SerializeField] ParticleSystem yellowSpray = null;
    private bool ingredientOnBoard = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IngredientA"))
        {
            ingredientOnBoard = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("IngredientA"))
        {
            ingredientOnBoard = false;
        }
    }

    public void OnEButton(InputAction.CallbackContext context) //when the E button is pressed (keyboard)
{
    if(context.performed && ingredientOnBoard) //if the E button is pressed, and the player is in the box collider, and if the "physical letter" exists
    {
        yellowSpray.Play();
    }

}


}
