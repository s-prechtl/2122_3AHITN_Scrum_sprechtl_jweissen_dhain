using TMPro;
using UnityEngine;

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
    public override void UseItem() {
        if(_playerController.Money >= Item.cost) {
            _inventory.AddItem(Item, 1);
            _shop.RemoveItem(Item, 1);
            _playerController.ChangeMoney(-Item.cost);
            
            Debug.Log("Buying Item: " + Item.displayName);
            Debug.Log("money left: " + _playerController.Money);
        } else {
            Debug.Log("Not enough money to buy item.");
        }
        
        _shop.onItemChangedCallback?.Invoke();
        _inventory.onItemChangedCallback?.Invoke();
    }
}