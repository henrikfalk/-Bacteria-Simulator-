using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBacteria : Bacteria
{

    //public int pregnacyTimeBacteria;

    private int greenCollisions;

    private Ray ray;
    private RaycastHit hitData;

    // MyStart is called before the first frame update
    protected override void BacteriaStart()
    {

        if (GameManager.Instance != null) {
            LaboratoryInfo info = GameManager.Instance.GetCurrentLaboratoryInfo();

            maxVelocity = info.maxVelocityGreen;
            temperatureOptimal = info.temperatureOptimalBacteriaGreen;
            temperatureRange = info.temperatureRangeBacteriaGreen;
            maxAgeMinutes = info.maxAgeMinutesBacteriaGreen;
            fertilityPercent = info.fertilityPercentBacteriaGreen;
        } else {
            // We run from "FishtankScene"
            maxVelocity = 1;
            temperatureOptimal = 20f;
            temperatureRange = 11f;
            maxAgeMinutes = 2;
            fertilityPercent = 75;
        }
    }

    // Update is called once per frame
    public void Update()
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

        // we hit another green bacteria then make sibling if condition
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Green") == true) {
            // Disabled HFALK
            // StartCoroutine(MakeSibling(other.gameObject));
        }
    }

    // Make sibling is all conditions is ok. This code does not Work!!!!
    private IEnumerator MakeSibling(GameObject parent) {

        // Check fertility percent
        float fertilitySucces = UnityEngine.Random.Range(0, 100);
        if (fertilityPercent > fertilitySucces) {
            yield return null;
        }

        // We need 5 collisions with other green bacteria
        if (greenCollisions < 6) {
            greenCollisions++;
            yield return null;
        } else {
            greenCollisions = 0;
        }

        // Pregnacy time with a bit of uncertainty
        float waitPeriod = UnityEngine.Random.Range(0.97f, 1.03f);
//        yield return new WaitForSeconds(pregnacyTimeBacteria * waitPeriod);

        // Ok, then make birth to a new bacteris
        fishTankSceneManager.MakeSiblingBacteria(parent);

    }

}
