using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Events/Bool Event Channel")]
public class BoolEventChannelSO : DescriptionBaseSO
{
    public event UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool value)
    {
        OnEventRaised?.Invoke(value);
    }
}