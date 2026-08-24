using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PotionRecipe", menuName = "Scriptable Objects/Recipe")]
public class PotionRecipe : ScriptableObject
{
    public string potionName;
    public List<string> requiredIngredientNames; 
}

// A scriptable object for each potion recipe 