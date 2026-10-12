using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueSO dialogue;

    [SerializeField] bool playOnStart;
    [SerializeField] bool playOnTriggerEnter;
    [SerializeField] string playerTag = "Player";

    [SerializeField] bool playOnce = true;

    bool hasPlayed;

    private bool isWaiting;

    void Start()
    {
        if (playOnStart) 
        {
            Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (playOnTriggerEnter && other.CompareTag(playerTag) && !isWaiting)
        {
           StartCoroutine(WaitAndExecute());
        }
         
    }

    private IEnumerator WaitAndExecute()
    {
        isWaiting = true;

        Play();

        yield return new WaitForSeconds(5f);

        isWaiting = false;
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
