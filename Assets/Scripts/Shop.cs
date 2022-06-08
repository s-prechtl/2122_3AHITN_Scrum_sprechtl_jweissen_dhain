using UnityEngine;

public class Shop : ItemStorage {
    #region Singleton

    public static Shop instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Shop found");
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
     * Calls ItemStorage.RemoveItem() and sets 2 Variables to remember the last bought item
     */
    public override void RemoveItem(Item item, int amount) {
        base.RemoveItem(item, amount);
        if(itemWasBought){
            _lastBoughtItem = item;
            _lastBoughtItemAmount = amount;
        }
    }
    
    public void UndoLastPurchase() {
        if(itemWasBought){
            _inventory = Inventory.instance;
            _playerController = PlayerController.instance;

            if(_lastBoughtItem) {
                _playerController.ChangeMoney(_lastBoughtItem.cost);
                _inventory.RemoveItem(_lastBoughtItem, _lastBoughtItemAmount);
                AddItem(_lastBoughtItem, _lastBoughtItemAmount);
                itemWasBought = false;
            }
        }
    }
}