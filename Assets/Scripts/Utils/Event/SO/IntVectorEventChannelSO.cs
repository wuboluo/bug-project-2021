using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Int-Vector Event Channel")]
public class IntVectorEventChannelSO : DescriptionBaseSO
{
    public UnityAction<int, Vector3> OnEventRaised;

    public void RaiseEvent(int value, Vector3 v3)
    {
        OnEventRaised?.Invoke(value, v3);
    }
}