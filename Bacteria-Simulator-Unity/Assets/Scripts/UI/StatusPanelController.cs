using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPanelController : MonoBehaviour
{

    public SimulationSceneManager simulationSceneManager;

    private SimulationController simulationController;

    // 
    public TextMeshProUGUI simulationTimeText;

    public TextMeshProUGUI greenBNumberText;
    public TextMeshProUGUI redBNumberText;
    public TextMeshProUGUI purpleBNumberText;

    public TextMeshProUGUI totalBNumberText;

    public TextMeshProUGUI temperatureBNumberText;

    public TextMeshProUGUI deadBNumberText;

    public TextMeshProUGUI toxicityBNumberText;

    private GameObject[] bacteriaList;
    private int green;
    private int red;
    private int purple;
    private int dead;

    public void Start() {

        // Get a handle to the SimulationSceneManager
//        GameObject obj = GameObject.Find("SimulationSceneManager");
//        simulationSceneManager = obj.GetComponent<SimulationSceneManager>();
        

    }

    void Update()
    {

        if (simulationSceneManager != null) {
            simulationController = simulationSceneManager.GetSimulationController();
        } else {
            return;
        }
        
        // Update time
        simulationTimeText.text = simulationController.GetElapsedSimulationTimeAsString();

        // Update middletemperature
        temperatureBNumberText.text = simulationController.GetMiddleTemperature() + " ºC";

        // Update toxicity
        toxicityBNumberText.text = simulationController.GetToxicity().ToString();

        // Update bacteria
        bacteriaList = simulationController.GetBacteriaList();

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

        totalBNumberText.text = bacteriaList.Length.ToString();
    }

}
