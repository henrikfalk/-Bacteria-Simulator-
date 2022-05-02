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
    public float middleTemperatureInfo;

    // The toxicity of the environment (0 - 100)
    public int toxicityInfo;

    /*
    * Green bacteria
    */

    public float maxVelocityGreen;

    // Most optimal temperature in degrees for this bacteria -
    public float temperatureOptimalBacteriaGreen;

    // The temperature range +/- in degrees that the bacteria can live within
    public float temperatureRangeBacteriaGreen;

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaGreen;

    // The fertility percent
    public float fertilityPercentBacteriaGreen;

    /*
    * Red bacteria
    */

    public float maxVelocityRed;

    // Most optimal temperature for this bacteria
    public float temperatureOptimalBacteriaRed;

    // The temperature range +/- that the bacteria can live within
    public float temperatureRangeBacteriaRed;

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaRed;

    // The fertility percent
    public float fertilityPercentBacteriaRed;

    // Implement ICloneable interface
    public System.Object Clone() {
        return this.MemberwiseClone();
    }


}
