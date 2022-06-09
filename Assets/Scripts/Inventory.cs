using System;
using UnityEngine;

public class Inventory : ItemStorage {
    #region Singleton

    public static Inventory instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Inventory found");
        }

        instance = this;
    }

    #endregion
    
    private const int InventorySpace = 28;
    private const int MaxItemStack = 999;
    
    /**
     * Adds the specified amount of items to the Inventory
     */
    public override void AddItem(Item item, int amount) {
        if(items.Count >= InventorySpace) {
            Debug.Log("Not enough inventory space!");
            return;
        }
        // Sell overflowing Items
        if(items.ContainsKey(item) && items[item] + amount >= MaxItemStack) {
            SellItem(item, amount - (MaxItemStack - items[item]));
            amount = MaxItemStack - items[item];
        }
        base.AddItem(item, amount);
    }

    public override void RemoveItem(Item item, int amount)
    {
        base.RemoveItem(item, amount);
        if (!items.ContainsKey(item))
        {
            PlayerController.instance.DeselectItem();
        }
    }

    public void SellItem(Item item, int amount) {
        PlayerController.instance.ChangeMoney(item.SellPrice);
        Shop.instance.AddItem(item, amount);
        RemoveItem(item, amount);
    }
}
