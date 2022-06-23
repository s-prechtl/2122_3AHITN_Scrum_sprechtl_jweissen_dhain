using System.Linq;
using UnityEngine;

namespace Shop {
    public class ShopUI : MonoBehaviour {
        public Transform itemsParent;
        public Transform animalsParent;
        public GameObject shopUI;
        public GameObject inventoryUI;

        private ItemShop _itemShop;
        private AnimalShop _animalShop;
        private ItemShopSlot[] _itemSlots;
        private AnimalShopSlot[] _animalSlots;

        private void Start() {
            // Get Shop instance and add UpdateUI method to OnItemChanged delegate
            _itemShop = ItemShop.instance;
            _animalShop = AnimalShop.instance;
            _itemShop.onElementChangedCallback += UpdateUI;
            _animalShop.onElementChangedCallback += UpdateUI;

            // Add all ItemShopSlot GameObjects to _itemSlots and turn off the Shop UI
            _itemSlots = itemsParent.GetComponentsInChildren<ItemShopSlot>();
            _animalSlots = animalsParent.GetComponentsInChildren<AnimalShopSlot>();
            ToggleShop();

            // Set the icon to not be a raycast target for the Description Hovering to work
            foreach(ItemShopSlot slot in _itemSlots) {
                slot.image.raycastTarget = false;
            }

            foreach(AnimalShopSlot slot in _animalSlots) {
                slot.image.raycastTarget = false;
            }

            UpdateUI();
        }

        private void Update() {
            // When "Shop" button is pressed turn on/off Shop UI
            if(Input.GetButtonDown("Shop")) {
                ToggleShop();
            }
        }

        /**
     * Turn on/off the Shop UI
     */
        private void ToggleShop() {
            inventoryUI.gameObject.SetActive(!shopUI.activeSelf);
            HoverManager.instance.HideDescription();
            shopUI.SetActive(!shopUI.activeSelf);
        }

        /**
     * Is called when something in the Shop UI should update
     */
        private void UpdateUI() {
            // Add all items to the correct slots and clear the ones where no item should be
            for(int i = 0; i < _itemSlots.Length; i++) {
                // Item Slots
                if(i < _itemShop.Elements.Count) {
                    _itemSlots[i].AddElement(_itemShop.Elements.ElementAt(i).Key);
                    _itemSlots[i].nameText.text = _itemSlots[i].Element.displayName;
                    _itemSlots[i].costText.text = _itemSlots[i].Element.price + " µ";
                    _itemSlots[i].amountText.text = _itemShop.Elements[_itemShop.Elements.ElementAt(i).Key] + " #";
                } else {
                    _itemSlots[i].ClearSlot();
                }

                // Animal SLots
                if(i < _animalShop.Elements.Count) {
                    _animalSlots[i].AddElement(_animalShop.Elements.ElementAt(i).Key);
                    _animalSlots[i].nameText.text = _animalSlots[i].Element.displayName;
                    _animalSlots[i].costText.text = _animalSlots[i].Element.price + " µ";
                    _animalSlots[i].amountText.text =
                        _animalShop.Elements[_animalShop.Elements.ElementAt(i).Key] + " #";
                } else {
                    _animalSlots[i].ClearSlot();
                }
            }
        }
    }
}