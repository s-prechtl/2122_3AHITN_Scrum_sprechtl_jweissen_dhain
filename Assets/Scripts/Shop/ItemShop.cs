using UnityEngine;

namespace Shop {
    public class ItemShop : ElementStorage<Item> {
        #region Singleton

        public static ItemShop instance;

        private void Awake() {
            if(instance != null) {
                Debug.LogWarning("More than one instance of ItemShop found");
            }

            instance = this;
        }

        #endregion

        public bool itemWasBought;

        private PlayerController _playerController;
        private Inventory _inventory;
        private Item _lastBoughtItem;
        private int _lastBoughtItemAmount;

        /**
         * Calls ElementStorage.RemoveItem() and sets 2 Variables to remember the last bought item
         */
        public override void RemoveElement(Item item, int amount) {
            base.RemoveElement(item, amount);
            if(itemWasBought) {
                _lastBoughtItem = item;
                _lastBoughtItemAmount = amount;
            }
        }

        /**
         * Undo the last purchase (refund money)
         */
        public void UndoLastPurchase() {
            if(itemWasBought) {
                _inventory = Inventory.instance;
                _playerController = PlayerController.instance;

                if(_lastBoughtItem) {
                    _playerController.ChangeMoney(_lastBoughtItem.price);
                    _inventory.RemoveElement(_lastBoughtItem, _lastBoughtItemAmount);
                    AddElement(_lastBoughtItem, _lastBoughtItemAmount);
                    itemWasBought = false;
                }
            }
        }
    }
}