using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria3 : MonoBehaviour
{

    protected SimulationSceneManager simulationSceneManager;
    public EnvironmentManager environment;

    protected Rigidbody bacteriaRigidbody;
    protected Renderer bacteriaRenderer;

    public float maxVelocity;

    public float fertilityPercent;

    // How much energy we have
    public float energiBacteria;

    // How much energy we have
    public float healthBacteria;

    // Most optimal temperature for this bacteria
    public float temperatureOptimal;

    // The temperature range +/- that the bacteria can live within
    public float temperatureRange;

    // The surface temperature
    private float waterMaxTemperature;

    /*
        Age specific stuff
    */
    // Material for dead bacteria
    public Material deadMaterial;

    // The date and time this bacteria was born
    public DateTime bornTime;

    // The max age of the bacteria in minutes
    public int maxAgeMinutes;

    // The date and time this bacteria dies of age
    public DateTime deadTime;

    // We can only die once
    protected bool bacteriaDead = false;

    // Start is called before the first frame update
    void Start()
    {
        bacteriaRigidbody = GetComponent<Rigidbody>();
        bacteriaRenderer = GetComponent<Renderer>();
//        Debug.Log("bacteriaRenderer.material.name = " + bacteriaRenderer.material.name);
//        Debug.Log("deadMaterial.name = " + deadMaterial.name);

        GameObject obj1 = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj1.GetComponent<SimulationSceneManager>();

        GameObject obj2 = GameObject.Find("EnvironmentManager");
        environment = obj2.GetComponent<EnvironmentManager>();

        // We like to be in the middle of the fishtank
        temperatureOptimal = environment.GetMiddleTemperature();

        // 
        temperatureRange = 2;

        transform.Rotate(UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f));

        // Remember when this bacteria is born
        bornTime = DateTime.Now;
        
        // Randommize deadTime around 20% of the maxAgeMinutes
        float deadTimeFloat = UnityEngine.Random.Range(maxAgeMinutes - (maxAgeMinutes * 0.2f), maxAgeMinutes + (maxAgeMinutes * 0.2f));
        int minutes = Mathf.FloorToInt(deadTimeFloat);
        int seconds = Mathf.FloorToInt((deadTimeFloat - minutes) * 100);
        deadTime = bornTime.Add(new System.TimeSpan(0,0,minutes, seconds));

        // Use this method in derived classes for overriding stuff
        BacteriaStart();
    }

    // Use this method in derived classes for overriding stuff in Start() method
    protected virtual void BacteriaStart(){

    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now > deadTime && bacteriaDead == false) {
            die("Dead");
        }

        move();
    }

    // ABSTRACTION
    protected void die(string deadName) {
            // This code must only run once
            bacteriaDead = true;

            // Bacteria is dead
            bacteriaRenderer.material = deadMaterial;
            bacteriaRigidbody.mass = 0.1f;
            bacteriaRigidbody.drag = 20;
            bacteriaRigidbody.useGravity = true;
            gameObject.name = deadName;

            // Disolve bacteria after some time
            StartCoroutine(DisolveBacteria());

    }

    void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Air") == true) {
            waterMaxTemperature = Mathf.Round(environment.GetEnvironmentTemperature(transform.position)) - 1;

            // Make random rotation when hitting water
            transform.Rotate(UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f));

            bacteriaRigidbody.useGravity = false;

        } else {
            // Use the environments temperature
            waterMaxTemperature = environment.GetMiddleTemperature();
        }
    }

    // Deaccelerate if we are to fast
    private void Deaccelerate() {

        var velocity = bacteriaRigidbody.velocity;
        if (velocity.magnitude > maxVelocity) {
        
            //we deaccelerate
            velocity -= velocity.normalized * 0.5f;
            
            bacteriaRigidbody.velocity = velocity;
        }
    }

    protected virtual void move() {

        if (bacteriaDead == true) {
            return;
        }

        Deaccelerate();

        float randomDirectionX;
        float randomDirectionY;
        float randomDirectionZ;
        Vector3 forceDir;

        // Get the environments temperature which is equals to ours
        float temp = environment.GetEnvironmentTemperature(transform.position);
        if ( temp > waterMaxTemperature) {
            // we are not in the water so go downwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, -1,randomDirectionZ);
            forceDir.Normalize();
            bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);

        }

        if (temp > temperatureOptimal + temperatureRange) {
            // It is getting to hot - move downwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, -1,randomDirectionZ);
            forceDir.Normalize();
            bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
            return;
        }

        if (temp < temperatureOptimal - temperatureRange) {
            // It is getting to cold - move upwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, 1,randomDirectionZ);
            forceDir.Normalize();
            bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
            return;
        }
            // Just move since our temperature inside the optimal range
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionY = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, randomDirectionY,randomDirectionZ);
            forceDir.Normalize();
            bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
    }

    protected IEnumerator DisolveBacteria() {
        // Wait a bit because it looks nice
//        float waitPeriod = UnityEngine.Random.Range(300f, 600f);
        float waitPeriod = UnityEngine.Random.Range(15f, 30f);
        yield return new WaitForSeconds(waitPeriod);

        // maybe later add toxic stuff to environment because of decay

        // Update UI
//        simulationSceneManager.BacteriaDies(gameObject);

        // Destroy me
        Destroy(gameObject);

    }

    public Boolean IsDead(){
        return bacteriaDead;
    }
}
