using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Bug/Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemStack> _items = new List<ItemStack>();

    public bool Contains(ItemSO item)
    {
        return _items.Any(t => item == t.Item);
    }

    public int Count(ItemSO item)
    {
        return (from currentItemStack in _items where item == currentItemStack.Item select currentItemStack._count)
            .FirstOrDefault();
    }
}