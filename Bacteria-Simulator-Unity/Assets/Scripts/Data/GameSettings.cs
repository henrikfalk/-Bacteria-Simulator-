using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettings : ICloneable
{

    /*
    * Current simulation configuration name
    */

    public string currentSimulationConfigurationName;

    // Implement ICloneable interface
    public System.Object Clone() {
        return this.MemberwiseClone();
    }

}
