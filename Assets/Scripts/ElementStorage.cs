using System.Collections.Generic;
using UnityEngine;

public class ElementStorage<T> : MonoBehaviour {
    public Dictionary<T, int> elements;
    public T[] startElements;

    /**
     * Methods can be added to this and they will get called every time onItemChangedCallback gets Invoked
     */
    public delegate void OnElementChanged();
    public OnElementChanged onElementChangedCallback;

    private void Start() {
        elements ??= new Dictionary<T, int>();
        foreach(T element in startElements) {
            AddElement(element, 1);
        }
    }

    /**
     * Adds the specified amount of elements to the Element Storage
     */
    public virtual void AddElement(T element, int amount) {
        if(!elements.ContainsKey(element)) {
            elements.Add(element, amount);
        } else {
            elements[element] += amount;
        }

        onElementChangedCallback?.Invoke();
    }

    /**
     * Removes the specified amount of elements in the Element Storage
     */
    public virtual void RemoveElement(T element, int amount) {
        if(elements[element]-amount <= 0) {
            elements.Remove(element);
        } else {
            elements[element] -= amount;
        }

        onElementChangedCallback?.Invoke();
    }
}
