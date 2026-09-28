using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{   
    public static DialogueManager Instance;

    [Header("Input")]
    public InputActionReference advanceAction;

    private void OnEnable()  { advanceAction.action.Enable(); }
    private void OnDisable() { advanceAction.action.Disable(); }


    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem glowParticles;

    public bool isDialogueActive;

    private DialogueSO currentDialogue;
    private int dialogueIndex;
    private int startFrame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        /* Skip the frame dialogue started so the triggering click/keypress doesn't skip line 1
        if (!isDialogueActive || Time.frameCount == startFrame) return;

        if (AdvancePressed())
        {
            AdvanceDialogue();
        }  
        */
        if (!isDialogueActive || Time.frameCount == startFrame) return;

        if (advanceAction.action.WasPressedThisFrame())
        {
            AdvanceDialogue();
        }
        
    }

    // Returns false if nothing was started (already talking, or empty dialogue)
    public bool StartDialogue(DialogueSO dialogueSO)
    {
        if (isDialogueActive || dialogueSO == null || dialogueSO.lines.Length == 0)
        {
            return false;
        }
        
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        startFrame = Time.frameCount;

        ShowDialogue();
        return true;
    }

    private void ShowDialogue()
    {
        DialogueLine lines = currentDialogue.lines[dialogueIndex];

        portrait.sprite = lines.speaker.portrait;
        actorName.text = lines.speaker.actorName;

        dialogueText.text = lines.text;

        canvasGroup.alpha = 0.8f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }

    public void AdvanceDialogue()
    {
        if(dialogueIndex < currentDialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

/*        if(currentDialogue = "Waking Up")
        {
            if (glowParticles != null)
            {
               glowParticles.Play(); 
            }
            
        } */
    }
}
