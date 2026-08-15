using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeTrigger : MonoBehaviour
{
    public GameObject textObject;
    
    private bool hasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        textObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            textObject.SetActive(true);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            textObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
