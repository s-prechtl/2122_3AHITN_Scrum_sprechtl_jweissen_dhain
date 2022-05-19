using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Dictionary<Item, int> inventory;
    private int money;
    private UsableItem selectedItem;

    public UsableItem SelectedItem => selectedItem;

    private static PlayerController instance;
    
    public int startMoney = 100;

    public static PlayerController getInstance() {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory ??= new Dictionary<Item, int>();
        money = startMoney;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelectedItem(UsableItem item) {
        if (inventory.ContainsKey(item)) {
            selectedItem = item;
            Cursor.SetCursor(item.defaultSprite.texture,  Vector2.zero, CursorMode.Auto);
        } else {
          Debug.Log("An item requested to select isn't in the inventory" + item);  
        }
    }
    
    
}
