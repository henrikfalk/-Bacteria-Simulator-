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

    // The toxicity of the environment (0 - 1000)
    public int toxicityInfo; // initial toxicity of a simulation
    public int yellowWarningToxicityInfo;
    public int redWarningToxicityInfo;
    public int maxLimitToxicityInfo;

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

    /*
    * Purple bacteria
    */

    public float maxVelocityPurple;

    // Most optimal temperature for this bacteria
    public float temperatureOptimalBacteriaPurple;

    // The temperature range +/- that the bacteria can live within
    public float temperatureRangeBacteriaPurple;

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaPurple;

    // The fertility percent
    public float fertilityPercentBacteriaPurple;

    // Implement ICloneable interface
    public System.Object Clone() {
        return this.MemberwiseClone();
    }


}
