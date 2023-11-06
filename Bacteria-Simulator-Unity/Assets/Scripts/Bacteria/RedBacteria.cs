using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class RedBacteria : Bacteria
{

    private int redCollisions;

    private Ray ray;
    private RaycastHit hitData;

    protected override void BacteriaStart()
    {
        if (GameManager.Instance != null) {
            LaboratoryInfo info = GameManager.Instance.GetCurrentLaboratoryInfo();

            maxVelocity = info.maxVelocityGreen;
            temperatureOptimal = info.temperatureOptimalBacteriaRed;
            temperatureRange = info.temperatureRangeBacteriaRed;
            maxAgeMinutes = info.maxAgeMinutesBacteriaRed;
            fertilityPercent = info.fertilityPercentBacteriaRed;
        } else {
            // We run from "SimulationScene"
            maxVelocity = 2;
            temperatureOptimal = 40f;
            temperatureRange = 10f;
            maxAgeMinutes = 3;
            fertilityPercent = 50;
        }

    }

    void OnCollisionEnter(Collision other) {

        // we hit another green bacteria then make sibling if condition
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Red") == true) {

            // Since both parents gets this collision we need to ensure that only one approximately "gets pregnant"
            // And during lifetime we are only being able to be pregnant in certain times
            int makeSiblings = UnityEngine.Random.Range(0,20);
            if (makeSiblings == 1) {
                StartCoroutine(MakeSibling(other.gameObject.transform.position));
            }

        }
    }

    private IEnumerator MakeSibling(Vector3 position) {


        // Check fertility percent
        float fertilitySucces = UnityEngine.Random.Range(0, 100);
        if (fertilityPercent > fertilitySucces) {
            yield return null;
        }

        // We need 10 collisions with other green bacteria
        if (redCollisions < 11) {
            redCollisions++;
            yield return null;
        } else {
            redCollisions = 0;
        }

        // Wait a bit. Pregancy time
        float waitPeriod = UnityEngine.Random.Range(0.50f, 1.5f);
        yield return new WaitForSeconds(waitPeriod);

        // Ok, then make birth to a new bacteris
        simulationSceneManager.MakeSiblingBacteria("Red", position + new Vector3(0, UnityEngine.Random.Range(-0.3f, 0.3f) + 0.3f, 0));

    }
}
