using System;
using UnityEngine;

[Serializable]
public class ItemStack
{
    [SerializeField] private ItemSO _item;

    public int _count;

    public ItemStack()
    {
        _item = null;
        _count = 0;
    }

    public ItemStack(ItemStack itemStack)
    {
        _item = itemStack.Item;
        _count = itemStack._count;
    }

    public ItemStack(ItemSO item, int count)
    {
        _item = item;
        _count = count;
    }

    public ItemSO Item => _item;
}