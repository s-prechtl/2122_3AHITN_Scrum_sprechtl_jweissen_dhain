using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Sprite _animalSprite;
    private Animator _animator;
    private int _animMoveID;

    public Sprite AnimalSprite => _animalSprite;
    
    public Item producedItem;
    public int movementSpeed;

    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animalSprite = _spriteRenderer.GetComponent<Sprite>();
        _animator = gameObject.GetComponent<Animator>();
        _animMoveID = Animator.StringToHash("moving");
        
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
            
            // Flip sprite in moving Direction
            if(direction.x > 0) {
                _spriteRenderer.flipX = true;
            }else if(direction.x < 0) {
                _spriteRenderer.flipX = false;
            }
            
            _rigidbody.velocity = movementSpeed * direction;
            _animator.SetBool(_animMoveID, true);
            yield return new WaitForSeconds(randTime);
            _rigidbody.velocity = new Vector2(0f, 0f); 
            _animator.SetBool(_animMoveID, false);
        }
        
        StartCoroutine(Move());
    }

    private void OnMouseDown() {
        ActionInvoker.InvokeAction(gameObject, PlayerController.instance.SelectedItem);
    }
}