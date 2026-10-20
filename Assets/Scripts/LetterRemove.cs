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
    public GameObject dollarSign;
    private bool hasPlayed = false;

    private readonly HashSet<GameObject> processedPotions = new HashSet<GameObject>();

    AudioSource source;
    private Coroutine hideDollarCoroutine;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        dollarSign.SetActive(false);
    }
    

    private void OnTriggerEnter(Collider other) //refers to the box collider on the crate 
    {
        if(letterPile == null)
        {
            Debug.LogWarning("LetterPileInteraction has not been assigned.", this);
            return;
        }

        GameObject potionObject = other.attachedRigidbody != null 
            ? other.attachedRigidbody.gameObject
            : other.transform.root.gameObject;
        
        if(processedPotions.Contains(potionObject))
        {
            return;
        }
        
        bool letterRemoved = letterPile.TryRemoveLetter(potionObject.tag);

        if(!letterRemoved)
        {
            return;
        }

        processedPotions.Add(potionObject);

        if(other.CompareTag("AirPotion") || other.CompareTag("SleepPotion") || other.CompareTag("SunPotion"))
        {
            
        if(source != null)
        {
            source.Play();
        }

        if(dollarSign != null)
        {
            dollarSign.SetActive(true);

            if(hideDollarCoroutine != null)
            {
                StopCoroutine(hideDollarCoroutine);

            }
            hideDollarCoroutine = StartCoroutine(HideDollarSign());
        }


        }

        PotionProgressTracker tracker = PotionProgressTracker.Instance;

        if(tracker != null)
        {
            tracker.RecordPotion(other.gameObject, other.tag);
        }
        else
        {
            Debug.LogWarning("PotionProgressTracker is missing from scene.", this);

        }
    }


    private IEnumerator HideDollarSign()
    {
        yield return new WaitForSeconds(1.5f);
        if(dollarSign != null)
        {
            dollarSign.SetActive(false);
        }
        hideDollarCoroutine = null;
    }


}
