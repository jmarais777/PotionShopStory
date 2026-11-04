using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
   public DialogueLine[] lines;
   public FollowupDialogue[] nextconvo;
}

[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3, 5)] public string text;

    public AudioClip voicelineClip; // The recorded voicelines
}

[System.Serializable]
public class FollowupDialogue
{
    public DialogueSO nextDialogue;
}
