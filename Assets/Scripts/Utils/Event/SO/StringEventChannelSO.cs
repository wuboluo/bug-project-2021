using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/String Event Channel")]
public class StringEventChannelSO : DescriptionBaseSO
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        OnEventRaised?.Invoke(value);
    }
}