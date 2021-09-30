using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Events/UI/Item Event Channel")]
public class ItemEventChannelSO : DescriptionBaseSO
{
    public UnityAction<ItemSO> OnEventRaised;

    public void RaiseEvent(ItemSO item)
    {
        OnEventRaised?.Invoke(item);
    }
}