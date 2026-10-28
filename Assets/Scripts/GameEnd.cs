using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class GameEnd : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    
    [SerializeField] private GameObject buttonPrompts;
    [SerializeField] private  GameObject letterContents;
    [SerializeField] private GameObject gameOverScreen;

    //[SerializeField] private Animator panelAnimator;
    //private string animatorBoolName = "isPlaying";

    private bool playerInZone;
    private bool menuIsOpen;
    private bool gameIsOver;

    private bool enabledActionHere;

    private void Awake()
    {
        buttonPrompts.SetActive(false);
        letterContents.SetActive(false);
        gameOverScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //if(panelAnimator != null)
        //{
           // panelAnimator.updateMode =
             //   AnimatorUpdateMode.UnscaledTime;
       // }
    }

    private void OnEnable()
    {
        if(interactAction == null || interactAction.action == null)
        {
            Debug.Log("Assign interact action", this);
            return;
        }

        var action = interactAction.action;

        action.performed += OnInteractPerformed;

        if(!action.enabled)
        {
            action.Enable();
            enabledActionHere = true;
        }
    }

    private void OnDisable()
    {
        if(interactAction != null && interactAction.action != null)
        {
            interactAction.action.performed -= OnInteractPerformed;
            if(enabledActionHere)
            {
                interactAction.action.Disable();
                enabledActionHere = false;
            }
        }
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            buttonPrompts.SetActive(true);
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            buttonPrompts.SetActive(false);
            playerInZone = false;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if(!playerInZone || gameIsOver)
        {
            return;
        }
        if(!menuIsOpen)
        {
            OpenLetterContents();
        }
        else
        {
            ShowGameOverScreen();

        }
    }

    private void OpenLetterContents()
    {
        menuIsOpen = true;

        Time.timeScale = 0f;

        letterContents.SetActive(true);
    }

    private void ShowGameOverScreen()
    {
        gameIsOver = true;
        Time.timeScale = 0f;
        //if(panelAnimator != null)
        //{
          //  panelAnimator.SetBool(animatorBoolName, true);

        //}
        gameOverScreen.SetActive(true);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
