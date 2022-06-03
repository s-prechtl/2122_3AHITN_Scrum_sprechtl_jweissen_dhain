using UnityEngine;

public class Inventory : ItemStorage {
    #region Singleton

    public static Inventory instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Inventory found");
        }

        instance = this;
    }

    #endregion
    
    private const int _InventorySpace = 28;
    
    /**
     * Adds the specified amount of items to the Inventory
     */
    public override void AddItem(Item item, int amount) {
        if(items.Count >= _InventorySpace) {
            Debug.Log("Not enough inventory space!");
            return;
        }
        
        base.AddItem(item, amount);
    }
}
