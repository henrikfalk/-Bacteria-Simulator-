using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAquariumState : AquariumState {

    public RunningAquariumState(SimulationController _simulationController) : base(_simulationController) {
        stateName = STATE.RUNNING;
    }

    public override void Enter() {

        base.Enter();
    }

    public override void Update() {

        // Quit simulation if ket 'Q' is pressed
        if (Input.GetKeyDown(KeyCode.Q) == true) {
            QuitSimulation();
        }

        // If we gets a QUIT_SIMULATION signal then quit simulation
        if (signal == SIGNAL.QUIT_SIMULATION) {

            signal = SIGNAL.NONE;
            QuitSimulation();
        }


        // If we press the 'Escape' key then reset camera
        if (Input.GetKeyDown(KeyCode.Escape) == true) {
            simulationController.simulationSceneManager.ResetCamera();
        }


        // Count living bacteria
        GameObject[] bacteria = GameObject.FindGameObjectsWithTag("Bacteria");

        // If we are more than 200 bacteria then the simulation has failed. This should maybe be donfigurable in the future.
        if (bacteria.Length > 500) {

            // Simulation failed due to overpopulation
            simulationController.SimulationFailed();

        } else {

            // Return if there is any living bateria
            for (int i = 0; i < bacteria.Length; i++) {
                if (bacteria[i].GetComponent<Bacteria>() != null && bacteria[i].GetComponent<Bacteria>().IsDead() == false) {

                    return;
                }
            }

            // Show "Simulation ended" dialog because there no more living bacteria in the simulation
            simulationController.QuitSimulation();
        }

        // Goto EmptyAquariumState
        nextState = new EmptyAquariumState(simulationController);
        stage = EVENT.EXIT;
    }

    public override void Exit() {

        base.Exit();
    }

    private void QuitSimulation() {

        simulationController.QuitSimulation();

        nextState = new EmptyAquariumState(simulationController);
        stage = EVENT.EXIT;
    }
}
