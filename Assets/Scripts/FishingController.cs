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
    private double fishCooldown {
        get => _fishCooldown;
        set {
            _fishCooldown = value;
            exMark.SetActive(Catchable);
        }
    }

    private const float MinFishCooldown = 1.5f;
    private const float MaxFishCooldown = 7f;
    private const double MaxTime = 2f;
    private bool _fishing;
    private const int MaxFishPerDay = 3;
    private int _fishedThisDay;
    private bool Catchable => fishCooldown <= 0;
    private bool _caught;
    private bool Fishable => _fishedThisDay <= MaxFishPerDay;
    private Vector2 _ampsXY;
    private Inventory _iv;
    private ItemContainer _ic;
    private MessageView _messageView;

    public bool Fishing => _fishing;

    // Start is called before the first frame update
    void Start() {
        _messageView = MessageView.instance;
        ResetFishing();
        _ampsXY = new Vector2(10, 10);
        _iv = Inventory.instance;
        _ic = ItemContainer.Instance;
        _fishedThisDay = 0;
        HouseController.NewDayEvent.AddListener(ResetPond);
    }

    // Update is called once per frame
    void Update() {
        if (_fishing) { //Fishing
            if (!Catchable) {
                // Fish not spawned yet
                fishCooldown -= Time.deltaTime;
            } else {
                _fishingTime += Time.deltaTime;
                NotifyShake();
            }
        }
    }

    private void ResetPond() {
        _fishedThisDay = 0;
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
        _fishingTime = 0f;
        fishCooldown = Random.Range(MinFishCooldown, MaxFishCooldown);
        exMark.SetActive(false);
    }

    public void StartFishing() {
        if (!_iv.items.ContainsKey(_ic.GetItemByName("Bait"))) {
            _messageView.SendMessage("No bait!", 1.0f);
            return;
        }
        
        if (!Fishable) {
            _messageView.SendMessage("You cannot fish anymore for today!", 1.0f);
            return;
        }
        _iv.RemoveItem(_ic.GetItemByName("Bait"), 1);
        _fishedThisDay++;
        
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
        
        _messageView.SendMessage("Fishing started", 1.0f);
        
    }

    public void TryCatch() {
        if (_fishing && Catchable) {
            if (_fishingTime <= MaxTime) {
                _messageView.SendMessage("Caught!", 1.5f);
                _iv.AddItem(_ic.GetItemByName("Fish"), Math.Max((int)(1 / (_fishingTime / 2)), 1));
            } else {
                _messageView.SendMessage("Failed to catch the fish! You were too slow!", 1.5f);
            }
            ResetFishing();
        }
    }
}