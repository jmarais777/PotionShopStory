using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

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

        ingredientsInCauldron.RemoveAll(g => g == null || !g.activeInHierarchy); // getting rid of disabled game objects

        List<string> namesInCauldron = ingredientsInCauldron.Select(g => g.name.Replace(" (1)", "")).ToList(); // Makes sure even copied objects register by removing the (1) at the end of its name

        bool success = currentRecipe.requiredIngredientNames.All(required => namesInCauldron.Contains(required));
//                       && namesInCauldron.Count == currentRecipe.requiredIngredientNames.Count;

        Debug.Log(success ? $"Success! You have all you need for a {currentRecipe.potionName}" : "Brewing failed.");
    }
}