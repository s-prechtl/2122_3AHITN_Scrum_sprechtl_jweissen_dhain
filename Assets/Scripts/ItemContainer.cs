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

    void Start() {
        _allItems ??= new List<Item>();
        Debug.Log("Itemcontainer started");
        string[] files =
            Directory.GetFiles("Assets\\Resources\\Items", "*.asset", SearchOption.AllDirectories);
        foreach (string file in files) {
            String path = file.Replace("Assets\\Resources\\", "").Replace(".asset", "");
           _allItems.Add(Resources.Load<Item>(path));
        }
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
