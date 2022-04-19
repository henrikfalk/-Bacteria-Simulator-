using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaSelectionHandler : MonoBehaviour
{

    private FishTankSceneManager fishTankSceneManager;

    private Color32 oldColor;
    private Color32 mouseOverColor;

    private int redColor;
    private int greenColor;
    private int blueColor;


//    private static GameObject selectedGameObject;
//    public GameObject selectedBacteria;
    
    void Start() {

        oldColor = GetComponent<Renderer>().material.color;
        mouseOverColor = new Color32((byte)redColor, (byte)greenColor, (byte)blueColor, 255);

        GameObject obj1 = GameObject.Find("FishTankSceneManager");
        fishTankSceneManager = obj1.GetComponent<FishTankSceneManager>();
    }

    void Update()
    {

        if (fishTankSceneManager.selectedBacteria == gameObject) {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } else {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    void OnMouseEnter() {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }

    void OnMouseExit() {
        GetComponent<Renderer>().material.color = oldColor;
    }
}
