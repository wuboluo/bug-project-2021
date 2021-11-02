using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Item/Item stack Event Channel")]
public class ItemStackEventChannelSO : DescriptionBaseSO
{
    public UnityAction<ItemStack> OnEventRaised;

    public void RaiseEvent(ItemStack item)
    {
        OnEventRaised?.Invoke(item);
    }
}