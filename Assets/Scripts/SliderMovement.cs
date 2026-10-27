//Title: Mathf.PingPong
//Author: Unity Documentation
//Date: 19 August 2026
//Code version: Unity 2017.1
//Availability: https://docs.unity3d.com/6000.5/Documentation/ScriptReference/Mathf.PingPong.html

//Title: Slider
//Author: Unity Documentation
//Date: 17 January 2019
//Code version: Unity 2018.2
//Availability: https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Slider.html

//Title: WaitForSeconds
//Author: Unity Documentation
//Date: 21 August 2026
//Code version: Unity 6000.0
//Availability: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/WaitForSeconds.html 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class SliderMovement : MonoBehaviour
{
    [Header("Slider")]
    public Slider slider; //reference slider
    public float speed = 0.5f; //set speed

    [Header("Safe Zone")]
    [SerializeField] private RectTransform safeZoneRect; 
    //width of teh safe zone as a fraction of the slider track
    //Level 1 = 30%, level 2 = 20%, level 1 = 10%
    [SerializeField] private float[] safeZoneWidths =
    {
        0.30f,
        0.20f,
        0.10f
    };

    [SerializeField, Range(0f, 1f)]
    private float safeZoneCenter = 0.5f;

    [Header("UI elements")]
    public GameObject winText;
    public GameObject loseText;
    public GameObject miniGamePanel;

    [Header("Win Event")]
    [SerializeField] private GameObject successfulPotion;

    [Header("Round Timing")]
    [SerializeField] private float nextRoundDelay = 0.6f;
    [SerializeField] private float resultDisplayDuration = 3f;

    [Header("Round Display")]
    [SerializeField] private GameObject round1Text;
    [SerializeField] private GameObject round2Text;
    [SerializeField] private GameObject round3Text;

    private float movement;
    private float winMin;
    private float winMax;

    private int currentRound;
    private bool miniGameActive;
    private bool sequenceInProgress;
    
    private void Start()
    {
        if(slider == null)
        {
            Debug.Log("Slider is not assigned", this);
            enabled = false;
            return;
        }

        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0f;

        if(safeZoneWidths == null || safeZoneWidths.Length != 3)
        {
            Debug.Log("SliderMovement requires exactly three safe zone widths", this);
            enabled = false;
            return;
        }

        if(winText != null)
        {
            winText.SetActive(false); //when game starts the mini game is not active

        }
        
        if(loseText != null)
        {
            loseText.SetActive(false);

        }


        if(successfulPotion != null)
        {
            successfulPotion.SetActive(false);
        }

        if(miniGamePanel != null)
        {
            miniGamePanel.SetActive(false);

        }

        if(successfulPotion != null)
        {
            successfulPotion.SetActive(false);
        }

        if(safeZoneRect == null)
        {
            Debug.Log("Safe zone rect is not assigned. The green zone will not resize automatically", this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!miniGameActive) //if mini game has ended, stop everything
        {
            return;
        }
        movement += speed * Time.unscaledDeltaTime; //manage movement
        slider.value = Mathf.PingPong(movement, 1f); //moves back and forth
    }

    public void StartMiniGame() //call to start the mini game
    {
        if(sequenceInProgress)
        {
            return;
        }
        sequenceInProgress = true;
        miniGameActive = false;
        currentRound = 0;
        Time.timeScale = 0f;
        UpdateRoundDisplay();
        winText.SetActive(false); 
        loseText.SetActive(false);
        successfulPotion.SetActive(false);
        miniGamePanel.SetActive(true);
        BeginRound();

    }

    private void BeginRound()
    {
        if(currentRound >= safeZoneWidths.Length)
        {
            return;
        }
        UpdateRoundDisplay();
        movement = 0f;
        slider.value = 0f;

        float width = Mathf.Clamp(safeZoneWidths[currentRound], 0.01f, 1f);

        winMin = Mathf.Clamp01(safeZoneCenter - width / 2f);
        winMax = Mathf.Clamp01(safeZoneCenter + width / 2f);

        if(safeZoneRect != null)
        {
            Vector2 anchorMin = safeZoneRect.anchorMin;
            Vector2 anchorMax = safeZoneRect.anchorMax;

            anchorMin.x = winMin;
            anchorMax.x = winMax;

            safeZoneRect.anchorMin = anchorMin;
            safeZoneRect.anchorMax = anchorMax;

            safeZoneRect.offsetMin = new Vector2(
                0f, safeZoneRect.offsetMin.y
            );

            safeZoneRect.offsetMax = new Vector2(
                0f, safeZoneRect.offsetMax.y
            );
        }

        miniGameActive = true;

    }

    public void OnSpace(InputAction.CallbackContext context) //call space input 
    {

        if(context.performed) //if spacebar is pressed
        {
            miniGameActive = false; //stop movement
            Debug.Log("Space pressed!");
            bool won = 
                slider.value >= winMin &&
                slider.value <= winMax;

            if(!won)
            {
                Debug.Log("Brewing failed.");
                loseText.SetActive(true);
                StartCoroutine(FinishSequence(false));
                return;
            }

            if(currentRound < safeZoneWidths.Length - 1)
            {
                currentRound++;
                StartCoroutine(BeginNextRound());
            }
            else
            {
                Debug.Log("All three rounds won!");
                winText.SetActive(true);
                StartCoroutine(FinishSequence(true));
            }

        }
    }

    private void UpdateRoundDisplay()
    {
        if(round1Text != null)
        {
            round1Text.SetActive(currentRound == 0);
        }

        if(round2Text != null)
        {
            round2Text.SetActive(currentRound == 1);
        }

        if(round3Text != null)
        {
            round3Text.SetActive(currentRound == 2);
        }
    }

    private IEnumerator BeginNextRound()
    {
        yield return new WaitForSecondsRealtime(nextRoundDelay);
        if(!sequenceInProgress)
        {
            yield break;
        }
        if(winText != null)
        {
            winText.SetActive(false);

        }
        if(loseText != null)
        {
            loseText.SetActive(false);
        }

        BeginRound();

    }

    private IEnumerator FinishSequence(bool won)
    {
        yield return new WaitForSecondsRealtime(resultDisplayDuration);

        miniGameActive = false;
        sequenceInProgress = false;
        miniGamePanel.SetActive(false);
        UpdateRoundDisplayEnd();
        if(winText != null)
        {
            winText.SetActive(false);
        }
        if(loseText != null)
        {
            loseText.SetActive(false);
        }
        if(won && successfulPotion != null)
        {
            successfulPotion.SetActive(true);
        }

        Time.timeScale = 1f;
        
    }

    private void UpdateRoundDisplayEnd()
    {
        if(round1Text != null)
        {
            round1Text.SetActive(false);
        }

        if(round2Text != null)
        {
            round2Text.SetActive(false);
        }

        if(round3Text != null)
        {
            round3Text.SetActive(false);
        }
    }
}
