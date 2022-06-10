using DefaultNamespace;
using UnityEngine;

public class Animal : MonoBehaviour {
    protected Sprite animalSprite;
    private Item producedItem;

    protected Animal(Item producedItem) {
        this.producedItem = producedItem;
    }
    
    // TODO: Movement
    
    // TODO: Animations
    
    private void OnMouseDown() {
        ActionInvoker.InvokeAction(gameObject, PlayerController.instance.SelectedItem);
    }
}
