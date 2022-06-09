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

    public void sendMessage(String msg, double duration) {
        
    }
}
