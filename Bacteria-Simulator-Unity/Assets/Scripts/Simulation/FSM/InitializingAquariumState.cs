using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializingAquariumState : AquariumState {

    public InitializingAquariumState(SimulationController _simulationController) : base(_simulationController) {
        stateName = STATE.INITIALIZING;
    }

    public override void Enter() {

        // Now is the time to start the simulation
        simulationController.SetSimulationStartTime(DateTime.Now);

        // Create new simulation
        simulationController.InitializeSimulation();

        base.Enter();
    }

    public override void Update() {

        if (simulationController.initializedSimulation == true) {
            nextState = new RunningAquariumState(simulationController);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit() {

        simulationController.initializedSimulation = false;

        base.Exit();
    }

}
