using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    
    private Item item;

    public void addItem(Item newItem) {
        item = newItem;

        icon.sprite = item.defaultSprite;
        icon.enabled = true;
    }

    public void clearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void removeItem() {
        PlayerController.instance.inventory.Remove(item);
    }

    public void useItem() {
        //TODO: use item
        Debug.Log("using " + item.displayName);
    }
}