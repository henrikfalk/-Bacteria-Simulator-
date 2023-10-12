using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class FishTankSceneManager : MonoBehaviour
{

    public EnvironmentManager environment;

    public GameObject newSimulationPopup;
//    private TwoSliderPopupManager newSimulationPopupManager;

    public GameObject addFoodPopup;
//    private TwoSliderPopupManager addFoodPopupManager;

    public GameObject simulationEndedPopup;

    public GameObject statusPanel;

    public GameObject helpPanel;

    private StatusPanelController statusPanelController;

    public GameObject bacteriaInfoPanel;
    private BacteriaInfoPanelManager bacteriaInfoPanelManager;

    // ENCAPSULATION
    public GameObject selectedBacteria { get; private set; }
 
    public GameObject greenBacteriaPrefab;
    public GameObject redBacteriaPrefab;
    public GameObject purpleBacteriaPrefab;

    public GameObject foodPrefab;
    public GameObject superFoodPrefab;

    private int initialNumberGreenBacteria;
    private int initialNumberRedBacteria;

    private bool simulationRunning = false;
    private bool simulationInitializing = false;

    // How long has this simulation been running?
    public DateTime simulationStartTime;
    private TimeSpan elapsedSimulationTime;

    // Cameras
    public Camera defaultCamera { get; private set; }
    public Camera lockCamera { get; private set; }

    private Ray ray;
    private RaycastHit hitData;


    void Start() {
        
//        newSimulationPopupManager = newSimulationPopup.GetComponent<TwoSliderPopupManager>();
        newSimulationPopup.SetActive(false);

//        addFoodPopupManager = addFoodPopup.GetComponent<TwoSliderPopupManager>();
        addFoodPopup.SetActive(false);

        simulationEndedPopup.SetActive(false);

        bacteriaInfoPanelManager = bacteriaInfoPanel.GetComponent<BacteriaInfoPanelManager>();
        bacteriaInfoPanel.SetActive(false);

        statusPanelController = statusPanel.GetComponent<StatusPanelController>();

        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        defaultCamera.gameObject.SetActive(true);

        lockCamera = GameObject.Find("Lock Camera").GetComponent<Camera>();
        lockCamera.gameObject.SetActive(false);

    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) == true && simulationRunning == true && addFoodPopup.activeSelf == false) {

            if (defaultCamera.gameObject.activeSelf == true) {
                ray = defaultCamera.ScreenPointToRay(Input.mousePosition);
            } else {
                ray = lockCamera.ScreenPointToRay(Input.mousePosition);
            }
            if (Physics.Raycast(ray, out hitData) == true) {
                selectedBacteria = hitData.collider.gameObject;
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


        // If we press the 'n' key then show "NewSimulationPopup"
        if (Input.GetKeyDown(KeyCode.N) == true) {

            if (simulationRunning == true) {
                QuitSimulation();
            }

            if (addFoodPopup.activeSelf == true) {
                return;
            }

            newSimulationPopup.SetActive(true);
        }

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

                // If 'l' pressed then lock cinemachineVirtualCamera on selected bacteria
        if (Input.GetKeyDown(KeyCode.L) == true) {
        
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

        // If we press the 'q' key then quit the simulation
        if (Input.GetKeyDown(KeyCode.Q) == true && simulationInitializing == false) {
            QuitSimulation();
        }

        // If we press the 'Escape' key then quit the simulation
        if (Input.GetKeyDown(KeyCode.Escape) == true) {
            ResetCamera();
        }

        // Update statusPanel
        if (simulationRunning == true) {
            elapsedSimulationTime = DateTime.Now - simulationStartTime;
            statusPanelController.UpdateStatus(elapsedSimulationTime);
        } else {
            // reset status UI
            statusPanelController.UpdateStatus(new TimeSpan(0));
        }


        // If all bacterias are dead then it is game over ie. the simulation has ended
        if (simulationRunning == true) {

            // Check for bacteria
            GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");

            // All bacteria are dead?
//            Boolean allDead = false;
            for (int i = 0; i < bacteria.Length; i++) {
                if (bacteria[i].GetComponent<Bacteria>().IsDead() == false) {
                        return;
                }
            }

            // Show "Simulation ended" dialog
            QuitSimulation();
            simulationEndedPopup.SetActive(true);

        }

    }

    public void NewSimulation() {

        initialNumberGreenBacteria = Int32.Parse(GameObject.Find("GreenBacteriaNumberText").GetComponent<TextMeshProUGUI>().text);
        initialNumberRedBacteria = Int32.Parse(GameObject.Find("RedBacteriaNumberText").GetComponent<TextMeshProUGUI>().text);

        // Empty fishtank for bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
        
        // remove them
        for (int i = 0; i < bacteria.Length; i++) {
            Destroy(bacteria[i]);
        }

        newSimulationPopup.SetActive(false);

        // reset status UI
        statusPanelController.UpdateStatus(new TimeSpan(0));

        // The simulation is running
        simulationRunning = true;

        // Start timer
        simulationStartTime = DateTime.Now;

        // Create new simulation
        StartCoroutine(InstantiateNewSimulation());

    }

    public void QuitSimulation() {

        if (simulationRunning == false) {

            if (GameManager.Instance != null) {
                GameManager.Instance.GotoScene("MenuScene");
            } else {
                #if UNITY_EDITOR
                    EditorApplication.ExitPlaymode();
                #endif
            }
        } else {

            ResetCamera();

            StopCoroutine(InstantiateNewSimulation());

            simulationRunning = false;

            // Empty fishtank for bacteria
            GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
        
            // remove them
            for (int i = 0; i < bacteria.Length; i++) {
                Destroy(bacteria[i]);
            }

            // reset status UI
            statusPanelController.UpdateStatus(new TimeSpan(0));

//            simulationEndedPopup.SetActive(false);

        }

    }

    private IEnumerator InstantiateNewSimulation() {

        simulationInitializing = true;

        ArrayList sourceList = new ArrayList();

        // Instantiate Green bacteria 
        for (int i = 0; i < initialNumberGreenBacteria; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = UnityEngine.Random.Range(-3f, 3f);
            float posY = UnityEngine.Random.Range(10.5f, 12.5f);
            float posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
            float rotX = UnityEngine.Random.Range(-90f, 90f);
            float rotY = UnityEngine.Random.Range(-90f, 90f);
            float rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(greenBacteriaPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Green" + (i+1).ToString();
            obj.SetActive(false);

            sourceList.Add(obj);
        }

        // Instantiate Red bacteria 
        for (int i = 0; i < initialNumberRedBacteria; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = UnityEngine.Random.Range(-3f, 3f);
            float posY = UnityEngine.Random.Range(10.5f, 12.5f);
            float posZ = UnityEngine.Random.Range(-1.5f, -0.5f);
            float rotX = UnityEngine.Random.Range(-90f, 90f);
            float rotY = UnityEngine.Random.Range(-90f, 90f);
            float rotZ = UnityEngine.Random.Range(-90f, 90f);

            GameObject obj = Instantiate(redBacteriaPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
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

        simulationInitializing = false;

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

    public void MakeSiblingBacteria(GameObject parent) {

            // make green bacteria if parent is green
            if (parent.name.StartsWith("Green") == true) {
                float rotX = UnityEngine.Random.Range(-90f, 90f);
                float rotY = UnityEngine.Random.Range(-90f, 90f);
                float rotZ = UnityEngine.Random.Range(-90f, 90f);
                
                // Generate a random position a bit away from parent
                Vector3 pos = parent.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f));

                GameObject obj = Instantiate(greenBacteriaPrefab, pos, Quaternion.identity);
                obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
                obj.name = "Green Child";
                obj.GetComponent<Rigidbody>().useGravity = false;

            }

    } 

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

    public void ShowBacteriaInfo(bool showInfo) {

        bacteriaInfoPanelManager.bacteria = selectedBacteria;

        bacteriaInfoPanel.SetActive(showInfo);
    }

    public Boolean IsSimulationRunning() {
        return simulationRunning;
    }

    public void ResetCamera() {
                // Reset camera
            // show mainCamera
            lockCamera.gameObject.SetActive(false);
            defaultCamera.gameObject.SetActive(true);

            // Reset 
            defaultCamera.transform.position = new Vector3(0, 1, -15);
    }

}
