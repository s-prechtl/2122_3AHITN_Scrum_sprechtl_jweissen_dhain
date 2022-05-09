using UnityEngine;

public class Item : MonoBehaviour {
    private string displayName;
    private string description;
    private int id;  //TODO: create an actual ID System that makes snens
    public SpriteRenderer spriteRenderer;
    public Sprite selectedSprite;
    public Sprite defaultSprite;

    public Item(string displayName, string description, int id) {
        this.displayName = displayName;
        this.description = description;
        this.id = id;
        spriteRenderer.sprite ??= defaultSprite;   // defaultSprite is set in UnityEditor
    }
}
