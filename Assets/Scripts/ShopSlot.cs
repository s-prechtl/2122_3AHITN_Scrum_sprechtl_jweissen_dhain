using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Image icon;
    public Item item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI amountText;
    
    private Shop _shop;
    private Inventory _inventory;
    private PlayerController _playerController;
    
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
        if(item) {
            HoverManager.onMouseHover(item.description, Input.mousePosition);
        }
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }

    #endregion

    private void Start() {
        _shop = Shop.instance;
        _inventory = Inventory.instance;
        _playerController = PlayerController.instance;
    }

    /**
     * Sets the Item of the Shop Slot
     */
    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.defaultSprite;
        icon.enabled = true;
    }
    
    /**
     * Clears the Shop Slot
     */
    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        nameText.text = "";
        costText.text = "";
        amountText.text = "";
    }

    /**
     * Gets called when the Shop Slot is clicked
     */
    public void UseItem() {
        if(_playerController.Money >= item.cost) {
            _inventory.AddItem(item, 1);
            _shop.RemoveItem(item, 1);
            _playerController.ChangeMoney(-item.cost);
            
            Debug.Log("Buying Item: " + item.displayName);
            Debug.Log("money left: " + _playerController.Money);
        } else {
            Debug.Log("Not enough money to buy item.");
        }
        
        _shop.onItemChangedCallback?.Invoke();
        _inventory.onItemChangedCallback?.Invoke();
    }
}