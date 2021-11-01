using Bug.Project21.Props;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Prop/Obtain A Prop")]
public class ObtainPropEventChannelSO : DescriptionScriptableObject
{
    public UnityAction<PropSO, int> OnEventRaised;

    public void RaiseEvent(PropSO item, int obtainAmount)
    {
        OnEventRaised?.Invoke(item, obtainAmount);
    }
}