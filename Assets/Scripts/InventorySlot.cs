using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    
    private Item _item;

    public void AddItem(Item newItem) {
        _item = newItem;

        icon.sprite = _item.defaultSprite;
        icon.enabled = true;
    }

    public void ClearSlot() {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void RemoveItem() {
        Inventory.instance.items.Remove(_item);
    }

    public void UseItem() {
        if(_item.GetType() == typeof(UsableItem)) {
            ((UsableItem) _item).select();
            Debug.Log("using " + _item.displayName);
        } else {
            Debug.Log("Item not usable " + _item.displayName);
        }

    }
}