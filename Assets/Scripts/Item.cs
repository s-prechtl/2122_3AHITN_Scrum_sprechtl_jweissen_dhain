using System;
using UnityEngine;

//TODO: Auf ScriptableItem umschreiben!!!!!!!!!!
//
//https://www.youtube.com/watch?v=YLhj7SfaxSE
public class Item : MonoBehaviour, IComparable<Item> {
    public readonly string displayName;
    public readonly string description;
    public readonly int id;  //TODO: create an actual ID System that makes snens
    public SpriteRenderer spriteRenderer;
    public Sprite selectedSprite;
    public Sprite defaultSprite;
    
    public Item(string displayName, string description, int id) {
        this.displayName = displayName;
        this.description = description;
        this.id = id;
        spriteRenderer.sprite ??= defaultSprite;   // defaultSprite is set in UnityEditor
    }

    public int CompareTo(Item other) {
        return this.id - other.id;
    }
}
