using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent; // Text
    public string[] lines; // The actual lines/script we'll use
    // If you change 'size' in the serialized field in Unity, you can add each dialogue line as an element
    public float textSpeed; // Speed of the lines
    private int index; // Position within the conversation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = String.Empty;
        gameObject.SetActive(false); // Just hiding the dialogue thing until I attach it to NPCs
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // If left mouse button clicked
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; // getting the line and filling it in
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() // A co-routine
    {
        foreach (char c in lines[index].ToCharArray()) // Breaking each string into characters
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index ++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); // If no other dialogue, set gameobject to inactive
        }
    }

}