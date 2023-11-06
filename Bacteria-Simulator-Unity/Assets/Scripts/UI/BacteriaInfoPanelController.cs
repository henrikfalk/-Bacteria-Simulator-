using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BacteriaInfoPanelManager : MonoBehaviour
{

    public TextMeshProUGUI typeBNameText;
    public TextMeshProUGUI nameBNameText;

    public TextMeshProUGUI fertilityBNumberText;

    public TextMeshProUGUI tempDynamicBNumberText;

    public TextMeshProUGUI energyBNumberText;

    public TextMeshProUGUI healthBNumberText;

    private SimulationSceneManager simulationSceneManager;

    public GameObject bacteria;

    void Start() {
        GameObject obj1 = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj1.GetComponent<SimulationSceneManager>();
    }

    void Update() {
        bacteria = simulationSceneManager.selectedBacteria;
        if (bacteria == null) {

            // None selected
            gameObject.SetActive(false);

            return;
        }

        // Update bacteria type
        string type = bacteria.name;
        if (type.StartsWith("Green") == true) {
            type = "Green";
        }
        if (type.StartsWith("Red") == true) {
            type = "Red";
        }
        if (type.StartsWith("Purple") == true) {
            type = "Purple";
        }
        if (type.StartsWith("Dead") == true) {
            type = "Dead";
        }

        typeBNameText.text = type;

        // Update name
        nameBNameText.text = bacteria.name;

        // Update fertility
        fertilityBNumberText.text = bacteria.GetComponent<Bacteria>().fertilityPercent.ToString("0.00");

        // Update temperature
        tempDynamicBNumberText.text = simulationSceneManager.simulationController.GetEnvironmentTemperature(bacteria.transform.position).ToString("0.0");

        // Update energy
        energyBNumberText.text = bacteria.GetComponent<Bacteria>().energiBacteria.ToString("0.0");

        // Update health
        healthBNumberText.text = bacteria.GetComponent<Bacteria>().healthBacteria.ToString("0.0");

    }
}
