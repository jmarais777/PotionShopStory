using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct RequiredIngredient
{
    public IngredientType type;
    public ProcessingState requiredState;
}

[CreateAssetMenu(fileName = "PotionRecipe", menuName = "Scriptable Objects/Recipe")]
public class PotionRecipe : ScriptableObject
{
    public string potionName;
    public List<RequiredIngredient> requiredIngredients; 
}

// A scriptable object for each potion recipe