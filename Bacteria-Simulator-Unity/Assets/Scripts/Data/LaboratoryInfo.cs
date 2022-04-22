using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryInfo : MonoBehaviour
{

    // the middletemperature of the water
    public float middleTemperatureInfo;

    // The toxicity of the environment    
    private int toxicityInfo;

    /*
    * Green bacteria
    */

    public float maxVelocityGreen;

    // Most optimal temperature for this bacteria
    public float temperatureOptimalBacteriaGreen;

    // The temperature range +/- that the bacteria can live within
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
