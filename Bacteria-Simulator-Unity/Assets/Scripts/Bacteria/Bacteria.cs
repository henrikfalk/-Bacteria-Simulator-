using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{

    public SimulationSceneManager simulationSceneManager;

    public Rigidbody bacteriaRigidbody;
    protected MeshRenderer bacteriaRenderer;

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

    // State
    public BacteriaState currentState { get; protected set; }
    // Start is called before the first frame update

    void Start()
    {
        bacteriaRigidbody = GetComponent<Rigidbody>();
        bacteriaRenderer = GetComponent<MeshRenderer>();

        GameObject obj1 = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj1.GetComponent<SimulationSceneManager>();

        // We like to be in the middle of the fishtank
        temperatureOptimal = 30;

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
    protected virtual void BacteriaStart(){}

    // FixedUpdate is called
    void FixedUpdate() {

        // FSM
        if (currentState != null) {
            currentState = currentState.ProcessFixed();
        }
    }

    // Update is called
    void Update() {

        // FSM
        if (currentState != null) {
            currentState = currentState.Process();
        }
    }

    public void die(string deadName) {
            // Bacteria is dead
            Material[] materials = bacteriaRenderer.materials;
            materials[0] = deadMaterial;
            bacteriaRenderer.materials = materials;

            bacteriaRigidbody.mass = 0.1f;
            bacteriaRigidbody.drag = 20;
            bacteriaRigidbody.useGravity = true;
            gameObject.name = deadName;

            // Add toxic stuff to environment because of decay
            // Change this later to size of bacteria
            float randomToxicityFactor = 100 + UnityEngine.Random.Range(-5, 5);
            simulationSceneManager.simulationController.AddToxicity((int)randomToxicityFactor);

            // Disolve bacteria after some time
            StartCoroutine(DisolveBacteria());

    }

    protected IEnumerator DisolveBacteria() {

        // Create dying particlesystem effect
        ParticleSystem particles = Instantiate(simulationSceneManager.dyingBacteriaParticles, transform.position, simulationSceneManager.dyingBacteriaParticles.transform.rotation);


        // Wait a bit because it looks nice
        float waitPeriod = UnityEngine.Random.Range(10f, 20f);
        yield return new WaitForSeconds(waitPeriod);

        // Reset camera if following a bacteria
        if (gameObject.transform.GetChild(0).gameObject.activeSelf == true) {
            simulationSceneManager.ResetCamera();
        }

        // Destroy bacteria
        Destroy(gameObject);

    }

    public Boolean IsDead(){

        if (currentState == null) {
            return false;
        }

        if (currentState.stateName == BacteriaState.STATE.ALIVE || currentState.stateName == BacteriaState.STATE.INIT) {
            return false;
        } else {
            return true;
        }
    }

    public void StartFSM(BacteriaState.STATE _state) {

        switch(_state) {
            case BacteriaState.STATE.INIT:
                    currentState = new BacteriaStateInitializing(this);
                break;

            case BacteriaState.STATE.ALIVE:
                    currentState = new BacteriaStateRunning(this);
                break;
        }

    }

}
