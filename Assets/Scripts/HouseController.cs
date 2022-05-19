using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HouseController : MonoBehaviour {
    private int dayCount = 0;
    private static UnityEvent newDayEvent;
    public static UnityEvent NewDayEvent => newDayEvent;

    public Canvas menu;

    private void OnMouseDown() {
        toggleMenu();
    }

    void Start() {
        newDayEvent ??= new UnityEvent();
    }

    public void newDay() {
        dayCount++;
        Debug.Log("New day: " + dayCount);
        newDayEvent?.Invoke();
    }

    public void toggleMenu() {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
    }
}