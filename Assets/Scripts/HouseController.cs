using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HouseController : MonoBehaviour {
    private int dayCount = 1;
    private static UnityEvent newDayEvent;
    public static UnityEvent NewDayEvent => newDayEvent;

    public Canvas menu;
    public TextMeshProUGUI dayCountTextMeshProUGUI;

    private void OnMouseDown() {
        toggleMenu();
    }

    void Start() {
        newDayEvent ??= new UnityEvent();
    }

    public void newDay() {
        dayCount++;
        dayCountTextMeshProUGUI.text = dayCount.ToString();
        newDayEvent?.Invoke();
    }

    public void toggleMenu() {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
    }
}