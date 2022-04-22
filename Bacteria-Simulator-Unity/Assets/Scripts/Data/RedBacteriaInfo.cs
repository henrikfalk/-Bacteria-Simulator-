using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBacteriaInfo : MonoBehaviour
{

    public float maxVelocity;

    // Most optimal temperature for this bacteria
    public float temperatureOptimal;

    // The temperature range +/- that the bacteria can live within
    public float temperatureRange;

    // The date and time this bacteria was born
    public string bornTime;

    // The max age of the bacteria in minutes
    public int maxAgeMinutes;

    // The date and time this bacteria dies of age
    public string deadTime;

    public float fertilityPercentBacteria;

    // How much energy we have
    public float energiBacteria;

    // How much energy we have
    public float healthBacteria;

    // velocity
    Vector3 velocityBacteria;

    // Position
    Vector3 positionBacteria;

    // Rotation
    Vector3 rotationBacteria;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
