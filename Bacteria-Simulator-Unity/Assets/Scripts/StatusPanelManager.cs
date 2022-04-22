using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPanelManager : MonoBehaviour
{

    // 
    public TextMeshProUGUI simulationTimeText;

    public TextMeshProUGUI greenBNumberText;
    public TextMeshProUGUI redBNumberText;
    public TextMeshProUGUI purpleBNumberText;

    public TextMeshProUGUI temperatureBNumberText;

    public TextMeshProUGUI deadBNumberText;

    public TextMeshProUGUI toxicityBNumberText;


    public void UpdateStatus(TimeSpan elapsedTime) {

        string timeString = "";
        if (elapsedTime.Days > 0) {
            timeString += timeString + elapsedTime.Days + " d ";
        }
        if (elapsedTime.Hours > 0) {
            timeString += elapsedTime.Hours.ToString() + ":";
        }

        timeString += elapsedTime.Minutes.ToString("00") + ":";

        timeString += elapsedTime.Seconds.ToString("00");

        simulationTimeText.text = timeString; // "d.hh:mm:ss"
    }

}
