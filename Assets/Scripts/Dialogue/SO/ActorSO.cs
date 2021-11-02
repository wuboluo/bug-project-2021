using UnityEngine;

namespace Bug.Project21.Dialogue
{
    [CreateAssetMenu(fileName = "new Actor", menuName = "Bug/Dialogue/Actor")]
    public class ActorSO : ScriptableObject
    {
        public int _actorId;
    }
}