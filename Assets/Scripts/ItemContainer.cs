using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    private List<Item> _allItems;

    private void Start() {
        string[] files = 
            Directory.GetFiles("Assets/Items", "*.cs", SearchOption.AllDirectories);
        foreach (string file in files) {
           _allItems.Add(Resources.Load<Item>("Assets/Items/" + file));            
        }
        Debug.Log(files);
        Debug.Log(_allItems);
    }

    public Item GetItemByName(String name) {
        for (int i = 0; i < _allItems.Count; i++) {
            if (_allItems[i].displayName == name) {
                return _allItems[i];
            }
        }

        return null;
    }

    public int GetItemIdByName(String name) {
        return GetItemByName(name).id;
    }
}
