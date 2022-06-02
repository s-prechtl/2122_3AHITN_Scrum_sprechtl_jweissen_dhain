using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : ItemStorageSlot {
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    private Shop _shop;
    private Inventory _inventory;
    private PlayerController _playerController;

    private void Start() {
        _shop = Shop.instance;
        _inventory = Inventory.instance;
        _playerController = PlayerController.instance;
    }
    
    /**
     * Clears the Shop Slot
     */
    public override void ClearSlot() {
        base.ClearSlot();
        nameText.text = "";
        costText.text = "";
        amountText.text = "";
    }

    /**
     * Gets called when the Shop Slot is clicked
     */
    public void UseItem() {
        if(_playerController.Money >= item.cost) {
            _inventory.AddItem(item, 1);
            _shop.RemoveItem(item, 1);
            _playerController.ChangeMoney(-item.cost);
            
            Debug.Log("Buying Item: " + item.displayName);
            Debug.Log("money left: " + _playerController.Money);
        } else {
            Debug.Log("Not enough money to buy item.");
        }
        
        _shop.onItemChangedCallback?.Invoke();
        _inventory.onItemChangedCallback?.Invoke();
    }
}