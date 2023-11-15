using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class AquariumStateEmpty : AquariumState {

    public GameObject newSimulationPopup;

    public AquariumStateEmpty(SimulationController _simulationController) : base(_simulationController) {
        stateName = STATE.EMPTY;
    }

    public override void Enter() {

        // Reset simulation time in SimulationController
        simulationController.simulationStartTime = new DateTime(0);

        // reset toxicity
        simulationController.ResetToxicity();

        // Hide NewSimulationPopup during startup since we have an empty simulation
        newSimulationPopup = simulationController.simulationSceneManager.newSimulationPopup;
        newSimulationPopup.SetActive(false);

        base.Enter();
    }

    public override void Update() {

        // If we press the 'n' key then show "NewSimulationPopup"
        if (Input.GetKeyDown(KeyCode.N) == true) {
            NewSimulation();
        }

        // If we gets a NEW_SIMULATION signal then show "NewSimulationPopup"
        if (signal == SIGNAL.NEW_SIMULATION) {

            signal = SIGNAL.NONE;
            NewSimulation();
        }

        // If we press the 'q' key and no simulation is running then quit the simulation
        if (Input.GetKeyDown(KeyCode.Q) == true) {
            QuitSimulation();
        }

        // If we gets a EXIT_GAME signal then exit game
        if (signal == SIGNAL.EXIT_GAME) {

            signal = SIGNAL.NONE;
            QuitSimulation();
        }

        // If the user Press 'Ok' in "NewSimulationPopup" goto initializing state
        if (simulationController.newSimulation == true) {
            nextState = new AquariumStateInitializing(simulationController);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit() {

        simulationController.initialNumberGreenBacteria = Int32.Parse(GameObject.Find("GreenBacteriaNumberText").GetComponent<TextMeshProUGUI>().text);
        simulationController.initialNumberRedBacteria = Int32.Parse(GameObject.Find("RedBacteriaNumberText").GetComponent<TextMeshProUGUI>().text);

        // Hide 'NewSimulatiopnPopup'
        newSimulationPopup.SetActive(false);
        simulationController.newSimulation = false;

        base.Exit();
    }

    private void NewSimulation() {

        // Hide SimulationEndedPopup if open. No need to remember
        simulationController.simulationSceneManager.simulationMessagePopup.SetActive(false);
        newSimulationPopup.SetActive(true);
    }

    private void QuitSimulation() {

        if (GameManager.Instance != null) {
            GameManager.Instance.GotoScene("MainMenuScene");
        } else {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #endif
        }

    }

}
