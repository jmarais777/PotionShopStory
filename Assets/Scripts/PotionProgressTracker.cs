using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PotionProgressTracker : MonoBehaviour
{
    public static PotionProgressTracker Instance { get; private set; }

    [Header("Required Potion Types")]
    [Tooltip("Enter the exact tags required to complete the crate")]
    [SerializeField] private List<string> requiredPotionTags = new List<string> { "SoothePotion", "AirPotion", "SunPotion", "SleepPotion" };
    
    private readonly List<string> actionLog = new List<string>();
    private readonly HashSet<string> collectedPotionTypes = new HashSet<string>();
    private readonly HashSet<GameObject> countedPotionObjects = new HashSet<GameObject>();

    public int IngredientsProcessed { get; private set; }
    public int PotionsCollected { get; private set; }

    public event Action OnProgressChanged;

    public bool AllPotionsCollected
    {
        get
        {
            return requiredPotionTags.Count > 0 &&
            requiredPotionTags.TrueForAll(
                tag => collectedPotionTypes.Contains(tag));
            

        }
    }

    public int RequiredPotionTypeCount
    {
        get { return requiredPotionTags.Count; }
    }

    public int CollectedPotionTypeCount
    {
        get { return collectedPotionTypes.Count; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple PotionProgressTracker instances found.", this);
            Destroy(gameObject);
            return;
        }
         Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void RecordIngredient(string ingredientName)
    {
        if (string.IsNullOrWhiteSpace(ingredientName))
        {
            ingredientName = "Unknown Ingredient";
        }
        IngredientsProcessed++;
        //AddLog(ingredientName + " has been processed. " + "Total ingredients processed: " + IngredientsProcessed);
        AddLog("Ingredient used: " + IngredientsProcessed + ". Cost: 10pp");
    }

    public bool IsRequiredPotionTag(string potionTag)
    {
        return !string.IsNullOrEmpty(potionTag) && 
        requiredPotionTags.Contains(potionTag);
    }

    public bool RecordPotion(GameObject potionObject, string potionTag)
    {
        if(potionObject == null)
        {
            Debug.LogWarning("Cannot record a potion: object is null.");
            return false;
        }

        if(!IsRequiredPotionTag(potionTag))
        {
            Debug.Log("Potion tag is not on the required list: " + potionTag);
            return false;
        }

        if(!countedPotionObjects.Add(potionObject))
        {
            return false;
        }

        PotionsCollected++;

        bool firstOfType = collectedPotionTypes.Add(potionTag);

        string message = "Potion added to the crate: " + potionTag + ". Price: 20pp";
        //string message = potionTag + " added to the crate. " + "Potions collected: " + PotionsCollected + ". Required types collected: " + CollectedPotionTypeCount + "/" + RequiredPotionTypeCount + ".";

       // if(firstOfType)
        //{
          //  message += "This potion type is now complete.";
       //}

        AddLog(message);

        if(AllPotionsCollected)
        {
            //AddLog("All required potion types have been collected!");
            AddLog("All potions have been added!");
        }

        return true;
    }

    private void AddLog(string message)
    {
        actionLog.Add(message);
        Debug.Log(message);
        OnProgressChanged?.Invoke();
    }

    public string GetActionLogText()
    {
        if(actionLog.Count == 0)
        {
            return "No actions have been recorded yet.";
        }

        StringBuilder builder = new StringBuilder();

        for(int i = 0; i < actionLog.Count; i++)
        {
            builder.Append(i + 1);
            builder.Append(". ");
            builder.AppendLine(actionLog[i]);
        }

        return builder.ToString();
    }

    public string GetSummaryText()
    {
        return "Cost of ingredients used = 10pp x " + IngredientsProcessed + ". Price of ingredients sold = 20pp x " + PotionsCollected;
        
        //return "Ingredients processed: " + IngredientsProcessed + "/nPotions collected: " + PotionsCollected + "/nRequired Potion types: " + CollectedPotionTypeCount + "/" + RequiredPotionTypeCount;
    }




}
