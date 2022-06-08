using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishingController : MonoBehaviour {
    #region Singleton

    public static FishingController instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of FishingController found");
        }

        instance = this;
    }

    #endregion

    private double _fishingTime;
    private bool _fishing;
    private bool _catchable;
    private bool _caught;
    private double _fishCooldown;
    private readonly double _totalFishCooldown = 1.5f;
    private readonly double _maxTime = 2f;
    private bool _spawnFish;

    public bool Fishing => _fishing;

    // Start is called before the first frame update
    void Start() {
        ResetFishing();
    }

    // Update is called once per frame
    void Update() {
        if (_fishing) {
            if (!_catchable) {
                _fishCooldown += Time.deltaTime;
                if (_spawnFish && _fishCooldown >= _totalFishCooldown && Random.Range(0, 100) <= 1) {
                    _spawnFish = false;
                    //particles for fish -> catchable true
                    
                }
            } else {
                if (!_caught) {
                    _fishingTime += Time.deltaTime;
                }
            }
        }
    }

    private void ResetFishing() {
        _fishing = false;
        _catchable = false;
        _fishingTime = 0f;
        _fishCooldown = 1.5f;
        _spawnFish = true;
    }

    public void StartFishing() {
        _fishing = true;
    }

    public void TryCatch() {
        if (_fishing && _catchable) {
            if (_fishingTime <= _maxTime) {
                _fishing = false;
                _caught = true;
                Inventory.instance.AddItem(ItemContainer.Instance.GetItemByName("Fish"), (int)(3/_fishingTime));
            } else {
                _spawnFish = true;
                _catchable = false;
                _fishingTime = 0f;
            }
        }
    }
}