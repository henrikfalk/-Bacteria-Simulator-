using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBacteria : Bacteria
{

    public int pregnacyTime;

    public float fertilityPercent;

    // How much energy we have
    private float energi;

    private int greenCollisions;

    // Start is called before the first frame update
    void Start()
    {
        bacteriaRigidbody = GetComponent<Rigidbody>();
        bacteriaRenderer = GetComponent<Renderer>();

        GameObject obj1 = GameObject.Find("FishTankSceneManager");
        fishTankSceneManager = obj1.GetComponent<FishTankSceneManager>();

        GameObject obj2 = GameObject.Find("EnvironmentManager");
        environment = obj2.GetComponent<EnvironmentManager>();

        // We like to be in the middle of the fishtank
        temperatureOptimal = 21f;

        // 
        temperatureRange = 2f;

        transform.Rotate(UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f));
        
        // Remember when this bacteria is born
        bornTime = DateTime.Now;
        
        // Randommize deadTime around 20% of the maxAgeMinutes
        float deadTimeFloat = UnityEngine.Random.Range(maxAgeMinutes - (maxAgeMinutes * 0.2f), maxAgeMinutes + (maxAgeMinutes * 0.2f));

        int minutes = Mathf.FloorToInt(deadTimeFloat);
        int seconds = Mathf.FloorToInt((deadTimeFloat - minutes) * 100);

        deadTime = bornTime.Add(new System.TimeSpan(0,0,minutes, seconds));
    }

    // Update is called once per frame
    public void Update()
    {
        if (DateTime.Now > deadTime) {
            // Bacteria is dead of age
            bacteriaRenderer.material = deadMaterial;
            bacteriaRigidbody.mass = 0.1f;
            bacteriaRigidbody.drag = 20;
            bacteriaRigidbody.useGravity = true;
            gameObject.name = "Dead Green";

            // Disolve bacteria after some time
            StartCoroutine(DisolveBacteria());

        } else {
            move();
        }

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
        yield return new WaitForSeconds(pregnacyTime * waitPeriod);

        // Ok, then make birth to a new bacteris
        fishTankSceneManager.MakeSiblingBacteria(parent);

    }

}
