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

    private FishTankSceneManager fishTankSceneManager;

    public GameObject bacteria { private get; set; }

    void Start() {
        GameObject obj1 = GameObject.Find("FishTankSceneManager");
        fishTankSceneManager = obj1.GetComponent<FishTankSceneManager>();
        bacteria = fishTankSceneManager.selectedBacteria;
    }

    void Update() {
        if (bacteria == null) {

            // We are dead
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
        tempDynamicBNumberText.text = fishTankSceneManager.environment.GetEnvironmentTemperature(bacteria.transform.position).ToString("0.0");

        // Update energy
        energyBNumberText.text = bacteria.GetComponent<Bacteria>().energiBacteria.ToString("0.0");

        // Update health
        healthBNumberText.text = bacteria.GetComponent<Bacteria>().healthBacteria.ToString("0.0");

    }
}
