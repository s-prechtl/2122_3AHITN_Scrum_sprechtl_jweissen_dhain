using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerController : MonoBehaviour {
    #region Singleton

    public static PlayerController instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of PlayerController found");
        }

        instance = this;
    }

    #endregion

    private Inventory _inventory;
    private int _money;
    private UsableItem _selectedItem;

    public int startMoney;
    public TextMeshProUGUI moneyTextMeshProUGUI;
    
    public int Money => _money;

    public UsableItem SelectedItem {
        get => _selectedItem;
        set {
            if(_inventory.elements.ContainsKey(value)) {
                _selectedItem = value;
                Cursor.SetCursor(value.defaultSprite.texture, Vector2.zero, CursorMode.Auto);
            } else {
                //Debug.Log("An item requested to select isn't in the inventory" + item);
            }
        }
    }
    
    public delegate void OnMoneyChanged();
    public OnMoneyChanged onMoneyChangedCallback;

    // Start is called before the first frame update
    private void Start() {
        _inventory = Inventory.instance;
        _money = startMoney;
        UpdateMoneyUI();

        onMoneyChangedCallback += UpdateMoneyUI;
    }

    public void DeselectItem() {
        _selectedItem = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ChangeMoney(int amount) {
        _money += amount;

        onMoneyChangedCallback?.Invoke();
    }

    private void UpdateMoneyUI() {
        moneyTextMeshProUGUI.text = _money + "µ";
    }
}
