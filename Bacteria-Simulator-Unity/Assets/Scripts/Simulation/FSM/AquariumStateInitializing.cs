using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumStateInitializing : AquariumState {

    public AquariumStateInitializing(SimulationController _simulationController) : base(_simulationController) {
        stateName = STATE.INITIALIZING;
    }

    public override void Enter() {

        // Create new simulation
        simulationController.InitializeSimulation();

        base.Enter();
    }

    public override void Update() {

        if (simulationController.initializedSimulation == true) {
            nextState = new AquariumStateRunning(simulationController);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit() {

        simulationController.initializedSimulation = false;

        // Now is the time to start the simulation
        simulationController.SetSimulationStartTime(DateTime.Now);

        base.Exit();
    }

}
