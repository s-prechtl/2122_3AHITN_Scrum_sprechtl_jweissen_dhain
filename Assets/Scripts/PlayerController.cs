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

    public int startMoney = 100;
    public TextMeshProUGUI moneyTextMeshProUGUI;

    public int Money => _money;

    // Start is called before the first frame update
    private void Start() {
        _money = startMoney;
        _inventory = Inventory.instance;
        moneyTextMeshProUGUI.text = _money + "µ";
    }

    public void SetSelectedItem(UsableItem item) {
        if(_inventory.items.ContainsKey(item)) {
            _selectedItem = item;
            Cursor.SetCursor(item.selectedSprite.texture, Vector2.zero, CursorMode.Auto);
        } else {
            Debug.Log("An item requested to select isn't in the inventory" + item);
        }
    }

    public UsableItem GetSelectedItem() {
        return _selectedItem;
    }

    public void ChangeMoney(int amount) {
        _money += amount;
        moneyTextMeshProUGUI.text = _money + "µ";
    }
}