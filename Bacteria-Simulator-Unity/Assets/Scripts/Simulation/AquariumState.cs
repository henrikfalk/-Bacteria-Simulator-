using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumState
{

    public enum STATE {
        EMPTY,
        INITIALIZING,
        RUNNING
    };

    public enum EVENT {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE stateName;

    protected EVENT stage;

    protected AquariumState nextState;

    protected SimulationController simulationController;

    public AquariumState(SimulationController _simulationController) {

        simulationController = _simulationController;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public AquariumState Process() {

        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT) {

            Exit();
            return nextState;
        }

        return this;
    }

}
