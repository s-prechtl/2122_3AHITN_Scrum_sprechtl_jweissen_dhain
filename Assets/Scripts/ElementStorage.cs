using System.Collections.Generic;
using UnityEngine;

public class ElementStorage<T> : MonoBehaviour {
    private Dictionary<T, int> _elements;
    public Dictionary<T, int> Elements => _elements;
    public T[] startElements;

    /**
     * Methods can be added to this and they will get called every time onElementChangedCallback gets Invoked
     */
    public delegate void OnElementChanged();
    public OnElementChanged onElementChangedCallback;

    private void Start() {
        _elements ??= new Dictionary<T, int>();
        foreach(T element in startElements) {
            AddElement(element, 1);
        }
    }

    /**
     * Adds the specified amount of elements to the Element Storage
     */
    public virtual void AddElement(T element, int amount) {
        if(!_elements.ContainsKey(element)) {
            _elements.Add(element, amount);
        } else {
            _elements[element] += amount;
        }

        onElementChangedCallback?.Invoke();
    }

    /**
     * Removes the specified amount of elements in the Element Storage
     */
    public virtual void RemoveElement(T element, int amount) {
        if(_elements[element]-amount <= 0) {
            _elements.Remove(element);
        } else {
            _elements[element] -= amount;
        }

        onElementChangedCallback?.Invoke();
    }
}
