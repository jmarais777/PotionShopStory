using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Title: Unity - Code Audio to Play When Entering an Area
//Author: Ryan Murray
//Date: 12 May 2022
//Code version: Unity 2020.3.32f1
//Availability: https://www.youtube.com/watch?v=x2qiWGcLku0

//NOTE: all of the potion orders have the tag "AirPotion" so no matter what order you add the potions to the crate, the letters will still be removed sequentially. This shouldn't have a huge effect on gameplay but might feel "sloppy"

public class LetterRemove : MonoBehaviour
{
    public LetterPileInteraction letterPile;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    

    private void OnTriggerEnter(Collider other) //refers to the box collider on the crate 
    {
        if(other.CompareTag("AirPotion")) //if this specific object enters the collider
        {
            letterPile.RemoveCurrentLetter(); //refers to the romove letter function in the LetterPileInteraction script - it, well, removes the letter
            source.Play();        
        }
    }


}
