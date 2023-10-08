using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class LaboratoryController : MonoBehaviour
{

    // middleTemperatureInfo
    public Slider envMiddleTempSlider;

    // toxicityInfo
    public Slider envToxicitySlider;

    /**
    *   Green bacteria
    **/
    // maxVelocityGreen
    public Slider greenSpeedSlider;

    // temperatureOptimalBacteriaGreen
    public Slider greenOptimalTempSlider;

    // temperatureRangeBacteriaGreen
    public Slider greenRangeTempSlider;

    // maxAgeMinutesBacteriaGreen
    public Slider greenMaxAgeSlider;

    // fertilityPercentBacteriaGreen
    public Slider greenFertilitySlider;

    /**
    *   Red bacteria
    **/
    // maxVelocityRed
    public Slider redSpeedSlider;

    // temperatureOptimalBacteriaRed
    public Slider redOptimalTempSlider;

    // temperatureRangeBacteriaRed
    public Slider redRangeTempSlider;

    // maxAgeMinutesBacteriaRed
    public Slider redMaxAgeSlider;

    // fertilityPercentBacteriaRed
    public Slider redFertilitySlider;

    // Standard laboratory settings - Only used if we run this scene directly in the editor
    private LaboratoryInfo defaultLaboratoryInfo = new LaboratoryInfo {
        middleTemperatureInfo = 30,                 // Between 20 and 40 degrees celcius. 30 is default middletemperature
        toxicityInfo = 0,                           // Between 0 and 80 %. Default is 0 %
        maxVelocityGreen = 1,                       // Between 0.25f and 5.00f. Default is 1.0f
        temperatureOptimalBacteriaGreen = 20,       // Between 10 and 50 degrees celcius. 20 is default
        temperatureRangeBacteriaGreen = 11,         // Between 1 and 14. Default is 11
        maxAgeMinutesBacteriaGreen = 2,             // Between 1 an 10 minutes. Default is 2
        fertilityPercentBacteriaGreen = 75,         // Between 20 and 90 %. Default is 75
        maxVelocityRed = 2,                         // Between 0.25f and 5.00f. Default is 2.0f
        temperatureOptimalBacteriaRed = 40,         // Between 10 and 50 degrees celcius. 40 is default
        temperatureRangeBacteriaRed = 10,           // Between 1 and 14. Default is 10
        maxAgeMinutesBacteriaRed = 3,               // Between 1 an 10 minutes. Default is 3
        fertilityPercentBacteriaRed = 50            // Between 20 and 90 %. Default is 50
    };

    private LaboratoryInfo currentLaboratoryInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null) {
            currentLaboratoryInfo = GameManager.Instance.GetCurrentLaboratoryInfo();
            defaultLaboratoryInfo = GameManager.Instance.GetDefaultLaboratoryInfo();
        } else {
            // We run from "LaboratoryScene"
            currentLaboratoryInfo = (LaboratoryInfo)defaultLaboratoryInfo.Clone();
        }

        // Update UI
        UpdateUI();
    }

    public void QuitLaboratory() {

            if (GameManager.Instance != null) {
                GameManager.Instance.GotoScene("MenuScene");
            } else {
                #if UNITY_EDITOR
                    EditorApplication.ExitPlaymode();
                #endif
            }
    }

    public void SaveLaboratory() {
        if (GameManager.Instance != null) {

            // middleTemperatureInfo
            currentLaboratoryInfo.middleTemperatureInfo = envMiddleTempSlider.value;

            // toxicityInfo
            currentLaboratoryInfo.toxicityInfo = (int)envToxicitySlider.value;

            /**
            *   Green bacteria
            **/
            // maxVelocityGreen
            currentLaboratoryInfo.maxVelocityGreen = greenSpeedSlider.value;

            // temperatureOptimalBacteriaGreen
            currentLaboratoryInfo.temperatureOptimalBacteriaGreen = greenOptimalTempSlider.value;

            // temperatureRangeBacteriaGreen
            currentLaboratoryInfo.temperatureRangeBacteriaGreen = greenRangeTempSlider.value;

            // maxAgeMinutesBacteriaGreen
            currentLaboratoryInfo.maxAgeMinutesBacteriaGreen = (int)greenMaxAgeSlider.value;

            // fertilityPercentBacteriaGreen
            currentLaboratoryInfo.fertilityPercentBacteriaGreen = greenFertilitySlider.value;

            /**
            *   Red bacteria
            **/
            // maxVelocityRed
            currentLaboratoryInfo.maxVelocityRed = redSpeedSlider.value;

            // temperatureOptimalBacteriaRed
            currentLaboratoryInfo.temperatureOptimalBacteriaRed = redOptimalTempSlider.value;

            // temperatureRangeBacteriaRed
            currentLaboratoryInfo.temperatureRangeBacteriaRed = redRangeTempSlider.value;

            // maxAgeMinutesBacteriaRed
            currentLaboratoryInfo.maxAgeMinutesBacteriaRed = (int)redMaxAgeSlider.value;

            // fertilityPercentBacteriaRed
            currentLaboratoryInfo.fertilityPercentBacteriaRed = redFertilitySlider.value;

            GameManager.Instance.SetCurrentLaboratoryInfo(currentLaboratoryInfo);
        }
    }

    public void RestoreDefaultSettings() {

        // Clone defaults
        currentLaboratoryInfo = (LaboratoryInfo)defaultLaboratoryInfo.Clone();

        // Save default values
        GameManager.Instance.SetCurrentLaboratoryInfo(currentLaboratoryInfo);

        // Update
        UpdateUI();
    }

    private void UpdateUI() {

        // middleTemperatureInfo
        envMiddleTempSlider.value = (float)Math.Round(currentLaboratoryInfo.middleTemperatureInfo,2);

        // toxicityInfo
        envToxicitySlider.value = currentLaboratoryInfo.toxicityInfo;

        /**
        *   Green bacteria
        **/
        // maxVelocityGreen
        greenSpeedSlider.value = (float)Math.Round(currentLaboratoryInfo.maxVelocityGreen,2);

        // temperatureOptimalBacteriaGreen
        greenOptimalTempSlider.value = (float)Math.Round(currentLaboratoryInfo.temperatureOptimalBacteriaGreen,2);

        // temperatureRangeBacteriaGreen
        greenRangeTempSlider.value = (float)Math.Round(currentLaboratoryInfo.temperatureRangeBacteriaGreen,2);

        // maxAgeMinutesBacteriaGreen
        greenMaxAgeSlider.value = currentLaboratoryInfo.maxAgeMinutesBacteriaGreen;

        // fertilityPercentBacteriaGreen
        greenFertilitySlider.value = (float)Math.Round(currentLaboratoryInfo.fertilityPercentBacteriaGreen,2);

        /**
        *   Red bacteria
        **/
        // maxVelocityRed
        redSpeedSlider.value = (float)Math.Round(currentLaboratoryInfo.maxVelocityRed,2);

        // temperatureOptimalBacteriaRed
        redOptimalTempSlider.value = (float)Math.Round(currentLaboratoryInfo.temperatureOptimalBacteriaRed,2);

        // temperatureRangeBacteriaRed
        redRangeTempSlider.value = (float)Math.Round(currentLaboratoryInfo.temperatureRangeBacteriaRed,2);

        // maxAgeMinutesBacteriaRed
        redMaxAgeSlider.value = currentLaboratoryInfo.maxAgeMinutesBacteriaRed;

        // fertilityPercentBacteriaRed
        redFertilitySlider.value = (float)Math.Round(currentLaboratoryInfo.fertilityPercentBacteriaRed,2);

    }
}
