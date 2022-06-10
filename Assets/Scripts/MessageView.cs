using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageView : MonoBehaviour
{
    #region Singleton

    public static MessageView instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of MessageView found");
        }

        instance = this;
    }

    #endregion

    public GameObject messageView;
    public TextMeshProUGUI message;

    private void Start() {
        messageView.SetActive(false);
    }

    public void sendMessage(String msg, float duration) {
        message.text = msg;
        StartCoroutine(showForSeconds(duration));
    }

    private IEnumerator showForSeconds(float duration) {
        messageView.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageView.SetActive(false);
    }
}
