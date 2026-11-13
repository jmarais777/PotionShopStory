using UnityEngine;

public class EdwinDialogueTrigger : MonoBehaviour
{
    [SerializeField] public GameObject potionOfSoothing; // the actual potion object
    
    [SerializeField] public GameObject SpilledTea; // to activate the spilled tea
    [SerializeField] public GameObject FallenMug; // to activate the fallen mug
    [SerializeField] public GameObject BrewedTea; // to deactivate the normal mug

    [SerializeField] public DialogueTrigger potionComplete; // the dialogue trigger for if the potion was completed
    [SerializeField] public DialogueTrigger potionInProgress; // the dialogue trigger for if the potion is not made yet

    bool canTrigger = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Edwin zone contains {other.name}, potion active: {potionOfSoothing.activeInHierarchy}"); // Checkin if player is detected and potion is checked

        if (!other.CompareTag("Player") || !canTrigger) return;
        canTrigger = false;   // locked until the player walks back out

        if (potionOfSoothing.activeInHierarchy)
        {
            potionComplete.Play();
            SpilledTea.SetActive(true);
            FallenMug.SetActive(true);
            BrewedTea.SetActive(false);
        } 
        else
        {
            potionInProgress.Play();
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = true;
        }
    }

/*    void OnTriggerStay(Collider other)
    {
        Debug.Log($"Edwin zone contains {other.name}, potion active: {potionOfSoothing.activeInHierarchy}"); // Checkin if player is detected and potion is checked
        if (!other.CompareTag("Player")) return;

        if (potionOfSoothing.activeInHierarchy)
        {
            potionComplete.Play();
        }
        else
        {
            potionInProgress.Play();
        }
    }
*/
}
