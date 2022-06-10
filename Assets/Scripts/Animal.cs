using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour {
    protected Sprite animalSprite;
    private Item _producedItem;
    private Rigidbody2D _rigidbody;
    public Item ProducedItem => _producedItem;

    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animalSprite = gameObject.GetComponent<SpriteRenderer>().GetComponent<Sprite>();
        
        _rigidbody.velocity = new Vector2(Random.Range(1, 10),
            Random.Range(1, 10));
    }

    private void Update() {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * Random.Range(0, 10),
            _rigidbody.velocity.y * Random.Range(0, 10));// TODO: wer?
    }

    // TODO: Animations
    
    private void OnMouseDown() {
        ActionInvoker.InvokeAction(gameObject, PlayerController.instance.SelectedItem);
    }
}
