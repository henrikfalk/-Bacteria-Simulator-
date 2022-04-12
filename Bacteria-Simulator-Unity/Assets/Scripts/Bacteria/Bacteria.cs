using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{

    public FishTankSceneManager fishTankSceneManager;
    public EnvironmentManager environment;

    protected Rigidbody bacteriaRigidbody;
    protected Renderer bacteriaRenderer;

    public float maxVelocity;

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
        Debug.Log("Lifetime = " + minutes + " minutes and " + seconds + " seconds");
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
            gameObject.name = "Dead";

            // Disolve bacteria after some time
            StartCoroutine(DisolveBacteria());
        } else {
            move();
        }
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
        float waitPeriod = UnityEngine.Random.Range(300f, 600f);
        yield return new WaitForSeconds(waitPeriod);

        // maybe later add toxic stuff to environment because of decay

        // Destroy me
        Destroy(gameObject);

    }

}
