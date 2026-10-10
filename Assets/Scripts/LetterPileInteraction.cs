//I asked my brother for healp with the arrays and "for" loops
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LetterPileInteraction : MonoBehaviour
{
    [Header("Prompt UI")]
    public GameObject buttonObject;

    [Header("Physical Letters")]
    public GameObject[] physicalLetters;

    [Header("Letter UI")]
    public GameObject[] letterUI;

    [Header("Required Objects")]
    public GameObject[] requiredObjects;

    [Header("Required Potion Tags")]
    public string[] requiredPotionTags;

    private bool playerInRange = false;

    private int currentLetter = 0; //which letter is currently available

    private bool letterUIOpen = false; //track whether current UI is open
  
    private void Start()
    {
        buttonObject.SetActive(false); //prompt starts off invisible
        for (int i = 0; i < letterUI.Length; i++) //hides all letter UI
        {
            letterUI[i].SetActive(false);
        }

        currentLetter = 0;
        letterUIOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            if (currentLetter < physicalLetters.Length) //only show propmpt if letters are still available
            {
                buttonObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            buttonObject.SetActive(false);
            CloseCurrentLetterUI(); //close UI when player leaves
        }
    }

    public void OnEButton(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if(!playerInRange) //player must be in collider
        {
            return;
        }
        if (currentLetter >= physicalLetters.Length) //ensure a letter is still available
        {
            return;
        }
        if(currentLetter >= requiredObjects.Length || requiredObjects[currentLetter] == null)
        {
            Debug.LogWarning("No reqired object assigned for Letter " + (currentLetter + 1));
            return;
        }
        
        if(letterUIOpen) //adjust current letter UI
        {
            CloseCurrentLetterUI();
        }
        else
        {
            OpenCurrentLetterUI();
        }
    }

    private void OpenCurrentLetterUI()
    {
        if(currentLetter >= letterUI.Length || letterUI[currentLetter] == null)
        {
            Debug.LogWarning("No letter UI assigned for letter " + (currentLetter + 1));
            return;
        }
        letterUI[currentLetter].SetActive(true);
        letterUIOpen = true;
    }

    private void CloseCurrentLetterUI()
    {
        if (currentLetter < letterUI.Length && letterUI[currentLetter] != null)
        {
            letterUI[currentLetter].SetActive(false);
        }

        letterUIOpen = false;
    }

    public bool TryRemoveLetter(string potionTag)
    {
        if(currentLetter >= physicalLetters.Length)
        {
            return false;
        }
        if(currentLetter >= requiredPotionTags.Length || string.IsNullOrEmpty(requiredPotionTags[currentLetter]))
        {
            Debug.LogWarning("No required potion tag assigned for letter " + (currentLetter + 1));
            return false;
        }

        if(potionTag != requiredPotionTags[currentLetter])
        {
            Debug.Log("Incorrect potion for letter " + (currentLetter + 1) + ". Required: " + requiredPotionTags[currentLetter] + ", received: " + potionTag);
            return false;
        }

        CloseCurrentLetterUI();

        if(physicalLetters[currentLetter] != null)
        {
            physicalLetters[currentLetter].SetActive(false);
        }

        currentLetter++;

        if(currentLetter < physicalLetters.Length)
        {
            if(playerInRange && buttonObject != null)
            {
                buttonObject.SetActive(true);
            }
        }
        else
        {
            if(buttonObject != null)
            {
                buttonObject.SetActive(false);
            }
            Debug.Log("All letters have been removed.");

        }
        letterUIOpen = false;
        return true;
    }

}

