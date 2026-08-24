//Title: Collider.OnTriggerEnter(Collider)
//Author: Unity Documentatiom
//Date: 6 January 2026
//Code version: Unity 6000.1
//Availability: https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Collider.OnTriggerEnter.html

//Title: Collider.OnTriggerExit(Collider)
//Author: Unity Documentation
//Date: 14 August 2026
//Code version: Unity 6000.5
//Availability: https://docs.unity3d.com/6000.5/Documentation/ScriptReference/Collider.OnTriggerExit.html

//Title: Play Sound Effects on Trigger Events ~ Unity 2022.1 Tutorial
//Author: Chris' Tutorials
//Date: 10 October 2022
// Code version: Unity 2022.1.19f1
//Availability: https://www.youtube.com/watch?v=E7-HAJ4Db64 

//This was adapted from numerous scripts from my Semester 1 project -Kailin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeTrigger : MonoBehaviour
{
    public GameObject textObject; //get the panel
    
    private bool hasPlayed = false; //To ensure this only appears once

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        textObject.SetActive(false); //Hide the panel at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayed) //if the player enters the trigger for the first time
        {
            hasPlayed = true; //this is the only time the panel appears
            textObject.SetActive(true); //panel appears
            source.Play();
        }
        else
        {
            return; //I just inlcuded this for safety
        }
    }

    private void OnTriggerExit(Collider other) //for when the player exits the collider
    {
        if(other.CompareTag("Player"))
        {
            textObject.SetActive(false); //panel disappears
            
        }
    
        
    }


}
