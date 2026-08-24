using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class Cauldron : MonoBehaviour
{

    [Header("Player Input from Action Map")]
    [SerializeField] private InputActionReference _testAction;

    [Header("Player in Range Checker")]
    [SerializeField] private string playerTag = "Player";
    private bool _isPlayerInRange = false;

    [Header("Ingredients in Cauldron")]
    [SerializeField] private string ingredientATag = "IngredientA";
    [SerializeField] private string ingredientBTag = "IngredientB";

    [Header("Recipe to Test Against")]
    [SerializeField] private PotionRecipe currentRecipe;

    private List<PotionIngredients> ingredientsInCauldron = new List<PotionIngredients>();

    private void OnEnable()
    {
        _testAction.action.performed += OnInteractPerformed; //The method will run when Test is engaged
        _testAction.action.Enable();
    }

    private void OnDisable()
    {
        _testAction.action.performed -= OnInteractPerformed; 
        _testAction.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag)) //Verifies against the player's Tag
        {
            _isPlayerInRange = true; //Sets the bool to true, so that the Interact will work
            return;
        }

        PotionIngredients ingredient = other.GetComponent<PotionIngredients>();
        if (ingredient != null && !ingredientsInCauldron.Contains(ingredient))
        {
            ingredientsInCauldron.Add(ingredient);
        }
    }

    private void OnTriggerExit(Collider other)                                      
    {
        if (other.CompareTag(playerTag)) //Checks to see if the player has left the collider's range
        {
            _isPlayerInRange = false; //Sets the bool to false, preventing the ingredient from being processed
            return;
        }

    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!_isPlayerInRange) return; // The checker 
        if (currentRecipe == null) return;

        bool success = CheckRecipe(currentRecipe);
        // The success message, and calling the minigame will come after this
    }

    private bool CheckRecipe(PotionRecipe recipe)
    {

    /*    if (ingredientsInCauldron.Count != recipe.requiredIngredients.Count)
        {
            return false;
        }

    ^ This would make the check fail if there are extra ingredients, don't think we need this level of strictness? Can always implement later.
    */

        List<(IngredientType type, ProcessingState state)> cauldronIngredientsPool = ingredientsInCauldron // creates a new list so that the actual game objects in the cauldron aren't affected
        .Select(i => (i.Type, i.State)) // this is a function from LINQ which is a data handler, basically convertin the ingredients into type and state pairs so the data is easier to work with
        .ToList(); // changes it to a basic list


    }
}
