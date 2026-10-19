using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ResultsUIController : MonoBehaviour
{
    [Header("New Input System")]
    [SerializeField] private InputActionReference interactAction;

    [Header("UI Panels")]
    [SerializeField] private GameObject completionPrompt;
    [SerializeField] private GameObject resultsInvoice;

    [Header("Results Text")]
    [SerializeField] private TMP_Text actionLogText;
    [SerializeField] private TMP_Text summaryText;

    private PotionProgressTracker tracker;
    private bool invoiceIsOpen;

    private bool enabledActionHere;

    private bool playerInCollider = false;

    private void Awake()
    {
        if(completionPrompt != null)
        {
            completionPrompt.SetActive(false);
        }

        if(resultsInvoice != null)
        {
            resultsInvoice.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(interactAction == null || interactAction.action == null)
        {
            Debug.LogError("Assign an Interact Input Action to ResultsUIController.", this);
            return;
        }

        interactAction.action.performed += OnInteractPerformed;

        if(!interactAction.action.enabled)
        {
            interactAction.action.Enable();
            enabledActionHere = true;
        }
    }

    private void Start()
    {
        tracker = PotionProgressTracker.Instance;

        if(tracker == null)
        {
            Debug.LogError("PotionProgressTracker is missing from this scene.", this);
            enabled = false;
            return;
        }

        tracker.OnProgressChanged += RefreshUI;

        RefreshUI();
    }

    private void OnDisable()
    {
        if(interactAction != null && interactAction.action != null)
        {
            interactAction.action.performed -= OnInteractPerformed;

            if(enabledActionHere)
            {
                interactAction.action.Disable();
            }
        }

        enabledActionHere = false;

        if(tracker != null)
        {
            tracker.OnProgressChanged -= RefreshUI;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInCollider = true;
            RefreshUI();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInCollider = false;
            invoiceIsOpen = false;
            if(completionPrompt != null)
            {
                completionPrompt.SetActive(false);
            }

            if(resultsInvoice != null)
            {
                resultsInvoice.SetActive(false);
            }
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if(tracker == null || !tracker.AllPotionsCollected)
        {
            return;
        }

        invoiceIsOpen = !invoiceIsOpen;

        if(resultsInvoice != null && playerInCollider)
        {
            resultsInvoice.SetActive(invoiceIsOpen);
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        if(tracker == null)
        {
            return;
        }

        if(actionLogText != null)
        {
            actionLogText.text = tracker.GetActionLogText();
        }

        if(summaryText != null)
        {
            summaryText.text = tracker.GetSummaryText();
        }

        if(completionPrompt != null)
        {
            completionPrompt.SetActive(playerInCollider && tracker.AllPotionsCollected && !invoiceIsOpen);
        }

        if(resultsInvoice != null)
        {
            resultsInvoice.SetActive(playerInCollider && invoiceIsOpen);
        }
    }




}
