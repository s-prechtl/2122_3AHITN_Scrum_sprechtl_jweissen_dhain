using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTransitionManager : MonoBehaviour {
    public Animator dayTransitionAnimator;
    public TextMeshProUGUI dayCountText;
    public Button sleepButton;

    private void Start() {
        HouseController.NewDayEvent.AddListener(NewDay);
    }

    private void NewDay() {
        sleepButton.enabled = false;
        dayTransitionAnimator.gameObject.SetActive(true);
        StartCoroutine(PlayTransition());
    }
    
    private IEnumerator PlayTransition() {
        dayCountText.text = "Day " + HouseController.DayCount;
        
        yield return new WaitForSeconds(3f);
        sleepButton.enabled = true;
        dayTransitionAnimator.gameObject.SetActive(false);
    }
}
