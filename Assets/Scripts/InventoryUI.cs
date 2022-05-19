using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    private Inventory _inventory;
    private InventorySlot[] _slots;
    
    // Start is called before the first frame update
    void Start() {
        _inventory = Inventory.instance;
        _inventory.onItemChangedCallback += UpdateUI;

        _slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        toggleInventory();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("Inventory")) {
            toggleInventory();
        }
    }

    private void toggleInventory() {
        
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    
    private void UpdateUI() {
        for(int i = 0; i < _slots.Length; i++) {
            if(i < _inventory.items.Count) {
                _slots[i].AddItem(_inventory.items.ElementAt(i).Key);
            } else {
                _slots[i].ClearSlot();
            }
        }
    }
}