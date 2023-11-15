using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimulationSceneManager : MonoBehaviour
{

    public SimulationController simulationController;

    // Pannels and popups
    public GameObject newSimulationPopup;

    public GameObject simulationToolsPopup;

    public GameObject addFoodPopup;

    public GameObject addDetoxPopup;

    public GameObject simulationMessagePopup;

//    public GameObject simulationEndedPopup;

//    public GameObject simulationFailedPopup;

    public GameObject statusPanel;

    public GameObject helpPanel;

    public GameObject toolbarPanel;


    public GameObject aquarium;

    // Particle Systems
    public ParticleSystem dyingBacteriaParticles;

    private StatusPanelController statusPanelController;

    public GameObject bacteriaInfoPanel;
    private BacteriaInfoPanelController bacteriaInfoPanelController;

    // ENCAPSULATION
    public GameObject selectedBacteria { get; private set; }
 
    public GameObject greenBacteriaPrefab;
    public GameObject redBacteriaPrefab;
    public GameObject purpleBacteriaPrefab;

    public GameObject detoxPrefab;

//    private int initialNumberGreenBacteria;
//    private int initialNumberRedBacteria;

    // How long has this simulation been running?
//    public DateTime simulationStartTime;
//    private TimeSpan elapsedSimulationTime;

    // Cameras
    public Camera defaultCamera { get; private set; }
    public Camera lockCamera { get; private set; }

    private Ray ray;
    private RaycastHit hitData;


    void Start() {
        
        // Add a SimulationController to the SimulationSceneManager
        simulationController =  this.gameObject.AddComponent<SimulationController>();
        simulationController.simulationSceneManager = this;

        addFoodPopup.SetActive(false);
        addDetoxPopup.SetActive(false);
        helpPanel.SetActive(false);
        simulationToolsPopup.SetActive(false);

        simulationMessagePopup.SetActive(false);
//        simulationEndedPopup.SetActive(false);
//        simulationFailedPopup.SetActive(false);

        bacteriaInfoPanelController = bacteriaInfoPanel.GetComponent<BacteriaInfoPanelController>();
        bacteriaInfoPanel.SetActive(false);

        statusPanelController = statusPanel.GetComponent<StatusPanelController>();

        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        defaultCamera.gameObject.SetActive(true);

        lockCamera = GameObject.Find("Lock Camera").GetComponent<Camera>();
        lockCamera.gameObject.SetActive(false);

        // 
        
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) == true && (simulationController.currentState.stateName == AquariumState.STATE.RUNNING || simulationController.currentState.stateName == AquariumState.STATE.PAUSED)) {
            //  && addFoodPopup.activeSelf == false

            if (defaultCamera.gameObject.activeSelf == true) {
                ray = defaultCamera.ScreenPointToRay(Input.mousePosition);
            } else {
                ray = lockCamera.ScreenPointToRay(Input.mousePosition);
            }
            if (Physics.Raycast(ray, out hitData) == true) {
                GameObject hitObject = hitData.collider.gameObject;
                Bacteria isBacteria = hitObject.GetComponent<Bacteria>();
                if (isBacteria == null) {
                    return;
                }
                selectedBacteria = hitObject;
                lockCamera.GetComponent<LockCameraController>().selectedObject = selectedBacteria;
                ShowBacteriaInfo(true);
            } else {
                selectedBacteria = null;
                if (lockCamera.gameObject.activeSelf == true) {
                    lockCamera.gameObject.SetActive(false);
                    defaultCamera.gameObject.SetActive(true);
                }
                lockCamera.GetComponent<LockCameraController>().selectedObject = selectedBacteria;
                ShowBacteriaInfo(false);
            }
        }
