using System;
using Shop;
using UnityEngine;

public class Inventory : ElementStorage<Item> {
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
    public override void AddElement(Item item, int amount) {
        if (Elements.Count >= _InventorySpace) {
            Debug.Log("Not enough inventory space!");
            return;
        }
        // Sell overflowing Items
        if (Elements.ContainsKey(item) && Elements[item] + amount >= _MaxItemStack) {
            SellItem(item, amount - (_MaxItemStack - Elements[item]));
            amount = _MaxItemStack - Elements[item];
        }
        base.AddElement(item, amount);
    }

    /**
     * Calls ItemStorage.RemoveElement() and deselects the item if removed
     */
    public override void RemoveElement(Item item, int amount) {
        base.RemoveElement(item, amount);
        if (!Elements.ContainsKey(item) && PlayerController.instance.SelectedItem == item) {
            PlayerController.instance.DeselectItem();
        }
    }

    /**
     * Sells the Item for the Item Sell Price and puts it in the Shop for the full price
     */
    public void SellItem(Item item, int amount) {
        PlayerController.instance.ChangeMoney(item.SellPrice);
        ItemShop.instance.AddElement(item, amount);
        RemoveElement(item, amount);
    }
}