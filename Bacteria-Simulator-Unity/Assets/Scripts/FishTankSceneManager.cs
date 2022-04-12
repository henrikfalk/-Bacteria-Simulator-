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

    public GameObject greenBacteriaPrefab;
    public GameObject redBacteriaPrefab;
    public GameObject purpleBacteriaPrefab;

    public GameObject foodPrefab;
    public GameObject superFoodPrefab;

    private int initialNumberGreenBacteria;
    private int initialNumberRedBacteria;

    void Start()
    {
        
        newSimulationPopupManager = newSimulationPopup.GetComponent<TwoSliderPopupManager>();
        newSimulationPopup.SetActive(false);

        addFoodPopupManager = addFoodPopup.GetComponent<TwoSliderPopupManager>();
        addFoodPopup.SetActive(false);

    }

    void Update()
    {
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

        // Create new simulation
        StartCoroutine(InstantiateNewSimulation());

    }

    private IEnumerator InstantiateNewSimulation() {

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

            // Wait a bit because it looks nice
            float waitPeriod = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitPeriod);
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

            // Set environment Manager for Bacteria
            Bacteria bacteria = obj.GetComponent<Bacteria>();
            bacteria.environment = environment;

            // Wait a bit because it looks nice
            float waitPeriod = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitPeriod);
        }


    }

    public void MakeSiblingBacteria(GameObject parent){

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

}
