using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Bug/Event/Float Event Channel")]
public class FloatEventChannelSO : DescriptionBaseSO
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
