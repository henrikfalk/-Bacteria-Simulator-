using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBacteria : Bacteria
{

    void Start()
    {
        bacteriaRigidbody = GetComponent<Rigidbody>();
        bacteriaRenderer = GetComponent<Renderer>();

        GameObject obj1 = GameObject.Find("FishTankSceneManager");
        fishTankSceneManager = obj1.GetComponent<FishTankSceneManager>();

        GameObject obj2 = GameObject.Find("EnvironmentManager");
        environment = obj2.GetComponent<EnvironmentManager>();

        // We like to be in the middle of the fishtank
        temperatureOptimal = 25;

        // 
        temperatureRange = 3f;

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
    void Update()
    {
        if (DateTime.Now > deadTime) {
            // Bacteria is dead of age
            bacteriaRenderer.material = deadMaterial;
            bacteriaRigidbody.mass = 0.1f;
            bacteriaRigidbody.drag = 20;
            bacteriaRigidbody.useGravity = true;

            // Disolve bacteria after some time
            StartCoroutine(DisolveBacteria());

        } else {
            move();
        }
    }

    void OnCollisionEnter(Collision other) {

        // we hit another green bacteria then maybe make sibling
        if (other.collider.tag.Equals("Bacteria") == true && other.collider.name.StartsWith("Red") == true) {
//            fishTankSceneManager.MakeSiblingBacteria(other.gameObject);
        }
    }
}
