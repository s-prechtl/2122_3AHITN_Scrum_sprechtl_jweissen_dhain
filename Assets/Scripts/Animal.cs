using System;
using System.Collections;
using Actions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private int _animatorMoveID;

    public int SellPrice => Convert.ToInt32(price * 0.8);
    
    public Item producedItem;
    public int movementSpeed;
    public GameObject animalPrefab;
    public Sprite defaultSprite;
    public Sprite selectedSprite;
    public string displayName;
    public string description;
    public int price;

    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _animatorMoveID = Animator.StringToHash("moving");
        
        _spriteRenderer.sprite = defaultSprite;
        
        // Move the Animal in any random direction every 1-5s
        InvokeRepeating(nameof(MoveInRandomDirection), 2f, Random.Range(1f, 5f));
    }

    /**
     * Moves the Animal in any random direction for a random amount of time
     */
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
            _animator.SetBool(_animatorMoveID, true);
            yield return new WaitForSeconds(randTime);
            _rigidbody.velocity = new Vector2(0f, 0f); 
            _animator.SetBool(_animatorMoveID, false);
        }
        
        StartCoroutine(Move());
    }
}