using System.Collections.Generic;
using UnityEngine;

public class Shop : ItemStorage {
    #region Singleton

    public static Shop instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Shop found");
        }

        instance = this;
    }

    #endregion
}