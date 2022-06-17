using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementStorageSlot<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Image icon;
    public TextMeshProUGUI amountText;

    public T Element => _element;

    private T _element;

    #region HoverOverSlot

    public float timeToWait;

    public void OnPointerEnter(PointerEventData eventData) {
        StopAllCoroutines();
        StartCoroutine(StartTimer());

        ChangeElementSelectedSprite(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        StopAllCoroutines();
        ChangeElementSelectedSprite(false);
        HoverManager.onMouseExit();
    }

    private void ShowMessage() {
        if(_element is Item) { //TODO: add animal description showing
            Item item = (Item)(object)_element;
            HoverManager.onMouseHoverDescription(item.description, Input.mousePosition);
        }
    }

    public void ChangeElementSelectedSprite(bool on) {
        if(_element is Item) { //TODO: add animal sprite change
            Item item = (Item)(object)_element;
            if(on) {
                icon.sprite = item.selectedSprite;
            } else {
                icon.sprite = item.defaultSprite;
            }
        }
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }

    #endregion

    /**
     * Sets the Element of the Element Storage Slot
     */
    public void AddElement(T newElement) {
        _element = newElement;

        if(_element is Item) { //TODO: add animal sprite change
            Item item = (Item)(object)_element;
            icon.sprite = item.defaultSprite;
        }

        icon.enabled = true;
    }

    /**
     * Clears the Element Storage Slot
     */
    public virtual void ClearSlot() {
        _element = default(T);
        icon.sprite = null;
        icon.enabled = false;
        amountText.text = "";
    }

    /**
     * Gets called when the Element Storage Slot is clicked
     */
    public virtual void UseElement() { }
}