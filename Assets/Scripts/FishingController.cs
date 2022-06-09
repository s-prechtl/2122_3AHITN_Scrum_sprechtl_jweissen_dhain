using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Mail;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;
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

    public GameObject exMark;

    private double _fishingTime;
    private double _fishCooldown;
    private const float MinFishCooldown = 1.5f;
    private const float MaxFishCooldown = 7f;
    private const double MaxTime = 2f;
    private bool _fishing;
    private bool _catchable;
    private bool _caught;
    private Vector2 _ampsXY;
    private Inventory _iv;
    private ItemContainer _ic;

    public bool Fishing => _fishing;

    // Start is called before the first frame update
    void Start() {
        ResetFishing();
        _ampsXY = new Vector2(10, 10);
        _iv = Inventory.instance;
        _ic = ItemContainer.Instance;
    }

    // Update is called once per frame
    void Update() {
        if (_fishing) { //Fishing
            if (!_catchable) { // Fish not spawned yet
                _fishCooldown -= Time.deltaTime;
                if (_fishCooldown <= 0) { //fish will get spawned
                    _catchable = true;
                    if (!exMark.activeSelf) {
                        exMark.SetActive(true);
                    }
                }
            } else {
                _fishingTime += Time.deltaTime;
                NotifyShake();
            }
        }
    }

    private void NotifyShake() {
        exMark.transform.position =
            new Vector3(exMark.transform.position.x + _ampsXY.x * Time.deltaTime,
                exMark.transform.position.y + _ampsXY.y * Time.deltaTime,
                transform.position.z);
        _ampsXY.x *= -1;
        _ampsXY.y *= -1;
    }

    private void ResetFishing() {
        _fishing = false;
        _catchable = false;
        _fishingTime = 0f;
        _fishCooldown = Random.Range(MinFishCooldown, MaxFishCooldown);
        exMark.SetActive(false);
    }

    public void StartFishing() {
        if (!_iv.items.ContainsKey(_ic.GetItemByName("Bait"))) {
            Debug.Log("No bait!");
            return;
        }
        Vector3 pos = Input.mousePosition;
        
        if (Camera.main != null) {
            float newPosX = pos.x;
            float newPosY;
            
            if (pos.y - 50 - ((RectTransform)exMark.transform).rect.height >= 0) { //check if bottom of panel is in screen
                newPosY = pos.y - ((RectTransform)exMark.transform).rect.height;
            } else {
                newPosY = pos.y + ((RectTransform)exMark.transform).rect.height;
            }
        
            exMark.transform.position = new Vector3(newPosX, newPosY);
        }
        _fishing = true;

        if (Random.Range(0, 10) > 5) { //uses bait to certain chance
            _iv.RemoveItem(_ic.GetItemByName("Bait"), 1);
        }
        
    }

    public void TryCatch() {
        if (_fishing && _catchable) {
            Debug.Log("Tried to catch!");
            if (_fishingTime <= MaxTime) {
                Debug.Log("Caught!");
                _iv.AddItem(_ic.GetItemByName("Fish"), Math.Max((int)(1 / (_fishingTime/2)), 1));
                ResetFishing();
            } else {
                Debug.Log("Failed to catch!");
                _catchable = false;
                _fishingTime = 0f;
                exMark.SetActive(false);
                _fishCooldown = Random.Range(MinFishCooldown+2, MaxFishCooldown);
            }
        }
    }
}