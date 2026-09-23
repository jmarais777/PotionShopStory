using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rigidbody2D;

public class BoilStove : MonoBehaviour
{
    [Header("Player Input from Action Map")]
    [SerializeField] private InputActionReference _interactAction;

    [Header("Player in Range Checker")]
    [SerializeField] private string playerTag = "Player";
    private bool _isPlayerInRange = false;

    [SerializeField] private AudioSource _kettleHeating;

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _isPlayerInRange = false;
            return;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!_isPlayerInRange) return;

        _kettleHeating.Play();
    }
}
