using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompts : MonoBehaviour
{
    public GameObject buttonObject; // get the buttons

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonObject.SetActive(false); //prompts start off invisible
    }

    private void OnTriggerEnter(Collider other) //when the player enters the box collider
    {
        if(other.CompareTag("Player")) //specifically the player
        {
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
            buttonObject.SetActive(false); //prompts disappear
        }
}
}
