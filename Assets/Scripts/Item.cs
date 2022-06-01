using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject, IComparable<Item> {
    public string displayName;
    public string description;
    public int id;  //TODO: create an actual ID System that makes snens
    public Sprite selectedSprite;
    public Sprite defaultSprite;
    public int cost;
    
    public Item(string displayName, string description, int id) {
        this.displayName = displayName;
        this.description = description;
        this.id = id;
    }

    public int CompareTo(Item other) {
        return this.id - other.id;
    }
}
