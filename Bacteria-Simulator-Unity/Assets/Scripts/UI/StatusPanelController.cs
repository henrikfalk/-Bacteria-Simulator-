using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Unity.VisualScripting;
//using Microsoft.Unity.VisualStudio.Editor;

public class StatusPanelController : MonoBehaviour
{

    public SimulationSceneManager simulationSceneManager;

    private SimulationController simulationController;

    // 
    public GameObject simulationStateImage;
    public Sprite greenStatusButton;
    public Sprite yellowStatusButton;
    public Sprite redStatusButton;

    public TextMeshProUGUI simulationTimeText;

    public TextMeshProUGUI greenBNumberText;
    public TextMeshProUGUI redBNumberText;
    public TextMeshProUGUI purpleBNumberText;

    public TextMeshProUGUI totalBNumberText;

    public TextMeshProUGUI temperatureBNumberText;

    public TextMeshProUGUI deadBNumberText;

    public TextMeshProUGUI toxicityBNumberText;
    public GameObject toxicityStateImage;

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

        // Failsafe
        if (simulationSceneManager == null) {
            GameObject obj = GameObject.Find("SimulationSceneManager");
            simulationSceneManager = obj.GetComponent<SimulationSceneManager>();
        }

        if (simulationController == null) {
            simulationController = simulationSceneManager.GetSimulationController();
            return;
        }

        if (simulationController.currentState == null) {
            return;
        }

        // Update SimulationStateImage
        switch (simulationController.currentState.stateName) {

            case AquariumState.STATE.INITIALIZING: 
                    simulationStateImage.transform.GetComponent<Image>().sprite = yellowStatusButton;
                    toxicityStateImage.transform.GetComponent<Image>().sprite = greenStatusButton;
                break;
            case AquariumState.STATE.RUNNING: 
                    simulationStateImage.transform.GetComponent<Image>().sprite = greenStatusButton;
    
                    // Update time
                    simulationTimeText.text = simulationController.GetElapsedSimulationTimeAsString();

                    // update toxicityStateImage
                    if (simulationController.GetToxicity() < 500) {
                        toxicityStateImage.transform.GetComponent<Image>().sprite = greenStatusButton;
                    } else if (simulationController.GetToxicity() < 1000) {
                        toxicityStateImage.transform.GetComponent<Image>().sprite = yellowStatusButton;
                    } else {
                        toxicityStateImage.transform.GetComponent<Image>().sprite = redStatusButton;
                    }
                break;
            case AquariumState.STATE.PAUSED: 
                    simulationStateImage.transform.GetComponent<Image>().sprite = yellowStatusButton;
                break;
                default:
                    simulationStateImage.transform.GetComponent<Image>().sprite = redStatusButton;
                break;
        }

        // HFALK maybe another way?
        if (simulationController.currentState.stateName == AquariumState.STATE.PAUSED) {
            return;
        }

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

        totalBNumberText.text = (bacteriaList.Length-dead).ToString();
    }

}
