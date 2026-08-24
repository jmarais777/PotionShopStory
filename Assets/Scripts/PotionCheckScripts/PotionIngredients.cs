using UnityEngine;

public enum IngredientType
{
    IngredientA,
    IngredientB
}

public enum ProcessingState
{
    Unprocessed,
    Sliced,
    Crushed
}

public class PotionIngredients : MonoBehaviour
{
    [Header("Ingredient Name")]
    [SerializeField] private IngredientType ingredientType;

    [Header("Current State")]
    [SerializeField] private ProcessingState currentState = ProcessingState.Unprocessed;

    public IngredientType Type => ingredientType;
    
    public ProcessingState State
    {
        get => currentState;
        set => currentState = value; 
    }
}

// This would handle the data on each of the ingredients. It's not actually linked yet because we're using a tag system right now. But I might convert it to utilize this.