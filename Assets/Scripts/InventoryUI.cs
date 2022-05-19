using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    private PlayerController playerController;
    private InventorySlot[] slots;
    
    // Start is called before the first frame update
    void Start() {
        playerController = PlayerController.instance;
        playerController.onItemChangedCallback += updateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    
    private void updateUI() {
        for(int i = 0; i < slots.Length; i++) {
            if(i < playerController.inventory.Count) {
                // slots[i].addItem(playerController.inventory[i]); //TODO: dictionary "letztes" Item finden, Wie?!?!?!
            } else {
                slots[i].clearSlot();
            }
        }
    }
}