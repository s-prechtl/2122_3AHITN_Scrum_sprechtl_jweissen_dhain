using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTransitionManager : MonoBehaviour {
    public Animator dayTransitionAnimator;
    public TextMeshProUGUI dayCountText;
    public GameObject sleepButton;

    private void Start() {
        HouseController.NewDayEvent.AddListener(NewDay);
    }

    private void NewDay() {
        sleepButton.GetComponent<Button>().enabled = false;
        dayTransitionAnimator.gameObject.SetActive(true);
        StartCoroutine(PlayTransition());
    }
    
    private IEnumerator PlayTransition() {
        dayCountText.text = "Day " + HouseController.DayCount;
        dayTransitionAnimator.SetTrigger("start");
        
        yield return new WaitForSeconds(3f);
        sleepButton.GetComponent<Button>().enabled = true;
        dayTransitionAnimator.gameObject.SetActive(false);
    }
}
