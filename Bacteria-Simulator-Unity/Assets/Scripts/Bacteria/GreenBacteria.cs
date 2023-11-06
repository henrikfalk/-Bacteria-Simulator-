using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class GreenBacteria : Bacteria
{

    //public int pregnacyTimeBacteria;

    private int greenCollisions;
    private int purpleCollisions;

    private Ray ray;
    private RaycastHit hitData;

    // MyStart is called before the first frame update
    protected override void BacteriaStart() {

        if (GameManager.Instance != null) {
            LaboratoryInfo info = GameManager.Instance.GetCurrentLaboratoryInfo();

            maxVelocity = info.maxVelocityGreen;
            temperatureOptimal = info.temperatureOptimalBacteriaGreen;
            temperatureRange = info.temperatureRangeBacteriaGreen;
            maxAgeMinutes = info.maxAgeMinutesBacteriaGreen;
            fertilityPercent = info.fertilityPercentBacteriaGreen;
        } else {
            // We run from "SimulationScene"
            maxVelocity = 1;
            temperatureOptimal = 20f;
            temperatureRange = 11f;
            maxAgeMinutes = 2;
            fertilityPercent = 75;
        }
    }

    void OnCollisionEnter(Collision other) {

        // we hit another green bacteria then make sibling if condition
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Green") == true) {

            // Since both parents gets this collision we need to ensure that only one approximately "gets pregnant"
            // And during lifetime we are only being able to be pregnant in certain times
            int makeSiblings = UnityEngine.Random.Range(0,20);
            if (makeSiblings == 1) {
                StartCoroutine(MakeSibling(other.gameObject.transform.position));
            }
            return;
        }

        // we hit a red bacteria then make purple sibling if condition
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Red") == true) {
            int makeSiblings = UnityEngine.Random.Range(0,20);
            if (makeSiblings == 1) {
                StartCoroutine(MakePurple(other.gameObject.transform.position));
//                Debug.Log("Collision: " + gameObject.name + " <-> " + other.collider.name);
            }
            return;

        }
    }

    private IEnumerator MakeSibling(Vector3 position) {

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

        // Wait a bit. Pregancy time
        float waitPeriod = UnityEngine.Random.Range(1.50f, 2.50f);
        yield return new WaitForSeconds(waitPeriod);

        // Ok, then make birth to a new bacteris
        simulationSceneManager.MakeSiblingBacteria("Green", position + new Vector3(0, UnityEngine.Random.Range(-0.3f, 0.3f) + 0.3f, 0));

    }

    private IEnumerator MakePurple(Vector3 position) {

        // Check fertility percent
        float fertilitySucces = UnityEngine.Random.Range(0, 100);
        if (80 > fertilitySucces) { // Hardcoded fertilityPercent = 50 as Green bacteria does not know the fertilityPercent of purple bacteria (Yet?)
            yield return null;
        }
/*
        // We need 5 collisions with other green bacteria
        if (purpleCollisions < 6) {
            purpleCollisions++;
            yield return null;
        } else {
            purpleCollisions = 0;
        }
*/
        // Wait a bit. Pregancy time
        float waitPeriod = UnityEngine.Random.Range(0.50f, 1.50f);
        yield return new WaitForSeconds(waitPeriod);

        // Get parents position
//        Vector3 pos = parent.transform.position;

        // Ok, then make birth to a new bacteris HFALK
        simulationSceneManager.MakePurpleBacteria(position + new Vector3(0, UnityEngine.Random.Range(-0.3f, 0.3f) + 0.3f, 0));
    }
}
