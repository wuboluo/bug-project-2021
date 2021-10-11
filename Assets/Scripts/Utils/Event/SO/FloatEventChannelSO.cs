using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Bug/Events/Float Event Channel")]
public class FloatEventChannelSO : DescriptionBaseSO
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
