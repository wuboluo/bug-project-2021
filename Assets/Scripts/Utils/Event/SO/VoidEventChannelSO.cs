using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Void Event Channel")]
public class VoidEventChannelSO : DescriptionBaseSO
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}