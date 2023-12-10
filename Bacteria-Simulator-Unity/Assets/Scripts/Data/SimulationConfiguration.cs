using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimulationConfiguration : ICloneable
{

    public string simulationName;

    public string simulationDescription;

    /*
    * Environment
    */

    // the middletemperature of the water optimal 25 degrees +/- maximum 20
    public int middleTemperatureInfo;

    // The toxicity of the environment (0 - 1000)
    public int toxicityInfo; // initial toxicity of a simulation
    public int yellowWarningToxicityInfo;
    public int redWarningToxicityInfo;
    public int maxLimitToxicityInfo;

    /*
    * Green bacteria
    */

    public string maxVelocityGreen;

    // Most optimal temperature in degrees for this bacteria -
    public int temperatureOptimalBacteriaGreen;

    // The temperature range +/- in degrees that the bacteria can live within
    public int temperatureRangeBacteriaGreen;

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaGreen;

    // The fertility percent
    public int fertilityPercentBacteriaGreen;

    /*
    * Red bacteria
    */

    public string maxVelocityRed;

    // Most optimal temperature for this bacteria
    public int temperatureOptimalBacteriaRed;

    // The temperature range +/- that the bacteria can live within
    public int temperatureRangeBacteriaRed;

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaRed;

    // The fertility percent
    public int fertilityPercentBacteriaRed;

    /*
    * Purple bacteria
    */

    public string maxVelocityPurple;

    // Most optimal temperature for this bacteria
    public int temperatureOptimalBacteriaPurple;

    // The temperature range +/- that the bacteria can live within
    public int temperatureRangeBacteriaPurple;

    // The max age of the bacteria in minutes
    public int maxAgeMinutesBacteriaPurple;

    // The fertility percent
    public int fertilityPercentBacteriaPurple;

    // Implement ICloneable interface
    public System.Object Clone() {
        return this.MemberwiseClone();
    }

    // Override Equals() method
    public override bool Equals(object target) {
        if (!(target is SimulationConfiguration)) {
            return false;
        }

        var other = target as SimulationConfiguration;
 
        if (simulationName != other.simulationName) {
//            Debug.Log("1");
            return false;
        }
        if (simulationDescription != other.simulationDescription) {
//            Debug.Log("2");
            return false;
        }
        if (middleTemperatureInfo != other.middleTemperatureInfo) {
//            Debug.Log("3");
            return false;
        }

        if (toxicityInfo != other.toxicityInfo) {
//            Debug.Log("4");
            return false;
        }
        if (yellowWarningToxicityInfo != other.yellowWarningToxicityInfo) {
//            Debug.Log("5");
            return false;
        }
        if (redWarningToxicityInfo != other.redWarningToxicityInfo) {
//            Debug.Log("6");
            return false;
        }
        if (maxLimitToxicityInfo != other.maxLimitToxicityInfo) {
//            Debug.Log("7");
            return false;
        }

        if (maxVelocityGreen != other.maxVelocityGreen) {
//            Debug.Log("8");
            return false;
        }
        if (temperatureOptimalBacteriaGreen != other.temperatureOptimalBacteriaGreen) {
//            Debug.Log("9");
            return false;
        }
        if (temperatureRangeBacteriaGreen != other.temperatureRangeBacteriaGreen) {
//            Debug.Log("10");
            return false;
        }
        if (maxAgeMinutesBacteriaGreen != other.maxAgeMinutesBacteriaGreen) {
//            Debug.Log("11");
            return false;
        }
        if (fertilityPercentBacteriaGreen != other.fertilityPercentBacteriaGreen) {
//            Debug.Log("12");
            return false;
        }


        if (maxVelocityRed != other.maxVelocityRed) {
//            Debug.Log("13");
            return false;
        }
        if (temperatureOptimalBacteriaRed != other.temperatureOptimalBacteriaRed) {
//            Debug.Log("14");
            return false;
        }
        if (temperatureRangeBacteriaRed != other.temperatureRangeBacteriaRed) {
//            Debug.Log("15");
            return false;
        }
        if (maxAgeMinutesBacteriaRed != other.maxAgeMinutesBacteriaRed) {
//            Debug.Log("16");
            return false;
        }
        if (fertilityPercentBacteriaRed != other.fertilityPercentBacteriaRed) {
//            Debug.Log("17");
            return false;
        }

        if (maxVelocityPurple != other.maxVelocityPurple) {
//            Debug.Log("18");
            return false;
        }
        if (temperatureOptimalBacteriaPurple != other.temperatureOptimalBacteriaPurple) {
//            Debug.Log("19");
            return false;
        }
        if (temperatureRangeBacteriaPurple != other.temperatureRangeBacteriaPurple) {
//            Debug.Log("20");
            return false;
        }
        if (maxAgeMinutesBacteriaPurple != other.maxAgeMinutesBacteriaPurple) {
//            Debug.Log("21");
            return false;
        }
        if (fertilityPercentBacteriaPurple != other.fertilityPercentBacteriaPurple) {
//            Debug.Log("22");
            return false;
        }

        return true;
    }

    public static bool operator ==(SimulationConfiguration source, SimulationConfiguration target) { 
        return source.Equals(target);
    }
 
    public static bool operator !=(SimulationConfiguration source, SimulationConfiguration target) {
        return !(source == target);
    }

    public override int GetHashCode() {
    
        unchecked {
            int hash = 17;

            hash = hash * 23 + simulationName.GetHashCode();
            hash = hash * 23 + simulationDescription.GetHashCode();
            hash = hash * 23 + middleTemperatureInfo.GetHashCode();


            hash = hash * 23 + toxicityInfo.GetHashCode();
            hash = hash * 23 + yellowWarningToxicityInfo.GetHashCode();
            hash = hash * 23 + redWarningToxicityInfo.GetHashCode();
            hash = hash * 23 + maxLimitToxicityInfo.GetHashCode();

            hash = hash * 23 + maxVelocityGreen.GetHashCode();
            hash = hash * 23 + temperatureOptimalBacteriaGreen.GetHashCode();
            hash = hash * 23 + temperatureRangeBacteriaGreen.GetHashCode();
            hash = hash * 23 + maxAgeMinutesBacteriaGreen.GetHashCode();
            hash = hash * 23 + fertilityPercentBacteriaGreen.GetHashCode();

            hash = hash * 23 + maxVelocityRed.GetHashCode();
            hash = hash * 23 + temperatureOptimalBacteriaRed.GetHashCode();
            hash = hash * 23 + temperatureRangeBacteriaRed.GetHashCode();
            hash = hash * 23 + maxAgeMinutesBacteriaRed.GetHashCode();
            hash = hash * 23 + fertilityPercentBacteriaRed.GetHashCode();

            hash = hash * 23 + maxVelocityPurple.GetHashCode();
            hash = hash * 23 + temperatureOptimalBacteriaPurple.GetHashCode();
            hash = hash * 23 + temperatureRangeBacteriaPurple.GetHashCode();
            hash = hash * 23 + maxAgeMinutesBacteriaPurple.GetHashCode();
            hash = hash * 23 + fertilityPercentBacteriaPurple.GetHashCode();

            return hash;
        }
    }
}
