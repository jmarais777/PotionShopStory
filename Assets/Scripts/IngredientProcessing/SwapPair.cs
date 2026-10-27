using System.Collections.Generic;
using UnityEngine;

public class SwapPair : MonoBehaviour
{
    [System.Serializable]
    public class WorkstationOutcome
    {
        public string stationId;
        public GameObject replacementIngredient; //Ingredient that replaces the initial unprocessed ingredient
    }

    [Tooltip("Defines what this ingredient becomes at each specific workstation.")]
    [SerializeField] private List<WorkstationOutcome> outcomes = new List<WorkstationOutcome>();
    public bool Swap(string stationId) //Called by Workstation script when the ingredient is swapped, stationID checks which workstation triggered the swap

    {
        WorkstationOutcome outcome = outcomes.Find(o => o.stationId == stationId); //Searches the list of outcomes against the stationID from the station calling the method

        if(outcome == null || outcome.replacementIngredient == null) //do not process ingredient if no valid outcome exists 
        {
            Debug.LogWarning("No valid replacement ingredient found for workstation: " + stationId, this);
            return false;
        }

        string ingredientTag = gameObject.tag;
        

       // string ingredientName = gameObject.name; //save the original ingredient's name before disabling it

        outcome.replacementIngredient.SetActive(true); //activate the processed ingredient

        gameObject.SetActive(false); //Disables the unprocessed ingredient

        if(PotionProgressTracker.Instance != null) //record the successful processing action
        {
            PotionProgressTracker.Instance.RecordIngredient(ingredientTag);
        }
        else
        {
            Debug.LogWarning("PotionProgressTracker is missing from the scene." + " The ingredient was processed, but this was not recorded.", this);

        }
        return true;


    }
}