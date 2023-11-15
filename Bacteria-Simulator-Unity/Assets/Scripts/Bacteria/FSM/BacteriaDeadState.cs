using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class BacteriaStateDead : BacteriaState {

    public GameObject newSimulationPopup;

    public BacteriaStateDead(Bacteria _bacteria) : base(_bacteria) {
        stateName = STATE.DEAD;
    }

    public override void Enter() {

        base.Enter();
    }

    public override void Update() {

    }

    public override void Exit() {

        base.Exit();
    }

}
