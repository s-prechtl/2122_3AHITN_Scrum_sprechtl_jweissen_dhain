using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UsableItem", menuName = "Inventory/UsableItem")]
public class UsableItem : Item, IUsable {
    public UsableItem(string displayName, string description, int id) : base(displayName, description, id) {
    }

    public void Select() {
        PlayerController.instance.SelectedItem = this;
    }
}