using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterRemove : MonoBehaviour
{
    public LetterPileInteraction letterPile;

    private void OnTriggerEnter(Collider other) //refers to the box collider on the crate 
    {
        if(other.CompareTag("AirPotion")) //if this specific object enters the collider
        {
            letterPile.RemoveCurrentLetter(); //refers to the romove letter function in the LetterPileInteraction script - it, well, removes the letter
        }
    }


}
