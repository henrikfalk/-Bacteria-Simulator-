using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationController : MonoBehaviour
{

    // SimulationSceneManager
    public SimulationSceneManager simulationSceneManager;

    // State
    public AquariumState currentState { get; private set; }

    // Specific flags from UI to  FSM
    public Boolean newSimulation = false;
    public Boolean initializedSimulation = false;

    // Environment
    // the middletemperature of the water
    private float middleTemperature;

    // The toxicity of the environment    
    public int toxicity;
    
    // Current laboratory settings - Use standard is we are running the SimulationScene directly from Unity editor
    LaboratoryInfo currentLaboratoryInfo = new LaboratoryInfo {
        middleTemperatureInfo = 30,
        toxicityInfo = 0,
        maxVelocityGreen = 1,
        temperatureOptimalBacteriaGreen = 20,
        temperatureRangeBacteriaGreen = 11,
        maxAgeMinutesBacteriaGreen = 2,
        fertilityPercentBacteriaGreen = 75,
        maxVelocityRed = 2,
        temperatureOptimalBacteriaRed = 40,
        temperatureRangeBacteriaRed = 10,
        maxAgeMinutesBacteriaRed = 3,
        fertilityPercentBacteriaRed = 50

    };

    // Elapsed simulation time
    // How long has this simulation been running?
    public DateTime simulationStartTime;
    TimeSpan elapsedSimulationTime = new TimeSpan(0);

    String originalElapsedTimeText;

    public int initialNumberGreenBacteria;
    public int initialNumberRedBacteria;

    void Start()
    {

        // Initialize environment
        if (GameManager.Instance != null) {
            currentLaboratoryInfo = GameManager.Instance.GetCurrentLaboratoryInfo();
        }

        middleTemperature = currentLaboratoryInfo.middleTemperatureInfo;
        toxicity = currentLaboratoryInfo.toxicityInfo;

        // Initialize FSM
        currentState = new EmptyAquariumState(this);


    }

    void Update() {

        // FSM
        currentState = currentState.Process();
    }

    public float GetEnvironmentTemperature (Vector3 position) {

        // If position.y = 0 then temperature around middleTemperature
        // Bacteria thrive between 5 - 55 degrees celcius

        // Make a Random temperature around the calculated temperature
        float app = UnityEngine.Random.Range(1f,1.02f);
        float temperature = middleTemperature + (position.y * 5 * app); // The tank is 5 units around the middletemperature

        return temperature;
    }

    public float GetMiddleTemperature() {
        return middleTemperature;
    }

    public int GetToxicity() {
        return toxicity;
    }

    public GameObject[] GetBacteriaList() {
        return GameObject.FindGameObjectsWithTag("Bacteria");
    }

    public TimeSpan GetElapsedSimulationTime() {

        if (simulationStartTime == new DateTime(0)) {
            elapsedSimulationTime = new TimeSpan(0);
        } else {
            elapsedSimulationTime = DateTime.Now - simulationStartTime;
        }

        return elapsedSimulationTime;
    }

    public String GetElapsedSimulationTimeAsString() {
        String elapsedTimeString = "00:00";

        TimeSpan elapsedTime = GetElapsedSimulationTime();

        if (elapsedTime != new TimeSpan(0)) {
            string timeString = "";
            if (elapsedTime.Days > 0) {
                timeString += timeString + elapsedTime.Days + " d ";
            }
            if (elapsedTime.Hours > 0) {
                timeString += elapsedTime.Hours.ToString() + ":";
            }

            timeString += elapsedTime.Minutes.ToString("00") + ":";

            timeString += elapsedTime.Seconds.ToString("00");

            elapsedTimeString = timeString; // "d.hh:mm:ss"
        }

        return elapsedTimeString;
    }
    
    public void SetSimulationStartTime(DateTime dateTime) {

        simulationStartTime = dateTime;
    }


    public void InitializeSimulation() {
        // Create new simulation
        StartCoroutine(InstantiateNewSimulation());
    }

    private IEnumerator InstantiateNewSimulation() {

        ArrayList sourceList = new ArrayList();
        float posX;
        float posY;
        float posZ;
        float rotX;
        float rotY;
        float rotZ;

        // Instantiate Green bacteria 
        for (int i = 0; i < initialNumberGreenBacteria; i++) {

            // Generate random spawnposition and rotation above aquarium
            posX = UnityEngine.Random.Range(-3f, 3f);
            posY = UnityEngine.Random.Range(10.5f, 12.5f);
            posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
            rotX = UnityEngine.Random.Range(-90f, 90f);
            rotY = UnityEngine.Random.Range(-90f, 90f);
            rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(simulationSceneManager.greenBacteriaPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.GetComponent<Bacteria>().StartFSM(BacteriaState.STATE.INIT);

            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Green" + (i+1).ToString();
            obj.SetActive(false);

            sourceList.Add(obj);
        }

        // Instantiate Red bacteria 
        for (int i = 0; i < initialNumberRedBacteria; i++) {

            // Generate random spawnposition and rotation above aquarium
            posX = UnityEngine.Random.Range(-3f, 3f);
            posY = UnityEngine.Random.Range(10.5f, 12.5f);
            posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
            rotX = UnityEngine.Random.Range(-90f, 90f);
            rotY = UnityEngine.Random.Range(-90f, 90f);
            rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(simulationSceneManager.redBacteriaPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.GetComponent<Bacteria>().StartFSM(BacteriaState.STATE.INIT);

            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Red" + (i+1).ToString();
            obj.SetActive(false);

            sourceList.Add(obj);
        }

        sourceList = ShuffleArrayList(sourceList);

        foreach (GameObject obj in sourceList) {

            obj.SetActive(true);

            // Wait a bit because it looks nice
            float waitPeriod = UnityEngine.Random.Range(0.07f, 0.14f);
            yield return new WaitForSeconds(waitPeriod);

        }

        initializedSimulation = true;
    }

    // Not the most efficient code but it works
    private ArrayList ShuffleArrayList(ArrayList sourceList) {
        ArrayList sortedList = new ArrayList();
        System.Random generator = new System.Random();

        while (sourceList.Count > 0) {
            int position = generator.Next(sourceList.Count);
            sortedList.Add(sourceList[position]);
            sourceList.RemoveAt(position);
        }

        return sortedList;
    }

    public void QuitSimulation() {

        simulationSceneManager.ResetCamera();

        // Empty fishtank for bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
        
        // remove bacteria if any
        for (int i = 0; i < bacteria.Length; i++) {
            Destroy(bacteria[i]);
        }

        // Show SimulationEndedPopup
        simulationSceneManager.simulationEndedPopup.SetActive(true);

        if (originalElapsedTimeText == null) {
            // HFALK Could give problems after internationalization
            GameObject objSimulationEndedElapsedTimeText = GameObject.Find("SimulationEndedElapsedTimeText");
            TextMeshProUGUI tmp = objSimulationEndedElapsedTimeText.GetComponent<TextMeshProUGUI>();
            originalElapsedTimeText = tmp.text;
        }

        // Show elapsed simulation time in SimulationEndedPopup
        GameObject obj = GameObject.Find("SimulationEndedElapsedTimeText");
        TextMeshProUGUI simulationEndedElapsedTimeText = obj.GetComponent<TextMeshProUGUI>();
        simulationEndedElapsedTimeText.text = originalElapsedTimeText + GetElapsedSimulationTimeAsString();

    }

    public void SimulationFailed() {

        simulationSceneManager.ResetCamera();

        // Empty fishtank for bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
        
        // remove bacteria if any
        for (int i = 0; i < bacteria.Length; i++) {
            Destroy(bacteria[i]);
        }
        // Show SimulationEndedPopup
        simulationSceneManager.simulationFailedPopup.SetActive(true);
    }

}