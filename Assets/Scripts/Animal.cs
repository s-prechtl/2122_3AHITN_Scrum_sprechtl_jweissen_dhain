using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour {
    private Item _producedItem;
    private Rigidbody2D _rigidbody;
    
    public Item ProducedItem => _producedItem;
    
    public Sprite animalSprite;
    public int movementSpeed;
    
    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animalSprite = gameObject.GetComponent<SpriteRenderer>().GetComponent<Sprite>();
    }

    private void Update() {
        _rigidbody.rotation = 0f;
        
        Vector2 direction = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));
        direction.Normalize();
        _rigidbody.velocity = movementSpeed * direction;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Vector2 oldPos = _rigidbody.position;
        //TODO: collide with edges working but no stopping
        string[] colNames = { "Top", "Bottom", "Left", "Right" };
        foreach(string colName in colNames) {
            if(colName.ToUpper().Equals(col.gameObject.name.ToUpper())) {
                Debug.Log("EEEEEEEE   " + col.gameObject.name);
                _rigidbody.position = oldPos;
            }
        }
    }

    // TODO: Animations

    private void OnMouseDown() {
        ActionInvoker.InvokeAction(gameObject, PlayerController.instance.SelectedItem);
    }
}