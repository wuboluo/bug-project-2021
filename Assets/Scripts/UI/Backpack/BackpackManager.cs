using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    [SerializeField] private int maxGrid;
    [SerializeField] private List<Item> items;

    private void Start()
    {
        items ??= new List<Item>(maxGrid);
    }

    public int Count => items.Count;
    public bool IsFull => items.Count >= maxGrid;


    public void AddItem(Item i)
    {
        items.Add(i);
    }

    public void RemoveItem(Item i)
    {
        if (items.Contains(i))
        {
            var removeCount = items.FindAll(_ => _ == i).Count;
        }
    }

    public void ChangeItem()
    {
    }
}