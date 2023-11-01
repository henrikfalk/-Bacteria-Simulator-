using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaSelectionHandler : MonoBehaviour
{

    private SimulationSceneManager simulationSceneManager;

    private Color32 oldColor;
    private Color32 mouseOverColor;

    private int redColor = 255;
    private int greenColor = 255;
    private int blueColor = 0;

    private Boolean inFocus = false;

    void Start() {

        mouseOverColor = new Color32((byte)redColor, (byte)greenColor, (byte)blueColor, 255);

        GameObject obj1 = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj1.GetComponent<SimulationSceneManager>();
    }

    void Update()
    {
        // Calling "GetChild(1)" is not safe! Will be replaced!
        if (gameObject == simulationSceneManager.selectedBacteria) {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } else {
            if (inFocus == true) {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            } else {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }


    void OnMouseEnter() {
        inFocus = true;
    }

    void OnMouseExit() {
        inFocus = false;
    }
}
