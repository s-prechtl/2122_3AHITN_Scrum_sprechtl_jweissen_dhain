using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Image icon;
    public TextMeshProUGUI amountText;

    public Item _item;
    
    #region DescriptionHover

    public float timeToWait;

    public void OnPointerEnter(PointerEventData eventData) {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData) {
        StopAllCoroutines();
        HoverManager.onMouseExit();
    }

    private void ShowMessage() {
        if(_item){
            HoverManager.onMouseHover(_item.description, Input.mousePosition);
        }
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }

    #endregion
    
    /**
     * Sets the Item of the Inventory Slot
     */
    public void AddItem(Item newItem) {
        _item = newItem;

        icon.sprite = _item.defaultSprite;
        icon.enabled = true;
    }

    /**
     * Clears the Inventory Slot
     */
    public void ClearSlot() {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    /**
     * Gets called when the Inventory Slot is clicked
     */
    public void UseItem() {
        if(_item.GetType() == typeof(UsableItem)) {
            ((UsableItem) _item).Select();
            Debug.Log("using " + _item.displayName);
        } else {
            Debug.Log("Item not usable " + _item.displayName);
        }
    }
}