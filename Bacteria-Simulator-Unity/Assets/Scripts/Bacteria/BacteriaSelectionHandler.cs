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

    private Ray ray;
    private RaycastHit hitData;

    void Start() {

        oldColor = GetComponent<Renderer>().material.color;
        mouseOverColor = new Color32((byte)redColor, (byte)greenColor, (byte)blueColor, 255);

        GameObject obj1 = GameObject.Find("FishTankSceneManager");
        fishTankSceneManager = obj1.GetComponent<FishTankSceneManager>();
    }

    void Update()
    {
        // If left mouse button pressed
        if (Input.GetMouseButtonDown(0) == true) {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitData) == true) {

                if (hitData.collider.name.Equals(gameObject.name) == true) {

                    fishTankSceneManager.ShowBacteriaInfo(gameObject);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    return;
                }
            } else {
                fishTankSceneManager.ShowBacteriaInfo(false);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }

    void OnMouseEnter() {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }

    void OnMouseExit() {
        GetComponent<Renderer>().material.color = oldColor;
    }
}
