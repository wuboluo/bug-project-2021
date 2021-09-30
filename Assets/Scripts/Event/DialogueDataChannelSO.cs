using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Events/Dialogue Data Channel")]
public class DialogueDataChannelSO : DescriptionBaseSO
{
    public UnityAction<DialogueDataSO> OnEventRaised;

    public void RaiseEvent(DialogueDataSO dialogue)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(dialogue);
    }
}