using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumStatePaused : AquariumState {

    private AquariumState previousRunningState;

    private DateTime pausedTime;

    public AquariumStatePaused(SimulationController _simulationController) : base(_simulationController) {

        stateName = STATE.PAUSED;
    }

    public AquariumStatePaused(SimulationController _simulationController, AquariumState _previousRunningState) : base(_simulationController) {

        stateName = STATE.PAUSED;
        previousRunningState = _previousRunningState;

    }

    public override void Enter() {

        // pause simulation
        Time.timeScale = 0f;

        // Pause audio if any
        AudioListener.pause = true;

        // remember time of pause
        pausedTime = DateTime.Now;

        base.Enter();
    }

    public override void Update() {

        // Resume simulation if ket 'P' or spacebar is pressed
        if (Input.GetKeyDown(KeyCode.P) == true || Input.GetKeyDown(KeyCode.Space) == true) {

            signal = SIGNAL.NONE;
            ResumeSimulation();
            return;
        }

        if (signal == SIGNAL.PAUSE_SIMULATION) {

            signal = SIGNAL.NONE;
            ResumeSimulation();
            return;
        }
    }

    public override void Exit() {

        base.Exit();
    }

    private void ResumeSimulation() {

        // resume simulation
        Time.timeScale = 1f;

        // Resume audio if any
        AudioListener.pause = false;

        // Flush the toxicity list if bateria has been killed during pause
        simulationController.FlushToxicity();

        // Update simulationtime
        simulationController.SetSimulationStartTime(simulationController.GetSimulationStartTime() + (DateTime.Now - pausedTime));

        nextState = new AquariumStateRunning(simulationController);;

        stage = EVENT.EXIT;
    }

}
