using Bug.Project21.Dialogue;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Dialogue/Dialogue line Channel")]
public class DialogueLineChannelSO : DescriptionBaseSO
{
    public UnityAction<string, ActorSO> OnEventRaised;

    public void RaiseEvent(string line, ActorSO actor)
    {
        OnEventRaised?.Invoke(line, actor);
    }
}