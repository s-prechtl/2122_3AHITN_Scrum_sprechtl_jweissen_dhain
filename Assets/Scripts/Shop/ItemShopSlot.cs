using TMPro;

namespace Shop {
    public class ItemShopSlot : ElementStorageSlot<Item> {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;

        private ItemShop _itemShop;
        private Inventory _inventory;
        private PlayerController _playerController;

        private void Start() {
            _itemShop = ItemShop.instance;
            _inventory = Inventory.instance;
            _playerController = PlayerController.instance;
        }

        /**
         * Clears the Shop Slot
        */
        public override void ClearSlot() {
            nameText.text = "";
            costText.text = "";
            amountText.text = "";
            base.ClearSlot();
        }

        /**
         * Gets called when the Shop Slot is clicked
         */
        public override void UseElement() {
            if(Element) {
                if(_playerController.Money >= Element.price) {
                    if(Element) {
                        _playerController.ChangeMoney(-Element.price);
                        _itemShop.itemWasBought = true;
                        // Debug.Log("Buying Item: " + Element.displayName);
                    }
                    _inventory.AddElement(Element, 1);
                    _itemShop.RemoveElement(Element, 1);
                } else {
                    // Debug.Log("Not enough money to buy Item.");
                }

                _itemShop.onElementChangedCallback?.Invoke();
                _inventory.onElementChangedCallback?.Invoke();
            }
        }
    }
}
