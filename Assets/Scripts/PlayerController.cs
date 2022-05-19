using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region Singleton

    public static PlayerController instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of PlayerController found");
        }

        instance = this;
    }

    #endregion

    public Inventory inventory;
    private int money;
    private UsableItem selectedItem;

    public int startMoney = 100;

    // Start is called before the first frame update
    void Start() {
        money = startMoney;
    }

    // Update is called once per frame
    void Update() { }

    public void SetSelectedItem(UsableItem item) {
        if(inventory.items.ContainsKey(item)) {
            selectedItem = item;
            Cursor.SetCursor(item.defaultSprite.texture, Vector2.zero, CursorMode.Auto);
        } else {
            Debug.Log("An item requested to select isn't in the inventory" + item);
        }
    }
}