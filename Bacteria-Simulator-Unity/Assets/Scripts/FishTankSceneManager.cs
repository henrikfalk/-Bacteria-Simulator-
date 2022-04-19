using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTankSceneManager : MonoBehaviour
{

    public EnvironmentManager environment;

    public GameObject newSimulationPopup;
    private TwoSliderPopupManager newSimulationPopupManager;

    public GameObject addFoodPopup;
    private TwoSliderPopupManager addFoodPopupManager;

    public GameObject statusPanel;
    private StatusPanelManager statusPanelManager;

    public GameObject bacteriaInfoPanel;
    private BacteriaInfoPanelManager bacteriaInfoPanelManager;
    public GameObject selectedBacteria { get; private set; }
 
    public GameObject greenBacteriaPrefab;
    public GameObject redBacteriaPrefab;
    public GameObject purpleBacteriaPrefab;

    public GameObject foodPrefab;
    public GameObject superFoodPrefab;

    private int initialNumberGreenBacteria;
    private int initialNumberRedBacteria;

    private bool simulationRunning = false;

    // Cameras
    public Camera defaultCamera { get; private set; }
    public Camera lockCamera { get; private set; }

    private Ray ray;
    private RaycastHit hitData;


    void Start() {
        
        newSimulationPopupManager = newSimulationPopup.GetComponent<TwoSliderPopupManager>();
        newSimulationPopup.SetActive(false);

        addFoodPopupManager = addFoodPopup.GetComponent<TwoSliderPopupManager>();
        addFoodPopup.SetActive(false);

        bacteriaInfoPanelManager = bacteriaInfoPanel.GetComponent<BacteriaInfoPanelManager>();
        bacteriaInfoPanel.SetActive(false);

        statusPanelManager = statusPanel.GetComponent<StatusPanelManager>();

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
                ShowBacteriaInfo(selectedBacteria);
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

        if (selectedBacteria != null) {
            bacteriaInfoPanelManager.tempDynamicBNumberText.text = environment.GetEnvironmentTemperature(selectedBacteria.transform.position).ToString("0.0");
        }
    }

    public void NewSimulation() {

        initialNumberGreenBacteria = newSimulationPopupManager.GetSlider1Number();
        initialNumberRedBacteria = newSimulationPopupManager.GetSlider2Number();

        // Empty fishtank for bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");
        
        // remove them
        for (int i = 0; i < bacteria.Length; i++) {
            Destroy(bacteria[i]);
        }

        newSimulationPopup.SetActive(false);

        // reset status UI
        statusPanelManager.greenBNumberText.text = "0";
        statusPanelManager.redBNumberText.text = "0";
        statusPanelManager.purpleBNumberText.text = "0";
        statusPanelManager.temperatureBNumberText.text = environment.GetMiddleTemperature() + " ºC";
        statusPanelManager.deadBNumberText.text = "0";
        statusPanelManager.toxicityBNumberText.text = "0";

        // The simulation is running
        simulationRunning = true;

        // Create new simulation
        StartCoroutine(InstantiateNewSimulation());

    }

    private IEnumerator InstantiateNewSimulation() {

        ArrayList sourceList = new ArrayList();

        // Instantiate Green bacteria 
        for (int i = 0; i < initialNumberGreenBacteria; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = Random.Range(-3f, 3f);
            float posY = Random.Range(10.5f, 12.5f);
            float posZ = Random.Range(-1.5f, -0.5f);
            float rotX = Random.Range(-90f, 90f);
            float rotY = Random.Range(-90f, 90f);
            float rotZ = Random.Range(-90f, 90f);

            GameObject obj = Instantiate(greenBacteriaPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Green" + (i+1).ToString();
            obj.SetActive(false);

            sourceList.Add(obj);
        }

        // Instantiate Red bacteria 
        for (int i = 0; i < initialNumberRedBacteria; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = Random.Range(-3f, 3f);
            float posY = Random.Range(10.5f, 12.5f);
            float posZ = Random.Range(-1.5f, -0.5f);
            float rotX = Random.Range(-90f, 90f);
            float rotY = Random.Range(-90f, 90f);
            float rotZ = Random.Range(-90f, 90f);

            GameObject obj = Instantiate(redBacteriaPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Red" + (i+1).ToString();
            obj.SetActive(false);

            sourceList.Add(obj);
        }

        sourceList = ShuffleArrayList(sourceList);

        int greenCount = 1;
        int redCount = 1;
        foreach (GameObject obj in sourceList) {

            if (obj.name.StartsWith("Green") == true) {
                obj.SetActive(true);
                statusPanelManager.greenBNumberText.text = greenCount.ToString();    
                greenCount++;
            }

            if (obj.name.StartsWith("Red") == true) {
                obj.SetActive(true);
                statusPanelManager.redBNumberText.text = redCount.ToString();    
                redCount++;
            }

            // Wait a bit because it looks nice
            float waitPeriod = Random.Range(0.07f, 0.14f);
            yield return new WaitForSeconds(waitPeriod);

        }
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

        int food = addFoodPopupManager.GetSlider1Number();
        int superFood = addFoodPopupManager.GetSlider2Number();

        addFoodPopup.SetActive(false);

        // Add food
        StartCoroutine(InstantiateAddFoodToSimulation(food, superFood));

    }

    private IEnumerator InstantiateAddFoodToSimulation(int food, int superFood) {

        // Instantiate food
        for (int i = 0; i < food; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = Random.Range(-3f, 3f);
            float posY = Random.Range(10.5f, 12.5f);
            float posZ = Random.Range(-1.5f, -0.5f);
            float rotX = Random.Range(-90f, 90f);
            float rotY = Random.Range(-90f, 90f);
            float rotZ = Random.Range(-90f, 90f);

            GameObject obj = Instantiate(foodPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "Food" + (i+1).ToString();

            // Wait a bit because it looks nice
            float waitPeriod = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitPeriod);
        }

        // Instantiate superFood
        for (int i = 0; i < superFood; i++) {

            // Generate random spawnposition and rotation above fishtank
            float posX = Random.Range(-3f, 3f);
            float posY = Random.Range(10.5f, 12.5f);
            float posZ = Random.Range(-1.5f, -0.5f);
            float rotX = Random.Range(-90f, 90f);
            float rotY = Random.Range(-90f, 90f);
            float rotZ = Random.Range(-90f, 90f);

            GameObject obj = Instantiate(superFoodPrefab, new Vector3(posX,posY,posZ), Quaternion.identity);
            obj.transform.Rotate(new Vector3(rotX,rotY,rotZ));
            obj.name = "SuperFood" + (i+1).ToString();

            // Wait a bit because it looks nice
            float waitPeriod = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitPeriod);
        }


    }

    public void BacteriaDies(GameObject bacteria) {

        string type = bacteria.name;

        if (type.StartsWith("Green") == true) {
            int number = System.Convert.ToInt32(statusPanelManager.greenBNumberText.text);
            number--;
            statusPanelManager.greenBNumberText.text = number.ToString();
            number = System.Convert.ToInt32(statusPanelManager.deadBNumberText.text);
            number++;
            statusPanelManager.deadBNumberText.text = number.ToString();
        }
        if (type.StartsWith("Red") == true) {
            int number = System.Convert.ToInt32(statusPanelManager.redBNumberText.text);
            number--;
            statusPanelManager.redBNumberText.text = number.ToString();
            number = System.Convert.ToInt32(statusPanelManager.deadBNumberText.text);
            number++;
            statusPanelManager.deadBNumberText.text = number.ToString();
        }
        if (type.StartsWith("Dead") == true) {
            int number = System.Convert.ToInt32(statusPanelManager.deadBNumberText.text);
            number--;
            statusPanelManager.deadBNumberText.text = number.ToString();
            number = System.Convert.ToInt32(statusPanelManager.toxicityBNumberText.text);
            number++;
            statusPanelManager.toxicityBNumberText.text = number.ToString();

            // If BacteriaInfo panel for this bacteria is shown then close it
            if (bacteriaInfoPanel.activeSelf == true) {
                if (bacteria.name.Contains(bacteriaInfoPanelManager.nameBNameText.text) == true) {
                    ShowBacteriaInfo(false);    
                }
            }
        }
    }

    public void UpdateBacteriaInfo(string typeAndName) {

        string type = "Unknown";
        if (bacteriaInfoPanel.activeSelf == true) {
            if (typeAndName.Contains("Green") == true) {
                type = "Green";
            }
            if (typeAndName.Contains("Red") == true) {
                type = "Red";
            }
            if (typeAndName.Contains("Purple") == true) {
                type = "Purple";
            }
            if (typeAndName.StartsWith("Dead") == true) {
                type = "Dead";
            }

            bacteriaInfoPanelManager.typeBNameText.text = type;
            bacteriaInfoPanelManager.nameBNameText.text = typeAndName;

        }
    }

    public void ShowBacteriaInfo(bool showInfo) {
        bacteriaInfoPanel.SetActive(showInfo);
    }

    public void ShowBacteriaInfo(GameObject obj) {
        selectedBacteria = obj;
        string type = selectedBacteria.name;
        if (type.StartsWith("Green") == true) {
            type = "Green";
        }
        if (type.StartsWith("Red") == true) {
            type = "Red";
        }
        if (type.StartsWith("Purple") == true) {
            type = "Purple";
        }
        if (type.StartsWith("Dead") == true) {
            type = "Dead";
        }

        bacteriaInfoPanelManager.typeBNameText.text = type;
        bacteriaInfoPanelManager.nameBNameText.text = selectedBacteria.name;

        bacteriaInfoPanelManager.tempDynamicBNumberText.text = environment.GetEnvironmentTemperature(selectedBacteria.transform.position).ToString("0.0");

        bacteriaInfoPanel.SetActive(true);
    }

}
