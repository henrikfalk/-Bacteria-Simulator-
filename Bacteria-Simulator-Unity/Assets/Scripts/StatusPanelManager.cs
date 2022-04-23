using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPanelManager : MonoBehaviour
{

    public EnvironmentManager environment;

    // 
    public TextMeshProUGUI simulationTimeText;

    public TextMeshProUGUI greenBNumberText;
    public TextMeshProUGUI redBNumberText;
    public TextMeshProUGUI purpleBNumberText;

    public TextMeshProUGUI temperatureBNumberText;

    public TextMeshProUGUI deadBNumberText;

    public TextMeshProUGUI toxicityBNumberText;

    private GameObject[] bacteriaList;
    private int green;
    private int red;
    private int purple;
    private int dead;

    public void Start() {

        // Get a handle to the EnvironmentManager
        GameObject obj1 = GameObject.Find("EnvironmentManager");
        environment = obj1.GetComponent<EnvironmentManager>();
    }

    public void UpdateStatus(TimeSpan elapsedTime) {

        // Update middletemperature
        temperatureBNumberText.text = environment.GetMiddleTemperature() + " ºC";

        // Update time
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

        // Update bacteria
        bacteriaList = GameObject.FindGameObjectsWithTag("Bacteria");
        green = 0;
        red = 0;
        purple = 0;
        dead = 0;
        foreach (GameObject bacteria in bacteriaList) {

            if (bacteria.name.StartsWith("Green") == true) {
                    green++;
            }
            if (bacteria.name.StartsWith("Red") == true) {
                    red++;
            }
            if (bacteria.name.StartsWith("Purple") == true) {
                    purple++;
            }
            if (bacteria.name.StartsWith("Dead") == true) {
                    dead++;
            }
        }

        greenBNumberText.text = green.ToString();    
        redBNumberText.text = red.ToString();    
        purpleBNumberText.text = purple.ToString();
        deadBNumberText.text = dead.ToString();

        // Update toxicity
        toxicityBNumberText.text = environment.GetToxicity().ToString();
    }

}
