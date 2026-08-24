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
    public void Swap(string stationId) //Called by Workstation script when the ingredient is swapped, stationID checks which workstation triggered the swap

    {
        WorkstationOutcome outcome = outcomes.Find(o => o.stationId == stationId); //Searches the list of outcomes against the stationID from the station calling the method

        gameObject.SetActive(false); //Disables the unprocessed ingredient

        if (outcome.replacementIngredient != null) 
        {
            outcome.replacementIngredient.SetActive(true); //activates the correct ingredient for the workstation
        }
    }
}