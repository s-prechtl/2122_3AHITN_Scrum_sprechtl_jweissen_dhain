using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : ItemStorageSlot, IPointerClickHandler {
    private Inventory _inventory;
    private PlayerController _playerController;

    private void Start() {
        _inventory = Inventory.instance;
        _playerController = PlayerController.instance;
    }

    /**
     * Gets called when the Inventory Slot is clicked
     */
    public override void UseItem() {
        if(Item) {
            if(Item.GetType() == typeof(UsableItem)) {
                ((UsableItem)Item).Select();
                Debug.Log("using " + Item.displayName);
            } else {
                Debug.Log("Item not usable " + Item.displayName);
            }
        } else {
            _playerController.DeselectItem();
        }
    }

    /**
     * Gets called when the Inventory Slot gets clicked on
     */
    public void OnPointerClick(PointerEventData eventData) {
        // When clicked on with right Mouse Button sell the Item
        if(eventData.button == PointerEventData.InputButton.Right) {
            if(Item) {
                _inventory.SellItem(Item, 1); //TODO: wie machen mehr als 1 verkaufen?!
            }
        }
    }
}