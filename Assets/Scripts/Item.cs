using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject, IComparable<Item> {
    public string displayName;
    public string description;
    private int _id = -1;
    public Sprite selectedSprite;
    public Sprite defaultSprite;
    public int price;
    public int SellPrice => Convert.ToInt32(price * 0.8);
    public int ID => _id;

    public Item(string displayName, string description, int id) {
        this.displayName = displayName;
        this.description = description;
        _id = id;
    }

    public int CompareTo(Item other) {
        return _id - other._id;
    }

    public void SetID(int id) {
        if (_id != -1) {
            _id = id;
        }
    }
}
