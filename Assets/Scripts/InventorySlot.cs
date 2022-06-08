using UnityEngine;

public class InventorySlot : ItemStorageSlot {
    /**
     * Gets called when the Inventory Slot is clicked
     */
    public override void UseItem() {
        if(Item){
            if(Item.GetType() == typeof(UsableItem)) {
                ((UsableItem)Item).Select();
                Debug.Log("using " + Item.displayName);
            } else {
                Debug.Log("Item not usable " + Item.displayName);
            }
        }
    }
}