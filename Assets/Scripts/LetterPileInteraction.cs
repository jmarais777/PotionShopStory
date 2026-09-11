using UnityEngine;
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

        for (int i = 0; i < physicalLetters.Length; i++)//make sure first letter is active
        {
            if (i == 0)
            {
                physicalLetters[i].SetActive(true);
            }
            else
            {
                physicalLetters[i].SetActive(false);

            }
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
        if(requiredObjects[currentLetter] == null) //required object must exist and still be active
        {
            Debug.LogWarning("No required object has been assigned for Letter " + (currentLetter + 1));
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
        letterUI[currentLetter].SetActive(true);
        letterUIOpen = true;
    }

    private void CloseCurrentLetterUI()
    {
        if (currentLetter < letterUI.Length)
        {
            letterUI[currentLetter].SetActive(false);
        }

        letterUIOpen = false;
    }

    public void RemoveCurrentLetter()
    {
        if (currentLetter >= physicalLetters.Length) //make sure current letter exists
        {
            return;
        }
        CloseCurrentLetterUI(); //close the UI
        physicalLetters[currentLetter].SetActive(false); //remove the physical letter
        currentLetter++; //move to the next letter
        if(currentLetter < physicalLetters.Length) //if there is another letter, activate it
        {
            physicalLetters[currentLetter].SetActive(true);
            if (playerInRange) //player can now interact with the next letter
            {
                buttonObject.SetActive(true);
            }
        }
        else
        {
            buttonObject.SetActive(false); //all letter are read; no need for prompt
            Debug.Log("All letters have been read.");

        }
        letterUIOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
