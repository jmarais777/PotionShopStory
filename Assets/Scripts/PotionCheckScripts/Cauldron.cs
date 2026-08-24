using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Cauldron : MonoBehaviour
{
    [Header("Player Input from Action Map")]
    [SerializeField] private InputActionReference _testAction;

    [Header("Player in Range Checker")]
    [SerializeField] private string playerTag = "Player";
    private bool _isPlayerInRange = false;

    [Header("Recipe to Test Against")]
    [SerializeField] private PotionRecipe currentRecipe; 

    [Header("MiniGame")]
    [SerializeField] private SliderMovement sliderMovement;

    [Header("Outcome Display")]
    [SerializeField] private TextMeshProUGUI outcomeText;
    [SerializeField] private float outcomeDisplayDuration = 2f;

    private List<GameObject> ingredientsInCauldron = new List<GameObject>();

    private void OnEnable()
    {
        _testAction.action.performed += OnInteractPerformed;
        _testAction.action.Enable();
    }

    private void OnDisable()
    {
        _testAction.action.performed -= OnInteractPerformed;
        _testAction.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _isPlayerInRange = true;
            return;
        }

        if (!ingredientsInCauldron.Contains(other.gameObject))
        {
            ingredientsInCauldron.Add(other.gameObject);
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

        ingredientsInCauldron.Remove(other.gameObject);
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!_isPlayerInRange) return;
        if (currentRecipe == null) return; // makin sure there is a recipe attached

        Debug.Log("The test is being performed!");

        ingredientsInCauldron.RemoveAll(g => g == null || !g.activeInHierarchy); // getting rid of disabled game objects

        List<string> namesInCauldron = ingredientsInCauldron.Select(g => g.name.Replace(" (1)", "")).ToList(); // Makes sure even copied objects register by removing the (1) at the end of its name


        bool success = currentRecipe.requiredIngredientNames.All(required => namesInCauldron.Contains(required));
//                       && namesInCauldron.Count == currentRecipe.requiredIngredientNames.Count;

        if(success)
        {
            Debug.Log($"Success! You have all you need for a {currentRecipe.potionName}");
            ShowOutcome($"Wonderful! You have all you need for a {currentRecipe.potionName}!");
            if(sliderMovement != null)
            {
                foreach (GameObject i in ingredientsInCauldron)
                {
                    i.SetActive(false);
                }

                sliderMovement.StartMiniGame();
            }
        }
        else
        {
            ShowOutcome("Brewing failed. Check the recipe again.");
            Debug.Log("Brewing failed.");
        }

    }

    private void ShowOutcome(string message)
    {
        outcomeText.text = message;
        outcomeText.gameObject.SetActive(true);
        StartCoroutine(HideResult());
    }

    private IEnumerator HideResult()
    {
        yield return new WaitForSeconds(outcomeDisplayDuration);
        outcomeText.gameObject.SetActive(false);
    }
}