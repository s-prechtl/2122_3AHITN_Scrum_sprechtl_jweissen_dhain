using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Mail;
using System.Threading;
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

    public GameObject _exMark;

    private double _fishingTime;
    private double _fishCooldown;
    private readonly float _minFishCooldown = 1.5f;
    private readonly float _maxFishCooldown = 7f;
    private readonly double _maxTime = 2f;
    private bool _fishing;
    private bool _catchable;
    private bool _caught;
    private Vector2 _ampsXY;

    public bool Fishing => _fishing;

    // Start is called before the first frame update
    void Start() {
        ResetFishing();
        _ampsXY = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update() {
        if (_fishing) { //Fishing
            if (!_catchable) { // Fish not spawned yet
                _fishCooldown -= Time.deltaTime;
                if (_fishCooldown <= 0) { //fish will get spawned
                    _catchable = true;
                    if (!_exMark.activeSelf) {
                        _exMark.SetActive(true);
                    }
                }
            } else {
                _fishingTime += Time.deltaTime;
                NotifyShake();
            }
        }
    }

    private void NotifyShake() {
        _exMark.transform.position =
            new Vector3(_exMark.transform.position.x + _ampsXY.x * Time.deltaTime,
                _exMark.transform.position.y + _ampsXY.y * Time.deltaTime,
                transform.position.z);
        _ampsXY.x *= -1;
        _ampsXY.y *= -1;
    }

    private void ResetFishing() {
        _fishing = false;
        _catchable = false;
        _fishingTime = 0f;
        _fishCooldown = Random.Range(_minFishCooldown, _maxFishCooldown);
        _exMark.SetActive(false);
    }

    public void StartFishing() {
        Vector3 pos = Input.mousePosition;
        
        if (Camera.main != null) {
            float newPosX = pos.x;
            float newPosY;
            
            if (pos.y - 50 - ((RectTransform)_exMark.transform).rect.height >= 0) { //check if bottom of panel is in screen
                newPosY = pos.y - ((RectTransform)_exMark.transform).rect.height;
            } else {
                newPosY = pos.y + ((RectTransform)_exMark.transform).rect.height;
            }
        
            _exMark.transform.position = new Vector3(newPosX, newPosY);
        }
        _fishing = true;
    }

    public void TryCatch() {
        if (_fishing && _catchable) {
            Debug.Log("Tried to catch!");
            if (_fishingTime <= _maxTime) {
                Debug.Log("Caught!");
                Inventory.instance.AddItem(ItemContainer.Instance.GetItemByName("Fish"), Math.Max((int)(1 / (_fishingTime/2)), 1));
                ResetFishing();
            } else {
                Debug.Log("Failed to catch!");
                _catchable = false;
                _fishingTime = 0f;
                _exMark.SetActive(false);
                _fishCooldown = Random.Range(_minFishCooldown+2, _maxFishCooldown);
            }
        }
    }
}