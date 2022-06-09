using System;
using UnityEngine;

public class Inventory : ItemStorage {
    #region Singleton

    public static Inventory instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Inventory found");
        }

        instance = this;
    }

    #endregion

    private const int _InventorySpace = 28;
    private const int _MaxItemStack = 999;

    /**
     * Adds the specified amount of items to the Inventory
     */
    public override void AddItem(Item item, int amount) {
        if (items.Count >= _InventorySpace) {
            Debug.Log("Not enough inventory space!");
            return;
        }
        // Sell overflowing Items
        if (items.ContainsKey(item) && items[item] + amount >= _MaxItemStack) {
            SellItem(item, amount - (_MaxItemStack - items[item]));
            amount = _MaxItemStack - items[item];
        }
        base.AddItem(item, amount);
    }

    /**
     * Calls ItemStorage.RemoveItem() and deselects the item if removed
     */
    public override void RemoveItem(Item item, int amount) {
        base.RemoveItem(item, amount);
        if (!items.ContainsKey(item) && PlayerController.instance.GetSelectedItem() == item) {
            PlayerController.instance.DeselectItem();
        }
    }

    /**
     * Sells the Item for the Item Sell Price and puts it in the Shop for the full price
     */
    public void SellItem(Item item, int amount) {
        PlayerController.instance.ChangeMoney(item.SellPrice);
        Shop.instance.AddItem(item, amount);
        RemoveItem(item, amount);
    }
}