using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaSelectionHandler3 : MonoBehaviour
{

    private SimulationSceneManager simulationSceneManager;

    private Color32 oldColor;
    private Color32 mouseOverColor;

    private int redColor = 255;
    private int greenColor = 255;
    private int blueColor = 0;


//    private static GameObject selectedGameObject;
//    public GameObject selectedBacteria;
    
    void Start() {

        mouseOverColor = new Color32((byte)redColor, (byte)greenColor, (byte)blueColor, 255);

        GameObject obj1 = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj1.GetComponent<SimulationSceneManager>();
    }

    void Update()
    {

        if (simulationSceneManager.selectedBacteria == gameObject) {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } else {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    void OnMouseEnter() {

        oldColor = GetComponent<Renderer>().material.GetColor("_BacteriaColor");
        GetComponent<Renderer>().material.SetColor("_BacteriaColor", mouseOverColor);
    }

    void OnMouseExit() {
        GetComponent<Renderer>().material.SetColor("_BacteriaColor", oldColor);

    }
}
