using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Dialogue/Dialogue Data Channel")]
public class DialogueDataChannelSO : DescriptionBaseSO
{
    public UnityAction<DialogueDataSO> OnEventRaised;

    public void RaiseEvent(DialogueDataSO dialogue)
    {
        OnEventRaised?.Invoke(dialogue);
    }
}