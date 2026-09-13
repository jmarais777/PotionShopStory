using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Title: Unity - Code Audio to Play When Entering an Area
//Author: Ryan Murray
//Date: 12 May 2022
//Code version: Unity 2020.3.32f1
//Availability: https://www.youtube.com/watch?v=x2qiWGcLku0


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
