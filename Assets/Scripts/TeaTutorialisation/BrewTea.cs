//Title: ParticleSystem
//Author: Unity Documentation
//Date: 15 September 2026
//Code version: Unity 6000.5
//Availability: https://docs.unity3d.com/6000.5/Documentation/ScriptReference/ParticleSystem.html

//Title: ParticleSystemStopBehavior
//Author: Unity Documentation
//Date: 27 September 2026
//Code version: Unity 6000.5
//Availability: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/ParticleSystemStopBehavior.html

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Collider))]
public class BrewTea : MonoBehaviour
{
    [Header("Player Input from Action Map")]
    [SerializeField] private InputActionReference _interactAction;

    [Header("Player in Range Checker")]
    [SerializeField] private string playerTag = "Player";
    private bool _isPlayerInRange = false;

    [Header("Recipe to Test Against")]
    [SerializeField] private PotionRecipe currentRecipe;

    [Header("Kettle Boil Audio")]
    [SerializeField] private AudioSource KettleBoil;

    [Header("GameObjects")]
    [SerializeField] private GameObject Brewing;
    [SerializeField] private GameObject Brewed;
    [SerializeField] private GameObject MagicKettle;

    [SerializeField] private ParticleSystem teaSteam; 

    private Vector3 _targetPosition = new Vector3(1000f, 1000f, 1000f);

    private List<GameObject> ingredientsInCup = new List<GameObject>();

    private void Start()
    {
        if (teaSteam != null)
        {
            teaSteam.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    private void OnEnable()
    {
        _interactAction.action.performed += OnInteractPerformed;
        _interactAction.action.Enable();
    }

    private void OnDisable()
    {
        _interactAction.action.performed -= OnInteractPerformed;
        _interactAction.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _isPlayerInRange = true;
            return;
        }

        if (!ingredientsInCup.Contains(other.gameObject))
        {
            ingredientsInCup.Add(other.gameObject);
        }

        Debug.Log("Ingredient entered: " + other.gameObject.name); // this was just for testing purposes to ensure it was detected correctly
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _isPlayerInRange = false;
            return;
        }

        ingredientsInCup.Remove(other.gameObject);
    }

    private string PlainName(string rawName) // to remove the (1) and (2) etc. from the end of the game object names, no matter how many duplicates
    {
        int index = rawName.IndexOf(" (");

        if (index >= 0)
        {
            return rawName.Substring(0, index);
        }
        else
        {
            return rawName;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!_isPlayerInRange) return;
        if (currentRecipe == null) return; // makin sure there is a recipe attached

        Debug.Log("The tea is being brewed!");

        ingredientsInCup.RemoveAll(g => g == null || !g.activeInHierarchy); // getting rid of disabled game objects

        List<string> namesInCauldron = ingredientsInCup.Select(g => PlainName(g.name)).ToList();


        bool success = currentRecipe.requiredIngredientNames.All(required => namesInCauldron.Contains(required)) && (namesInCauldron.Count == currentRecipe.requiredIngredientNames.Count);
        //                     && namesInCauldron.Count == currentRecipe.requiredIngredientNames.Count;

        if (success)
        {
            Debug.Log($"Success! You have all you need for a {currentRecipe.potionName}");
            KettleBoil.Play();
            if (teaSteam != null)
            {
                teaSteam.Play();

            }

            StartCoroutine(StopSteamWhenWhistlingEnds());
            
        }
        else if (namesInCauldron.Contains("kettle"))
            {
            Brewing.transform.position = _targetPosition;
            Brewed.SetActive(true);
             foreach (GameObject i in ingredientsInCup)
        {
            i.SetActive(false);
        }
            MagicKettle.SetActive(true);
        }
        else
        {
            Debug.Log("Brewing failed.");
        }

    }

    private IEnumerator StopSteamWhenWhistlingEnds()
    {
        while(KettleBoil.isPlaying)
        {
            yield return null;
        }
        if (teaSteam != null)
        {
            teaSteam.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}