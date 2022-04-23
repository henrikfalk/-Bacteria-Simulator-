using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    // the middletemperature of the water
    public float middleTemperature;

    // The toxicity of the environment    
    private int toxicity;
    
    // Current laboratory settings - Use standard is we are running FishtankScene directly
    LaboratoryInfo currentLaboratoryInfo = new LaboratoryInfo {
        middleTemperatureInfo = 30,
        toxicityInfo = 0,
        maxVelocityGreen = 1,
        temperatureOptimalBacteriaGreen = 20,
        temperatureRangeBacteriaGreen = 11,
        maxAgeMinutesBacteriaGreen = 2,
        fertilityPercentBacteriaGreen = 75,
        maxVelocityRed = 2,
        temperatureOptimalBacteriaRed = 40,
        temperatureRangeBacteriaRed = 10,
        maxAgeMinutesBacteriaRed = 3,
        fertilityPercentBacteriaRed = 50

    };

    void Start()
    {
        if (GameManager.Instance != null) {
            currentLaboratoryInfo = GameManager.Instance.getCurrentLaboratoryInfo();
        }

        middleTemperature = currentLaboratoryInfo.middleTemperatureInfo;
        toxicity = currentLaboratoryInfo.toxicityInfo;
    }

    public float GetEnvironmentTemperature (Vector3 position) {

        // If position.y = 0 then temperature around middleTemperature
        // Bacteria thrive between 5 - 55 degrees celcius

        // Make a Random temperature around the calculated temperature
        float app = Random.Range(1f,1.02f);
        float temperature = middleTemperature + (position.y * 5 * app); // The tank is 5 units around the middletemperature

        return temperature;
    }

    public float GetMiddleTemperature() {
        return middleTemperature;
    }

    public int GetToxicity() {
        return toxicity;
    }
}
