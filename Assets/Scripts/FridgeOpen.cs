//Title: Camrea.ScreenPointToRay
//Author: Unity Documentation
//Date: 10 October 2026
//Code version: Unity 6000.6
//Availability: https://docs.unity3d.com/6000.6/Documentation/ScriptReference/Camera.ScreenPointToRay.html

//Title: Class Mouse
//Author: Unity Documentation
//Code version: Input System 1.19.0
//Availability: https://docs.unity.cn/Packages/com.unity.inputsystem%401.19/api/UnityEngine.InputSystem.Mouse.html

//Title: Physics.Raycast
//Author: Unity Documentation
//Date: 10 October 2026
//Code version: Unity 6000.6
//Availability: https://docs.unity3d.com/6000.6/Documentation/ScriptReference/Physics.Raycast.html 

//Title: Animator.SetBool
//Author: Unity Documentation
//Date: 10 October 2026
//Code version: Unity 6000.6
//Availability: https://docs.unity3d.com/6000.6/Documentation/ScriptReference/Animator.SetBool.html 

//Title: Transform.IsChildOf
//Author: Unity Documentation 
//Date: 10 October 2026
//Code version: Unity 6000.6
//Availability: https://docs.unity3d.com/6000.6/Documentation/ScriptReference/Transform.IsChildOf.html



using UnityEngine;
using UnityEngine.InputSystem;

public class FridgeOpen : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private Camera playerCamera;

    private static readonly int IsOpenHash =
    Animator.StringToHash("IsOpen");

    private bool isOpen = false;

    private void Awake()
    {
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }


    private void Update()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame) // use new input system to detect a mouse click
        {
            return;
        }

        if (playerCamera == null || doorAnimator == null)
        {
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay( // cast a ray from camera through the mouse position
            Mouse.current.position.ReadValue()
        );

        if (Physics.Raycast(ray, out RaycastHit hit)) // checks if the ray hits a collider
        {
            if(hit.transform == transform || hit.transform.IsChildOf(transform)) // check if clicked object belongs to the door 
            {
                ToggleDoor();
            }
        }
        
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;

        doorAnimator.SetBool(IsOpenHash, isOpen); //tells the Animator which animation to play
    }
}
