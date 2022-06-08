using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStorageSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Image icon;
    public TextMeshProUGUI amountText;
    
    public Item Item => _item;
    
    private Item _item;
    
    #region HoverOverItem

    public float timeToWait;

    public void OnPointerEnter(PointerEventData eventData) {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
        
        ChangeItemSelectedSprite(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        StopAllCoroutines();
        ChangeItemSelectedSprite(false);
        HoverManager.onMouseExit();
    }

    private void ShowMessage() {
        if(_item){
            HoverManager.onMouseHoverDescription(_item.description, Input.mousePosition);
        }
    }

    private void ChangeItemSelectedSprite(bool on) {
        if(_item) {
            if(on) {
                icon.sprite = _item.selectedSprite;
            } else {
                icon.sprite = _item.defaultSprite;
            }
        }
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }

    #endregion
    
    /**
     * Sets the Item of the Item Storage Slot
     */
    public void AddItem(Item newItem) {
        _item = newItem;

        icon.sprite = _item.defaultSprite;
        icon.enabled = true;
    }
    
    /**
     * Clears the Item Storage Slot
     */
    public virtual void ClearSlot() {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.text = "";
    }
    
    /**
     * Gets called when the Item Storage Slot is clicked
     */
    public virtual void UseItem() {
    }
}
