using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayController : MonoBehaviour {
    private int dayCount = 0;
    private static UnityEvent newDayEvent;
    
    private void OnMouseDown() {
        newDay();
    }

    void Start() {
        newDayEvent ??= new UnityEvent();

        newDayEvent.AddListener(newDay);
    }

    private void newDay() {
        dayCount++;
        newDayEvent?.Invoke();
    }
}