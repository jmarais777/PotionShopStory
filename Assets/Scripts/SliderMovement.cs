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

public class SliderMovement : MonoBehaviour
{
    public Slider slider; //reference slider
    public float speed = 0.5f; //set speed
    public float winMin = 0.33f;
    public float winMax = 0.66f;
    private float movement; //movement variable (for the time delta time stuff)
    private bool miniGameActive = false; //slider is moving
    public GameObject winText;
    public GameObject loseText;
    public GameObject miniGamePanel;
    [SerializeField] private GameObject successfulPotion;
    
    void Start()
    {
        //successfulPotion.SetActive(false);
        winText.SetActive(false); //when game starts the mini game is not active
        loseText.SetActive(false);
        miniGamePanel.SetActive(false);
        slider.minValue = 0f; //set minimum value
        slider.maxValue = 1f; // set maximum value
        slider.value = 0f; // set current or starting value
    }

    // Update is called once per frame
    void Update()
    {
        if (!miniGameActive) //if mini game has ended, stop everything
        {
            return;
        }
        movement += speed * Time.deltaTime; //manage movement
        slider.value = Mathf.PingPong(movement, 1f); //moves back and forth
    }

    public void StartMiniGame() //call to start the mini game
    {
        miniGameActive = true;
        movement = 0f;
        slider.value = 0f;
        winText.SetActive(false); 
        loseText.SetActive(false);
        miniGamePanel.SetActive(true);
    }

    public void OnSpace(InputAction.CallbackContext context) //call space input 
    {
        if(!miniGameActive)
        {
            return;
        }
        if(context.performed) //if spacebar is pressed
        {
            miniGameActive = false; //stop movement
            Debug.Log("Space pressed!");
            bool won = false;
            if(slider.value >= winMin && slider.value <= winMax) //if slider is in middle
            {
                Debug.Log("You win!");
                winText.SetActive(true);
                won = true;
            }
            else
            {
                Debug.Log("You lose");
                loseText.SetActive(true);

            }

            StartCoroutine(EndMiniGame(won));
        }
    }
    IEnumerator EndMiniGame(bool won)
    {
        yield return new WaitForSeconds(3f);
        miniGamePanel.SetActive(false);
        if(won && successfulPotion != null)
        {
            successfulPotion.SetActive(true);
        }
    }
}
