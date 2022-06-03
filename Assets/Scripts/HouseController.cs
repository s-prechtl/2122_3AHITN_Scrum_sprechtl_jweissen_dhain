using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HouseController : MonoBehaviour {
    private int _dayCount = 1;
    private static UnityEvent _newDayEvent;
    public static UnityEvent NewDayEvent => _newDayEvent;

    public Canvas menu;
    public TextMeshProUGUI dayCountTextMeshProUGUI;

    private void OnMouseDown() {
        ToggleMenu();
    }

    void Start() {
        _newDayEvent ??= new UnityEvent();
        ToggleMenu();
    }

    public void NewDay() {
        _dayCount++;
        dayCountTextMeshProUGUI.text = _dayCount.ToString();
        _newDayEvent?.Invoke();
    }

    public void ToggleMenu() {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
    }
}