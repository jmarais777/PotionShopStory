using UnityEngine;

[CreateAssetMenu(fileName = "ActorSO", menuName = "Dialogue/Character")] // Any "SO" in file names is for ScriptableObject
public class ActorSO : ScriptableObject
{
   public string actorName;
   public Sprite portrait;
    public string side; // Left or Right

}
