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
    public int PotionsSold { get; private set; }
    public int TotalIngredientCost { get; private set; }
    public int TotalPotionsPrice { get; private set; }
    public GameObject doorBlocker;

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

    private void Start()
    {
        doorBlocker.SetActive(true);
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

    public void RecordIngredient(string ingredientTag)
    {
        string ingredientName;
        int costPerItem;

        if(ingredientTag == "IngredientA")
        {
            ingredientName = "Alber Root";
            costPerItem = 10;
        }
        else if (ingredientTag == "IngredientB")
        {
            ingredientName = "Lamin Bulb";
            costPerItem = 15;
        }
        else
        {
            Debug.Log("Unrecognised tag: " + ingredientTag);
            return;

        }

        //if (string.IsNullOrWhiteSpace(ingredientName))
       // {
           // ingredientName = "Unknown Ingredient";
        //}
       
        IngredientsProcessed++;
        TotalIngredientCost += costPerItem;

        AddLog("Ingredient used: " + ingredientName + ". Cost per item: " + costPerItem + "pp");
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

        string potionName;
        int pricePerPotion = 0;

        if(potionTag == "AirPotion")
        {
            potionName = "Potion of Breathing";
            pricePerPotion = 25;
        }
        else if(potionTag == "SleepPotion")
        {
            potionName = "Potion of Sleeping";
            pricePerPotion = 10;
        }
        else if(potionTag == "SunPotion")
        {
            potionName = "Potion of Sunlight";
            pricePerPotion = 30;
        }
        else if(potionTag == "SoothePotion")
        {
            potionName = "Potion of Soothing";
        }
        else
        {
            Debug.Log("Unrecognised tag: " + potionTag);
            return false;
        }

        PotionsCollected++;
        collectedPotionTypes.Add(potionTag);

        if(potionTag == "SoothePotion")
        {
            AddLog("Potions made: " + potionName + ".");
        }
        else
        {
            PotionsSold++;
            TotalPotionsPrice += pricePerPotion;

            AddLog("Potion added to the crate: " + potionName + ". Price: " + pricePerPotion + "pp");

        }



        if(AllPotionsCollected)
        {
            AddLog("All potion orders have been completed!");
            doorBlocker.SetActive(false);
            Debug.Log("Door can open.");
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
        return "Cost of ingredients used = " + TotalIngredientCost + "pp.";
    }




}
