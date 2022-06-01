using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    #region Singleton

    public static Shop instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Shop found");
        }

        instance = this;
    }

    #endregion
    
    public Dictionary<Item, int> items;
    public Item[] tempItems;
    
    /**
     * Methods can be added to this and they will get called every time onItemChangedCallback gets Invoked
     */
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    private void Start() {
        items ??= new Dictionary<Item, int>();
        foreach(Item item in tempItems) {
            AddItem(item, 1);
        }
    }
    
    /**
     * Adds the specified amount of items to the Shop
     */
    public void AddItem(Item item, int amount) {
        if(!items.ContainsKey(item)) {
            items.Add(item, amount);
        } else {
            items[item] += amount;
        }

        onItemChangedCallback?.Invoke();
    }
    // TODO: add to buy more than one item
    /**
     * Removes the specified amount of items in the Shop
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