using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class SimulationController : MonoBehaviour
{

    public enum SIMULATION_MESSAGE {
        ENDEDNORMAL,
        ENDEDBYQUIT,
        OVERPOPULATION,
        UNDERPOPULATION,
        MAXTOXICIYREACHED
    };

    private SIMULATION_MESSAGE simulationMessage;

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
    private int toxicity;
    Queue<int> toxicityQueue = new Queue<int>();
    
    // Current laboratory settings - Use standard is we are running the SimulationScene directly from Unity editor
/*    
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
*/

   // Default simulation configuration if runing scene inside Unity
    private SimulationConfiguration defaultSimulationConfiguration = new SimulationConfiguration {
        simulationName = "Default simulation",
        simulationDescription = "This is the default simulation.",
        middleTemperatureInfo = 30,
        toxicityInfo = 0,
        yellowWarningToxicityInfo = 500,
        redWarningToxicityInfo = 1000,
        maxLimitToxicityInfo = 2000,
        maxVelocityGreen = "1",
        temperatureOptimalBacteriaGreen = 20,
        temperatureRangeBacteriaGreen = 11,
        maxAgeMinutesBacteriaGreen = 2,
        fertilityPercentBacteriaGreen = 75,
        maxVelocityRed = "2",
        temperatureOptimalBacteriaRed = 40,
        temperatureRangeBacteriaRed = 10,
        maxAgeMinutesBacteriaRed = 3,
        fertilityPercentBacteriaRed = 50,
        maxVelocityPurple = "3",
        temperatureOptimalBacteriaPurple = 40,
        temperatureRangeBacteriaPurple = 10,
        maxAgeMinutesBacteriaPurple = 3,
        fertilityPercentBacteriaPurple = 50
    };
    // Current simulation configuration
    private SimulationConfiguration currentSimulationConfiguration;


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
            currentSimulationConfiguration = GameManager.Instance.GetCurrentSimulationConfiguration();
        } else {
            currentSimulationConfiguration = (SimulationConfiguration)defaultSimulationConfiguration.Clone();
        }

        middleTemperature = currentSimulationConfiguration.middleTemperatureInfo;
        toxicity = currentSimulationConfiguration.toxicityInfo;

        simulationSceneManager.simulationNameText.text = "\"" + currentSimulationConfiguration.simulationName + "\"";

        // Initialize FSM
        currentState = new AquariumStateEmpty(this);

    }

    void Update() {

        // FSM
        currentState = currentState.Process();
    }

    public float GetEnvironmentTemperature (Vector3 position) {

        // If position.y = 0 then temperature around middleTemperature
        // Bacteria thrive between 5 - 55 degrees celcius

        // Make a Random temperature around the calculated temperature
        float app = 1.0f;
        if (currentState.stateName != AquariumState.STATE.PAUSED) {
            // Hack: Properly not the rigth way to do it
            app = UnityEngine.Random.Range(0.98f,1.02f);
        }

        float temperature = middleTemperature + (position.y * 5 * app); // The tank is 5 units around the middletemperature

        return temperature;
    }

    public float GetMiddleTemperature() {

        return middleTemperature;
    }

    public int GetToxicity() {

        return toxicity;
    }

    public void AddToxicity(int _toxicity) {

        toxicityQueue.Enqueue(_toxicity);
        if (currentState.stateName == AquariumState.STATE.RUNNING) {
            StartCoroutine(UpdateToxicityCoroutine());
        }
    }

    public void RemoveToxicity(int _toxicity) {
        toxicity -= _toxicity;
        if (toxicity < 0) {
            toxicity = 0;
        }
    }

    public void FlushToxicity() {

        StartCoroutine(UpdateToxicityCoroutine());
    }

    public void ResetToxicity() {

        toxicityQueue.Clear();
        toxicity = currentSimulationConfiguration.toxicityInfo;
    }

    private IEnumerator UpdateToxicityCoroutine() {

            while (toxicityQueue.Count != 0) {
                int tox = toxicityQueue.Dequeue();

                for (int i = 0; i < tox; i++) {

                    // Wait a bit because it looks nice
                    float waitPeriod = UnityEngine.Random.Range(0.01f, 0.05f);
                    yield return new WaitForSecondsRealtime(waitPeriod);

                    if (currentState.stateName == AquariumState.STATE.RUNNING) {
                        toxicity++;
                    }
                }
            }
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

    public DateTime GetSimulationStartTime() {

        return simulationStartTime;
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

            obj.transform.Rotate(Time.deltaTime * new Vector3(rotX,rotY,rotZ));
//            obj.name = "Green" + Time.frameCount;
            obj.name = "Green " + Guid.NewGuid().ToString();
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

            obj.transform.Rotate(Time.deltaTime * new Vector3(rotX,rotY,rotZ));
//            obj.name = "Red" + Time.frameCount;
            obj.name = "Red " + Guid.NewGuid().ToString();
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
/*
    public void QuitSimulation() {

        simulationSceneManager.ResetCamera();

        // Empty aquarium for bacteria
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

        // Empty aquarium for bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
        
        // remove bacteria if any
        for (int i = 0; i < bacteria.Length; i++) {
            Destroy(bacteria[i]);
        }
        // Show SimulationEndedPopup
        simulationSceneManager.simulationFailedPopup.SetActive(true);
    }
*/
    public void SimulationEnded(SIMULATION_MESSAGE _simulationMessage) {

        // Reset camera
        simulationSceneManager.ResetCamera();

        // Empty aquarium for bacteria. Find any bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");

        // Remove living bacteria if any
        for (int i = 0; i < bacteria.Length; i++) {
            Destroy(bacteria[i]);
        }

        // Destroy any detox objects
        GameObject[] detox = GameObject.FindGameObjectsWithTag("Detox");
        for (int i = 0; i < detox.Length; i++) {
            Destroy(detox[i]);
        }

        // Show SimulationMessagePopup
        simulationSceneManager.simulationMessagePopup.SetActive(true);

        GameObject objTitle = GameObject.Find("SimulationMessageTitleText");
        TextMeshProUGUI simulationMessageTitleText = objTitle.GetComponent<TextMeshProUGUI>();
        GameObject objMessage = GameObject.Find("SimulationMessageText");
        TextMeshProUGUI simulationMessageText = objMessage.GetComponent<TextMeshProUGUI>();

        // Quitting the running simulation
        if (_simulationMessage == SIMULATION_MESSAGE.ENDEDBYQUIT) {

            // Show title
            simulationMessageTitleText.text = "Quitting simulation";

            // Show message
            simulationMessageText.text = "Elapsed time: " + GetElapsedSimulationTimeAsString();
        }

        // The running simulation ended normally
        if (_simulationMessage == SIMULATION_MESSAGE.ENDEDNORMAL) {

            // Show title
            simulationMessageTitleText.text = "Simulation ended";

            // Show message
            simulationMessageText.text = "Elapsed time: " + GetElapsedSimulationTimeAsString();
        }

        // Simulation ran into overpopulation
        if (_simulationMessage == SIMULATION_MESSAGE.OVERPOPULATION) {

            // Show title
            simulationMessageTitleText.text = "Simulation failed -  Overpopulation";

            // Show message
            simulationMessageText.text = "Elapsed time: " + GetElapsedSimulationTimeAsString();
        }

        // Simulation ran into underpopulation
        if (_simulationMessage == SIMULATION_MESSAGE.UNDERPOPULATION) {

            // Show title
            simulationMessageTitleText.text = "Simulation failed - Underpopulation";

            // Show message
            simulationMessageText.text = "Elapsed time: " + GetElapsedSimulationTimeAsString();
        }

        // Simulation ran into to much toxicity in the water
        if (_simulationMessage == SIMULATION_MESSAGE.MAXTOXICIYREACHED) {

            // Show title
            simulationMessageTitleText.text = "Simulation Failed - Too much toxicity in the water";

            // Show message
            simulationMessageText.text = "Elapsed time: " + GetElapsedSimulationTimeAsString();
        }
    }

    public void AddDetoxToSimulation() {

        int numDetox = Int32.Parse(GameObject.Find("DetoxicateNumberText").GetComponent<TextMeshProUGUI>().text);

        simulationSceneManager.addDetoxPopup.SetActive(false);

        // Add detox coroutine
        StartCoroutine(InstantiateAddDetoxToSimulation(numDetox));

    }

    private IEnumerator InstantiateAddDetoxToSimulation(int _numDetox) {

        // Instantiate detox
        for (int i = 0; i < _numDetox; i++) {

            // Generate random spawnposition and rotation above aquarium
            float posX = UnityEngine.Random.Range(-3f, 3f);
            float posY = UnityEngine.Random.Range(8.5f, 10.5f);
            float posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
//            float rotX = UnityEngine.Random.Range(-90f, 90f);
//            float rotY = UnityEngine.Random.Range(-90f, 90f);
//            float rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(simulationSceneManager.detoxPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
//            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Detox " + Guid.NewGuid().ToString();

            // Wait a bit because it looks nice
            float waitPeriod = UnityEngine.Random.Range(0.07f, 0.14f);
            yield return new WaitForSeconds(waitPeriod);
        }
    }

    public int GetYellowWarningToxicityInfo() {
        return currentSimulationConfiguration.yellowWarningToxicityInfo;
    }

    public int GetRedWarningToxicityInfo() {
        return currentSimulationConfiguration.redWarningToxicityInfo;
    }


    public int GetMaxLimitToxicityInfo() {
        return currentSimulationConfiguration.maxLimitToxicityInfo;
    }

}