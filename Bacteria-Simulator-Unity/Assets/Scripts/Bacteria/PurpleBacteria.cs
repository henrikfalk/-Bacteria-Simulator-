using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBacteria : Bacteria
{
    private int greenCollisions;

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
        if (greenCollisions < 6) {
            greenCollisions++;
            yield return null;
        } else {
            greenCollisions = 0;
        }

        // Wait a bit. Pregancy time
        float waitPeriod = UnityEngine.Random.Range(0.5f, 1.50f);
        yield return new WaitForSeconds(waitPeriod);

        // Ok, then make birth to a new bacteris
        simulationSceneManager.MakeSiblingBacteria("Purple", position);

    }

}
