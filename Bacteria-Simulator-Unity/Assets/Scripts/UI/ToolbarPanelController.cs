using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarPanelController : MonoBehaviour
{

    public SimulationSceneManager simulationSceneManager;

    private SimulationController simulationController;

    public Button newSimulationButton;
    public Button pauseSimulationButton;
    public Button quitSimulationButton;
    public Button helpSimulationButton;
    public Button exitGameButton;

    public GameObject helpPanel;

    // Start is called before the first frame update
    void Start() {
        // Get a handle to the SimulationSceneManager
        GameObject obj = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj.GetComponent<SimulationSceneManager>();
        

    }

    // Update is called once per frame
    void Update() {

        if (simulationSceneManager == null && simulationController.currentState == null) {
            return;
        }
        simulationController = simulationSceneManager.GetSimulationController();

        // Update new simulation button
        if (simulationController.currentState.stateName == AquariumState.STATE.EMPTY) {
            newSimulationButton.interactable = true;
            quitSimulationButton.interactable = false;
            exitGameButton.interactable = true;
        } else {
            newSimulationButton.interactable = false;
            exitGameButton.interactable = false;
        }

        // Update quit simulation button
        if (simulationController.currentState.stateName == AquariumState.STATE.RUNNING) {
            quitSimulationButton.interactable = true;
//            pauseSimulationButton.interactable = true;
        } else {
            quitSimulationButton.interactable = false;
//            pauseSimulationButton.interactable = false;
        }

    }

    public void NewSimulation() {

        simulationController.currentState.Signal(AquariumState.SIGNAL.NEW_SIMULATION);
    }

    public void QuitSimulation() {
        
        simulationController.currentState.Signal(AquariumState.SIGNAL.QUIT_SIMULATION);
    }

    public void ExitGame() {
        
        simulationController.currentState.Signal(AquariumState.SIGNAL.EXIT_GAME);
    }

    public void ToggleHelp() {
        if (helpPanel.activeSelf == false) {
            helpPanel.SetActive(true);
        } else {
            helpPanel.SetActive(false);
        }
    }
}