/* HFALK move to RunningAquariumState
        // If we press the 'f' key then show "AddFoodPopup"
        if (Input.GetKeyDown(KeyCode.F) == true) {

            // If the fishtank is empty the do nothing
            GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
            if (bacteria.Length == 0) {
                return;
            } else {

                // Show add food dialog
                addFoodPopup.SetActive(true);
            }

        }
*/
        // If we press the 'h' key then show "HelpPanel"
        if (Input.GetKeyDown(KeyCode.H) == true) {
            if (helpPanel.activeSelf == false) {
                helpPanel.SetActive(true);
            } else {
                helpPanel.SetActive(false);
            }
        }

    }

    public void NewSimulation() {

        // Notify the FSM
        simulationController.newSimulation = true;

    }

    public void QuitSimulation() {
        
    }

    public void MakeSiblingBacteria(String type, Vector3 pos) {

            GameObject obj = null;
            float rotX = UnityEngine.Random.Range(-90f, 90f);
            float rotY = UnityEngine.Random.Range(-90f, 90f);
            float rotZ = UnityEngine.Random.Range(-90f, 90f);

            // make green bacteria
            if (type == "Green") {
                
                obj = Instantiate(greenBacteriaPrefab, pos, Quaternion.identity);
//                obj.name = "Green Child " + Time.frameCount;
                obj.name = "Green Child " + Guid.NewGuid().ToString();
            }

            // make red bacteria
            if (type == "Red") {
                
                obj = Instantiate(redBacteriaPrefab, pos, Quaternion.identity);
//                obj.name = "Red Child " + Time.frameCount;
                obj.name = "Red Child " + Guid.NewGuid().ToString();
            }

            // make purple bacteria
            if (type == "Purple") {
                
                obj = Instantiate(purpleBacteriaPrefab, pos, Quaternion.identity);
//                obj.name = "Purple Child " + Time.frameCount;
                obj.name = "Purple Child " + Guid.NewGuid().ToString();
            }

            if (obj != null) {
                obj.GetComponent<Bacteria>().StartFSM(BacteriaState.STATE.ALIVE);
                obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
                obj.GetComponent<Rigidbody>().useGravity = false;
            }
    } 

    public void MakePurpleBacteria(Vector3 pos) {

                float rotX = UnityEngine.Random.Range(-90f, 90f);
                float rotY = UnityEngine.Random.Range(-90f, 90f);
                float rotZ = UnityEngine.Random.Range(-90f, 90f);
                
                GameObject obj = Instantiate(purpleBacteriaPrefab, pos, Quaternion.identity);
                obj.GetComponent<Bacteria>().StartFSM(BacteriaState.STATE.ALIVE);
                obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
//                obj.name = "Purple " + Time.frameCount;
                obj.name = "Purple " + Guid.NewGuid().ToString();
                obj.GetComponent<Rigidbody>().useGravity = false;
    } 
/*
    public void AddFoodToSimulation() {

        int food = Int32.Parse(GameObject.Find("FoodNumberText").GetComponent<TextMeshProUGUI>().text);
        int superFood = Int32.Parse(GameObject.Find("SuperFoodNumberText").GetComponent<TextMeshProUGUI>().text);

        addFoodPopup.SetActive(false);

        // Add food
        StartCoroutine(InstantiateAddFoodToSimulation(food, superFood));

    }

    private IEnumerator InstantiateAddFoodToSimulation(int food, int superFood) {

        // Instantiate food
        for (int i = 0; i < food; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = UnityEngine.Random.Range(-3f, 3f);
            float posY = UnityEngine.Random.Range(10.5f, 12.5f);
            float posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
            float rotX = UnityEngine.Random.Range(-90f, 90f);
            float rotY = UnityEngine.Random.Range(-90f, 90f);
            float rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(foodPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Food" + (i+1).ToString();

            // Wait a bit because it looks nice
            float waitPeriod = UnityEngine.Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitPeriod);
        }

        // Instantiate superFood
        for (int i = 0; i < superFood; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = UnityEngine.Random.Range(-3f, 3f);
            float posY = UnityEngine.Random.Range(10.5f, 12.5f);
            float posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
            float rotX = UnityEngine.Random.Range(-90f, 90f);
            float rotY = UnityEngine.Random.Range(-90f, 90f);
            float rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(superFoodPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "SuperFood" + (i+1).ToString();

            // Wait a bit because it looks nice
            float waitPeriod = UnityEngine.Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitPeriod);
        }


    }
*/
    public void ShowBacteriaInfo(bool showInfo) {

        bacteriaInfoPanelController.bacteria = selectedBacteria;
        bacteriaInfoPanel.SetActive(showInfo);

    }

    public Boolean IsSimulationRunning() {

        if (simulationController != null || simulationController.currentState.stateName == AquariumState.STATE.RUNNING || simulationController.currentState.stateName == AquariumState.STATE.PAUSED) {
            return true;
        }
        return false;
    }

    public void ResetCamera() {
                // Reset camera
            // show mainCamera
            lockCamera.gameObject.SetActive(false);
            defaultCamera.gameObject.SetActive(true);

            // Reset 
            defaultCamera.transform.position = new Vector3(0, 1, -15);
    }

    public void ToggleLookAtSelected() {

        if (selectedBacteria != null) {
            if (defaultCamera.gameObject.activeSelf == true) {
                // Show lockCamera
                defaultCamera.gameObject.SetActive(false);
                lockCamera.gameObject.SetActive(true);
            } else {
                // show mainCamera
                lockCamera.gameObject.SetActive(false);
                defaultCamera.gameObject.SetActive(true);
            }
        }
    }

    public SimulationController GetSimulationController() {
        return simulationController;
    }

    public void AddDetoxToSimulation() {

        simulationController.AddDetoxToSimulation();
    }

}
