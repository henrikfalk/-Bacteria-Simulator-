using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaState
{

    public enum STATE {
        INIT,
        ALIVE,
        DEAD
    };

    public enum EVENT {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE stateName;

    protected EVENT stage;

    protected BacteriaState nextState;

    protected Bacteria bacteria;

    public BacteriaState(Bacteria _bacteria) {

        bacteria = _bacteria;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public BacteriaState Process() {

        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT) {

            Exit();
            return nextState;
        }

        return this;
    }

}
