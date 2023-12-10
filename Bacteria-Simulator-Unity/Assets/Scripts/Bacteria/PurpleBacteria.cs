using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBacteria : Bacteria
{
    private int purpleCollisions;

    private Ray ray;
    private RaycastHit hitData;

    // MyStart is called before the first frame update
    protected override void BacteriaStart() {

        if (GameManager.Instance != null) {
//            LaboratoryInfo info = GameManager.Instance.GetCurrentLaboratoryInfo();
            SimulationConfiguration info = GameManager.Instance.GetCurrentSimulationConfiguration();

            maxVelocity = float.Parse(info.maxVelocityPurple);
            temperatureOptimal = info.temperatureOptimalBacteriaPurple;
            temperatureRange = info.temperatureRangeBacteriaPurple;
            maxAgeMinutes = info.maxAgeMinutesBacteriaPurple;
            fertilityPercent = info.fertilityPercentBacteriaPurple;
        } else {
            // We run from "SimulationScene"
            maxVelocity = 2;
            temperatureOptimal = 30f;
            temperatureRange = 20f;
            maxAgeMinutes = 3;
            fertilityPercent = 80;
        }
    }

    void OnCollisionEnter(Collision other) {

        // we hit another purple bacteria then make sibling if condition
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Purple") == true) {

            // Since both parents gets this collision we need to ensure that only one approximately "gets pregnant"
            // And during lifetime we are only being able to be pregnant in certain times
            int makeSiblings = UnityEngine.Random.Range(0,20);
            if (makeSiblings == 1) {
                StartCoroutine(MakeSibling(other.gameObject.transform.position));
            }
            return;
        }

        // we hit a green or red bacteria then eat them
        if (other.collider.tag.Equals("Bacteria") == true && (other.collider.name.StartsWith("Green") == true || other.collider.name.StartsWith("Red") == true)) {

            // if we are looking at a selcted bacteria then reset camera
            if (simulationSceneManager.selectedBacteria != null && simulationSceneManager.selectedBacteria.name == other.collider.name) {
                simulationSceneManager.ResetCamera();
            }

            Destroy(other.gameObject);
        }

    }

    private IEnumerator MakeSibling(Vector3 position) {

        // Check fertility percent
        float fertilitySucces = UnityEngine.Random.Range(0, 100);
        if (fertilityPercent > fertilitySucces) {
            yield return null;
        }

        // We need 5 collisions with other green bacteria
        if (purpleCollisions < 6) {
            purpleCollisions++;
            yield return null;
        } else {
            purpleCollisions = 0;
        }

        // Wait a bit. Pregancy time
        float waitPeriod = UnityEngine.Random.Range(0.5f, 1.50f);
        yield return new WaitForSeconds(waitPeriod);

        // Ok, then make birth to a new bacteris
        simulationSceneManager.MakeSiblingBacteria("Purple", position);

    }

}
