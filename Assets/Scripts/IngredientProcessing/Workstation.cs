using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))] //Makes it so that a collider is required, and if there isn't one on the game object, it gives it one. So cool :)
public class Workstation : MonoBehaviour
{
    [Header("Player Input from Action Map")]
    [SerializeField] private InputActionReference _interactAction;

    [Header("Station Type")]
    [SerializeField] private string _stationID; 

    [Header("Player in Range Checker")]
    [SerializeField] private string playerTag = "Player";
    private bool _isPlayerInRange = false;

    private List<SwapPair> _pairsInRange = new List<SwapPair>(); //A list of the ingredient pairs in the collider's trigger boundary

    private void OnEnable()
    {
        _interactAction.action.performed += OnInteractPerformed; //The method will run when Interact is engaged
        _interactAction.action.Enable();
    }

    private void OnDisable()
    {
        _interactAction.action.performed -= OnInteractPerformed; 
        _interactAction.action.Disable();
    }

    private void OnTriggerEnter(Collider _otherCollider)
    {
        if (_otherCollider.CompareTag(playerTag)) //Verifies against the player's Tag
        {
            _isPlayerInRange = true; //Sets the bool to true, so that the Interact will work
            return;
        }

        if (_otherCollider.TryGetComponent(out SwapPair pair)) //Check to see if the object triggering the workstation collider has a SwapPair component
        {
            _pairsInRange.Add(pair); //If true, it adds a pair to the _pairInRange list
        }
    }

    private void OnTriggerExit(Collider _otherCollider) //This is the inverse of the OnTriggerEnter
    {
        if (_otherCollider.CompareTag(playerTag)) //Checks to see if the player has left the collider's range
        {
            _isPlayerInRange = false; //Sets the bool to false, preventing the ingredient from being processed
            return;
        }

        if (_otherCollider.TryGetComponent(out SwapPair pair))
        {
            _pairsInRange.Remove(pair); //Removes the pair from the list when it leaves the collider's range
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!_isPlayerInRange) return; //The checker that prevents the SwapPair from swapping

        foreach (SwapPair pair in _pairsInRange) //Makes it so that every ingredient in range will get swapped, if they have a SwapPair in the list
        {
            pair.Swap(_stationID);//Calls the Swap method, from my SwapPair script
        }

        _pairsInRange.Clear(); //Clears the list, since everything on it was just processed. Because apparently disabling the object doesn't trigger the OnTriggerExit :(
    }
}