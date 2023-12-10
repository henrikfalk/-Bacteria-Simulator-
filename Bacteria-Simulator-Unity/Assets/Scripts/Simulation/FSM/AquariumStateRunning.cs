using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumStateRunning : AquariumState {

    List<GameObject> livingBacteria = new List<GameObject>();

    public AquariumStateRunning(SimulationController _simulationController) : base(_simulationController) {
        stateName = STATE.RUNNING;
    }

    public override void Enter() {

        // Start updating the toxicity value
//        simulationController.UpdateToxicity();

        simulationController.simulationSceneManager.simulationToolsPopup.SetActive(true);

        base.Enter();
    }

    public override void Update() {

        // Quit simulation if ket 'Q' is pressed
        if (Input.GetKeyDown(KeyCode.Q) == true) {
            QuitSimulation();
            return;
        }

        // If we gets a QUIT_SIMULATION signal then quit simulation
        if (signal == SIGNAL.QUIT_SIMULATION) {

            signal = SIGNAL.NONE;
            QuitSimulation();
            return;
        }

        // Pause simulation if ket 'P' or spacebar is pressed
        if (Input.GetKeyDown(KeyCode.P) == true || Input.GetKeyDown(KeyCode.Space) == true) {
            PauseSimulation();
            return;
        }

        // If we gets a QUIT_SIMULATION signal then quit simulation
        if (signal == SIGNAL.PAUSE_SIMULATION) {

            signal = SIGNAL.NONE;
            PauseSimulation();
            return;
        }

        // If we press the 'Escape' key then reset camera
        if (Input.GetKeyDown(KeyCode.Escape) == true) {
            simulationController.simulationSceneManager.ResetCamera();
        }

        // If we press the 'm' key then show "AddDetoxPopup"
        if (Input.GetKeyDown(KeyCode.M) == true) {

            // If the aquarium is empty the do nothing HFALK: Implement better handling!
            GameObject[] bac = GameObject.FindGameObjectsWithTag("Bacteria");
            if (bac.Length == 0) {
                return;
            } else {
                AddDetoxToSimulation();
            }

        }

        // If we gets a ADD_DETOX signal then add detox to simulation
        if (signal == SIGNAL.ADD_DETOX) {

            signal = SIGNAL.NONE;
            AddDetoxToSimulation();
            return;
        }

        // If 'l' pressed then lock Camera on selected bacteria
        if (Input.GetKeyDown(KeyCode.L) == true) {
        
            ToggleLookAtSelected();
        }

        if (IsSimulationFailed() == false) {
            return;
        }

        // Goto EmptyAquariumState
        nextState = new AquariumStateEmpty(simulationController);
        stage = EVENT.EXIT;
    }

    public override void Exit() {

        simulationController.simulationSceneManager.simulationToolsPopup.SetActive(false);

        base.Exit();
    }

    private void QuitSimulation() {

        simulationController.SimulationEnded(SimulationController.SIMULATION_MESSAGE.ENDEDBYQUIT);

        nextState = new AquariumStateEmpty(simulationController);
        stage = EVENT.EXIT;
    }

    private void PauseSimulation() {

        nextState = new AquariumStatePaused(simulationController);
        stage = EVENT.EXIT;
    }

    private Boolean IsSimulationFailed() {

        // Count living bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");

        // **** Test for maximun allowed population

        // If the toxicity level exceeds 2000 the 
        if (simulationController.GetToxicity() > simulationController.GetMaxLimitToxicityInfo()) {
            // Show "Simulation message" dialog because the toxicity is too high
            simulationController.SimulationEnded(SimulationController.SIMULATION_MESSAGE.MAXTOXICIYREACHED);

            return true;

        }

        // If we are more than 500 bacteria then the simulation has failed. HFALK This should maybe be donfigurable in the future.
        if (bacteria.Length > 500) {

            // Show "Simulation ended" dialog because there no more living bacteria in the simulation
            simulationController.SimulationEnded(SimulationController.SIMULATION_MESSAGE.OVERPOPULATION);

            return true;
        }

        // **** Test if there are any living bacteria

        // Test if there two laft and they are of same type

        livingBacteria.Clear();

        for (int i = 0; i < bacteria.Length; i++) {
            if (bacteria[i].GetComponent<Bacteria>() != null && bacteria[i].GetComponent<Bacteria>().IsDead() == false) {

                livingBacteria.Add(bacteria[i]);
            }
        }

        // Return false if there is any living bateria
        // Note: We should end earlier when there is no way the bacteria can populate. Example: 1 bacteria left etc.
        if (livingBacteria.Count >= 2) {
            return false;
        }

        // Show "Simulation ended" dialog because there no more bacteria in the simulation that can breed
        simulationController.SimulationEnded(SimulationController.SIMULATION_MESSAGE.ENDEDNORMAL);

        return true;
    }

    private void ToggleLookAtSelected() {

        simulationController.simulationSceneManager.ToggleLookAtSelected();
    }

    private void AddDetoxToSimulation() {

        // Show add food dialog
        simulationController.simulationSceneManager.addDetoxPopup.SetActive(true);

    }
}
