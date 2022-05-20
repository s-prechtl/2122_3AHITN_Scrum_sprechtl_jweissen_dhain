using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour {
    #region Singleton

    public static ItemContainer Instance;

    private void Awake() {
        if(Instance != null) {
            Debug.LogWarning("More than one instance of ItemContainer found");
        }

        Instance = this;
    }

    #endregion
    
    public Item[] allItems;
    // Start is called before the first frame update
    public Item GetItemByName(String name) {
        for (int i = 0; i < allItems.Length; i++) {
            if (allItems[i].displayName == name) {
                return allItems[i];
            }
        }

        return null;
    }

    public int GetItemIdByName(String name) {
        return GetItemByName(name).id;
    }
}
