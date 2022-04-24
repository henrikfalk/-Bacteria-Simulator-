using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaboratoryInfo : ICloneable
{

    /*
    * Environment
    */

    // the middletemperature of the water optimal 25 degrees +/- maximum 20
    public float middleTemperatureInfo { get; set; }

    // The toxicity of the environment (0 - 100)
    public int toxicityInfo { get; set; }

    /*
    * Green bacteria
    */

    public float maxVelocityGreen { get; set; }

    // Most optimal temperature in degrees for this bacteria -
    public float temperatureOptimalBacteriaGreen { get; set; }

    // The temperature range +/- in degrees that the bacteria can live within
    public float temperatureRangeBacteriaGreen { get; set; }

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaGreen { get; set; }

    // The fertility percent
    public float fertilityPercentBacteriaGreen { get; set; }

    /*
    * Red bacteria
    */

    public float maxVelocityRed { get; set; }

    // Most optimal temperature for this bacteria
    public float temperatureOptimalBacteriaRed { get; set; }

    // The temperature range +/- that the bacteria can live within
    public float temperatureRangeBacteriaRed { get; set; }

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaRed { get; set; }

    // The fertility percent
    public float fertilityPercentBacteriaRed { get; set; }

    // Implement ICloneable interface
    public System.Object Clone() {
        return this.MemberwiseClone();
    }


}
