using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject, IComparable<Item> {
    public string displayName;
    public string description;
    private int _id = -1;
    public Sprite selectedSprite;
    public Sprite defaultSprite;
    public int cost;

    public int ID => _id;

    public Item(string displayName, string description, int id) {
        this.displayName = displayName;
        this.description = description;
        this._id = id;
    }

    public int CompareTo(Item other) {
        return this._id - other._id;
    }

    public void SetID(int id) {
        if (_id != -1) {
            _id = id;
        }
    }
}
