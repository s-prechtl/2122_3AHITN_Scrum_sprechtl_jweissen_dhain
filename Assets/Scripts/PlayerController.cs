using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private List<Item> inventory;
    private int money;
    private UsableItem selectedItem;

    private static PlayerController instance;

    public int startMoney = 100;

    public static PlayerController getInstance() {
        return instance;
    }


    // Start is called before the first frame update
    void Start() {
        inventory ??= new List<Item>();
        money = startMoney;
        instance = this;
    }

    // Update is called once per frame
    void Update() {
    }

    public void setSelectedItem(UsableItem item) {
        selectedItem = item;
    }
}
