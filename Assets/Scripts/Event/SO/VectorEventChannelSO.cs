using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Vector Event Channel")]
public class VectorEventChannelSO : DescriptionBaseSO
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 v3)
    {
        OnEventRaised?.Invoke(v3);
    }
}