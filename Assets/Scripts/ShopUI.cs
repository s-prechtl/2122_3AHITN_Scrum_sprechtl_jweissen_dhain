using System.Linq;
using UnityEngine;

public class ShopUI : MonoBehaviour {
    public Transform itemsParent;
    public GameObject shopUI;
    public GameObject inventoryUI;

    private Shop _shop;
    private ShopSlot[] _slots;

    private void Start() {
        // Get Shop instance and add UpdateUI method to OnItemChanged delegate
        _shop = Shop.instance;
        _shop.onItemChangedCallback += UpdateUI;

        // Add all ShopSlot GameObjects to _slots and turn off the Shop UI
        _slots = itemsParent.GetComponentsInChildren<ShopSlot>();
        ToggleShop();

        // Set the icon to not be a raycast target for the Description Hovering to work
        foreach (ShopSlot slot in _slots) {
            slot.icon.raycastTarget = false;
        }
    }

    private void Update() {
        // When "Shop" button is pressed turn on/off Shop UI
        if (Input.GetButtonDown("Shop")) {
            ToggleShop();
        }
    }

    /**
     * Turn on/off the Shop UI
     */
    private void ToggleShop() {
        inventoryUI.gameObject.SetActive(!shopUI.activeSelf);
        shopUI.SetActive(!shopUI.activeSelf);
    }

    /**
     * Is called when something in the Shop UI should update
     */
    private void UpdateUI() {
        // Add all items to the correct slots and clear the ones where no item should be
        for (int i = 0; i < _slots.Length; i++) {
            if (i < _shop.items.Count) {
                _slots[i].AddItem(_shop.items.ElementAt(i).Key);
                _slots[i].nameText.text = _slots[i].Item.displayName;
                _slots[i].costText.text = _slots[i].Item.price + " µ";
                _slots[i].amountText.text = _shop.items[_shop.items.ElementAt(i).Key] + " #";
            }
            else {
                _slots[i].ClearSlot();
            }
        }
    }
}