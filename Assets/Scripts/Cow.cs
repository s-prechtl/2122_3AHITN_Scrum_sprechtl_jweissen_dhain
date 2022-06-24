using Actions;
using UnityEngine;

public class Cow : Animal {
    private bool _canBeMilked;

    private void Awake() {
        _canBeMilked = true;
        HouseController.NewDayEvent.AddListener(NextDay);
    }

    private void NextDay() {
        UpdateCanBeMilked();
        //ActionManager.Instance.NextDayAction(gameObject);
    }

    /**
     * Update the _canBeMilked bool
     */
    public void UpdateCanBeMilked() {
        _canBeMilked = true;
    }
    
    /**
     * Get Milk if cow is able to be milked
     */
    private void OnMouseDown() {
        if(_canBeMilked) {
            ActionManager.Instance.ClickAction(gameObject, PlayerController.instance.SelectedItem);
            _canBeMilked = false;
        }
    }
}