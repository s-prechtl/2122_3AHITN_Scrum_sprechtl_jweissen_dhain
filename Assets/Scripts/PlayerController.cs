using System;
using System.Collections;
using System.Collections.Generic;
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
    private int money;
    private UsableItem selectedItem;

    public int startMoney = 100;

    // Start is called before the first frame update
    void Start() {
        money = startMoney;
        _inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update() { }

    public void SetSelectedItem(UsableItem item) {
        if(_inventory.items.ContainsKey(item)) {
            selectedItem = item;
            Cursor.SetCursor(item.defaultSprite.texture, Vector2.zero, CursorMode.Auto);
        } else {
            Debug.Log("An item requested to select isn't in the inventory" + item);
        }
    }

    public UsableItem GetSelectedItem() {
        return selectedItem;
    }
}