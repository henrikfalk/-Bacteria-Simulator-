using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBacteria : Bacteria
{

    private Ray ray;
    private RaycastHit hitData;

    protected override void MyStart()
    {

        // We like to be in the middle of the fishtank
        temperatureOptimal = 25;

        // 
        temperatureRange = 3f;

    }


    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now > deadTime && bacteriaDead == false) {
            die("Dead " + gameObject.name);
        }

        // If right mouse button pressed then die
        if (Input.GetMouseButtonDown(1) == true) {
            if (fishTankSceneManager.defaultCamera.gameObject.activeSelf == true) {
                ray = fishTankSceneManager.defaultCamera.ScreenPointToRay(Input.mousePosition);
            } else {
                ray = fishTankSceneManager.lockCamera.ScreenPointToRay(Input.mousePosition);
            }

            if (Physics.Raycast(ray, out hitData) == true) {

                if (hitData.collider.name.Equals(gameObject.name) == true && bacteriaDead == false) {

                    die("Dead " + gameObject.name);
                }
            }
        }

        move();
    }

    void OnCollisionEnter(Collision other) {

        // we hit another green bacteria then maybe make sibling
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Red") == true) {
//            fishTankSceneManager.MakeSiblingBacteria(other.gameObject);
        }
    }
}
