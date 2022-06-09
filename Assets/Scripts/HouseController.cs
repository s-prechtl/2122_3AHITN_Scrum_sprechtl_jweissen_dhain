using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HouseController : MonoBehaviour {
    private static int _dayCount = 1;
    private static UnityEvent _newDayEvent;
    public static UnityEvent NewDayEvent => _newDayEvent;
    public static int DayCount => _dayCount;

    public Canvas menu;
    public TextMeshProUGUI dayCountTextMeshProUGUI;
    public GameObject menuPanel;

    private void OnMouseDown() {
        ToggleMenu();
    }

    private void Awake() {
        _newDayEvent ??= new UnityEvent();
    }

    void Start() {
        ToggleMenu();
    }

    public void NewDay() {
        _dayCount++;
        dayCountTextMeshProUGUI.text = _dayCount.ToString();
        _newDayEvent?.Invoke();
    }

    public void ToggleMenu() {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);

        if (Camera.main != null) {
            Vector3 pos =  Camera.main.WorldToScreenPoint(transform.position);
            float newPosX = pos.x;
            float newPosY;
            
            if (pos.y - 50 - ((RectTransform)menuPanel.transform).rect.height >= 0) { //check if bottom of panel is in screen
                newPosY = pos.y - ((RectTransform)menuPanel.transform).rect.height;
            } else {
                newPosY = pos.y + ((RectTransform)menuPanel.transform).rect.height;
            }
        
            menuPanel.transform.position = new Vector3(newPosX, newPosY);
        }
    }
}