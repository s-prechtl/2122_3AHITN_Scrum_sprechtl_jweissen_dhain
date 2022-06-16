using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    
    public Item producedItem;
    public Sprite animalSprite;
    public int movementSpeed;
    
    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animalSprite = gameObject.GetComponent<SpriteRenderer>().GetComponent<Sprite>();
        
        // Move the Animal in any random direction every 1-5s
        InvokeRepeating(nameof(MoveInRandomDirection), 2f, Random.Range(1f, 5f));
    }
    
    // Moves the Animal in any random direction for a random amount of time
    private void MoveInRandomDirection() {
        IEnumerator Move() { 
            float randTime = Random.Range(0.5f, 1f);
        
            _rigidbody.rotation = 0f;
            Vector2 direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f));
            direction.Normalize();
        
            _rigidbody.velocity = movementSpeed * direction;
            StopAllCoroutines();
            yield return new WaitForSeconds(randTime);
            _rigidbody.velocity = new Vector2(0f, 0f); }
        
        StartCoroutine(Move());
    }

    // TODO: Animations

    private void OnMouseDown() {
        ActionInvoker.InvokeAction(gameObject, PlayerController.instance.SelectedItem);
    }
}