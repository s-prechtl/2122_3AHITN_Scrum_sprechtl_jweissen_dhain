using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Singleton

    public static Inventory instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Inventory found");
        }

        instance = this;
    }

    #endregion
    
    public Dictionary<Item, int> items;
    public Item[] startItems;
    public const int inventorySpace = 28;
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Start() {
        items ??= new Dictionary<Item, int>();
        foreach(Item item in startItems) {
            AddItem(item, 1);
        }
    }

    public void AddItem(Item item, int amount) {
        if(items.Count >= inventorySpace) {
            Debug.Log("Not enough inventory space!");
            return;
        }

        if(!items.ContainsKey(item)) {
            items.Add(item, amount);
        } else {
            items[item] += amount;
        }
        
        
        onItemChangedCallback?.Invoke();
    }

    public void RemoveItem(Item item, int amount) {
        if(items[item] <= 0) {
            items.Remove(item);
        } else {
            items.Add(item, -amount);
        }

        onItemChangedCallback?.Invoke();
    }
}
