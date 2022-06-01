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
    public const int InventorySpace = 28;
    
    /**
     * Methods can be added to this and they will get called every time onItemChangedCallback gets Invoked
     */
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Start() {
        items ??= new Dictionary<Item, int>();
        foreach(Item item in startItems) {
            AddItem(item, 1);
        }
    }

    /**
     * Adds the specified amount of items to the Inventory
     */
    public void AddItem(Item item, int amount) {
        if(items.Count >= InventorySpace) {
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
    
    /**
     * Removes the specified amount of items in the Inventory
     */
    public void RemoveItem(Item item, int amount) {
        if(items[item] <= 0) {
            items.Remove(item);
        } else {
            items[item] -= amount;
        }

        onItemChangedCallback?.Invoke();
    }
}
