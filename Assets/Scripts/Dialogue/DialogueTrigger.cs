using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueSO dialogue;

    [SerializeField] bool playOnStart;
    [SerializeField] bool playOnTriggerEnter;
    [SerializeField] string playerTag = "Player";

    [SerializeField] bool playOnce = true;

    bool hasPlayed;

    void Start()
    {
        if (playOnStart) 
        {
            Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (playOnTriggerEnter && other.CompareTag(playerTag))
        {
           Play(); 
        }
         
    }

    public void Play()
    {
        if (playOnce && hasPlayed) 
        {
            return;
        }
        

        if (DialogueManager.Instance.StartDialogue(dialogue))
        {
            hasPlayed = true;
        }
            
    }
}
