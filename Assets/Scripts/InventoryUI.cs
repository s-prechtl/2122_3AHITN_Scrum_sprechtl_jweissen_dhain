using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public Transform itemsParent;
    public GameObject inventoryUI;
    private Inventory _inventory;
    private InventorySlot[] _slots;

    private void Start() {
        // Get Inventory instance and add UpdateUI method to OnItemChanged delegate
        _inventory = Inventory.instance;
        _inventory.onItemChangedCallback += UpdateUI;

        // Add all InventorySlot GameObjects to _slots and turn off the Inventory UI
        _slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        ToggleInventory();

        // Set the icon to not be a raycast target for the Description Hovering to work
        foreach(InventorySlot slot in _slots) {
            slot.icon.raycastTarget = false;
        }
    }

    private void Update() {
        // When "Inventory" button is pressed turn on/off Inventory UI
        if(Input.GetButtonDown("Inventory")) {
            ToggleInventory();
        }
    }

    /**
     * Turn on/off the Inventory UI
     */
    private void ToggleInventory() {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    /**
     * Is called when something in the Inventory UI should update
     */
    private void UpdateUI() {
        // Add all items to the correct slots and clear the ones where no item should be
        for(int i = 0; i < _slots.Length; i++) {
            if(i < _inventory.items.Count) {
                _slots[i].AddItem(_inventory.items.ElementAt(i).Key);
                if(_inventory.items[_inventory.items.ElementAt(i).Key] > 1) {
                    _slots[i].amountText.text = "" + _inventory.items[_inventory.items.ElementAt(i).Key];
                }
            } else {
                _slots[i].ClearSlot();
            }
        }
    }
}
