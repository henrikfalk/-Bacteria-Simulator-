using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationToolsPopupController : MonoBehaviour
{

    public SimulationSceneManager simulationSceneManager;

    private SimulationController simulationController;

    public Button lookButton;
    public Button addDetoxButton;
    public Button addFoodButton;

    // Start is called before the first frame update
    void Start()
    {
        lookButton.interactable = false;

        // Get a handle to the SimulationSceneManager
        GameObject obj = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj.GetComponent<SimulationSceneManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (simulationSceneManager == null) {
            return;
        }

        if (simulationSceneManager.selectedBacteria != null) {
            lookButton.interactable = true;
        } else {
            lookButton.interactable = false;
        }

        if (simulationSceneManager.simulationController.GetToxicity() > 0) {
            addDetoxButton.interactable = true;
        } else {
            addDetoxButton.interactable = false;
        }
    }

    public void LookAtSelected () {

        simulationSceneManager.ToggleLookAtSelected();
    }

    public void AddDetoxToSimulation() {
        simulationSceneManager.simulationController.currentState.Signal(AquariumState.SIGNAL.ADD_DETOX);
    }

}
