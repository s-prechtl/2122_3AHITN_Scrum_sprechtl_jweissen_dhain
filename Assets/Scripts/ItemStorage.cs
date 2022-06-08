using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour {
    public Dictionary<Item, int> items;
    public Item[] startItems;

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
     * Adds the specified amount of items to the Item Storage
     */
    public virtual void AddItem(Item item, int amount) {
        if(!items.ContainsKey(item)) {
            items.Add(item, amount);
        } else {
            items[item] += amount;
        }

        onItemChangedCallback?.Invoke();
    }

    /**
     * Removes the specified amount of items in the Item Storage
     */
    public virtual void RemoveItem(Item item, int amount) {
        if(items[item]-amount <= 0) {
            items.Remove(item);
        } else {
            items[item] -= amount;
        }

        onItemChangedCallback?.Invoke();
    }
}
