using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string Id;
    public string Label;
    public int StartingValue = 0;
    [Tooltip("-1 For Skills with no applicable Max")]
    public int MaxValue = -1;

    public bool HasMax
    {
        get { return MaxValue != -1; }
    }
}

public class InventoryItem
{
    public Item Item { get; private set; }
    public int Amount;

    public InventoryItem(Item item)
    {
        Item = item;
        Amount = item.StartingValue;
    }

    public bool ReachedMax
    {
        get { return Item.HasMax && Amount >= Item.MaxValue; }
    }
}