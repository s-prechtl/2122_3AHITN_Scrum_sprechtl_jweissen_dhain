using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : ElementStorageSlot<Item>, IPointerClickHandler {
    private Inventory _inventory;
    private PlayerController _playerController;

    private void Start() {
        _inventory = Inventory.instance;
        _playerController = PlayerController.instance;
    }

    /**
     * Gets called when the Inventory Slot is clicked
     */
    public override void UseElement() {
        if(Element) {
            if(Element.GetType() == typeof(UsableItem)) {
                ((UsableItem)Element).Select();
                //Debug.Log("using " + Item.displayName);
            } else {
                //Debug.Log("Item not usable " + Item.displayName);
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
            if(Element) {
                _inventory.SellItem(Element, 1); //TODO: wie machen mehr als 1 verkaufen?!
            }
        }
    }
}