using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;

    public InventoryItem(string name, int qty)
    {
        itemName = name;
        quantity = qty;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventoryItem> items = new List<InventoryItem>();

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(string name, int amount)
    {
        InventoryItem item = items.Find(i => i.itemName == name);

        if (item != null)
        {
            item.quantity += amount;
        }
        else
        {
            items.Add(new InventoryItem(name, amount));
        }

        UIInventory.Instance.RefreshUI();
    }

    public void RemoveItem(string name, int amount)
    {
        InventoryItem item = items.Find(i => i.itemName == name);

        if (item == null) return;

        item.quantity -= amount;

        if (item.quantity <= 0)
            items.Remove(item);

        UIInventory.Instance.RefreshUI();
    }
}
