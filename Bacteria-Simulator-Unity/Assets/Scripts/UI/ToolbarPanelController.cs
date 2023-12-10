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
    public Button laboratoryButton;

    public GameObject helpPanel;

    // Start is called before the first frame update
    void Start() {
/*
        // Get a handle to the SimulationSceneManager
        GameObject obj = GameObject.Find("SimulationSceneManager");
        simulationSceneManager = obj.GetComponent<SimulationSceneManager>();

        simulationController = simulationSceneManager.GetSimulationController();
*/
    }

    // Update is called once per frame
    void Update() {

        // Failsafe
        if (simulationSceneManager == null) {
            GameObject obj = GameObject.Find("SimulationSceneManager");
            simulationSceneManager = obj.GetComponent<SimulationSceneManager>();
        }

        if (simulationController == null) {
            simulationController = simulationSceneManager.GetSimulationController();
            return;
        }

        // HFALK Code can be optimized :)

        // Update buttons
        if (simulationController.currentState.stateName == AquariumState.STATE.EMPTY) {
            newSimulationButton.interactable = true;
            pauseSimulationButton.interactable = false;
            quitSimulationButton.interactable = false;
            exitGameButton.interactable = true;
            laboratoryButton.interactable = true;
        } else {
            newSimulationButton.interactable = false;
            exitGameButton.interactable = false;
            laboratoryButton.interactable = false;
        }

        // If we are initializing
        if (simulationController.currentState.stateName == AquariumState.STATE.INITIALIZING) {
            pauseSimulationButton.interactable = false;
        }

        // Update quit and pause buttons
        if (simulationController.currentState.stateName == AquariumState.STATE.RUNNING) {
            quitSimulationButton.interactable = true;
            pauseSimulationButton.interactable = true;
        } else {
            quitSimulationButton.interactable = false;
        }

    }

    public void NewSimulation() {

        simulationController.currentState.Signal(AquariumState.SIGNAL.NEW_SIMULATION);
    }

    public void PauseSimulation() {

        simulationController.currentState.Signal(AquariumState.SIGNAL.PAUSE_SIMULATION);
    }

    public void QuitSimulation() {
        
        simulationController.currentState.Signal(AquariumState.SIGNAL.QUIT_SIMULATION);
    }

    public void ExitSimulation() {
        
        simulationController.currentState.Signal(AquariumState.SIGNAL.EXIT_SIMULATION);
    }

    public void GotoLaboratory() {

        if (GameManager.Instance != null) {
            GameManager.Instance.GotoScene("LaboratoryScene");
        }
    }

    public void ToggleHelp() {
        if (helpPanel.activeSelf == false) {
            helpPanel.SetActive(true);
        } else {
            helpPanel.SetActive(false);
        }
    }
}
