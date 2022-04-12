using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    public float middleTemperature;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetEnvironmentTemperature (Vector3 position) {

        // Temperature must be between middleTemperature +/- 3 degrees celcius
        // If position.y = 0 then temperature around middleTemperature
        // 3 degrees / 4.5 units high  = 0.666 degrees per unit

        // Make a Random temperature around the calculated temperature
        float app = Random.Range(1f,1.02f);
        float temperature = middleTemperature + (position.y * app);
//        Debug.Log("temperature = " + temperature);
        return temperature;
    }

    public float GetMiddleTemperature() {
        return middleTemperature;
    }
}
