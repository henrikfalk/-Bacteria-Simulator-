using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderRocketUpdater : MonoBehaviour
{

    public Slider sliderFirst;
    public Slider sliderSecond;
    public Slider sliderThird;
    public Slider sliderFourth;

//    public float minimumFirstValue;
    public float minimumSecondValue = 100f;
    public float minimumThirdValue = 100f;
    public float minimumFourthValue = 100f;


    // Start is called before the first frame update
    void Start()
    {

        sliderFirst.onValueChanged.AddListener(UpdateFirstSlider);
        sliderSecond.onValueChanged.AddListener(UpdateSecondSlider);
        sliderThird.onValueChanged.AddListener(UpdateThirdSlider);
        sliderFourth.onValueChanged.AddListener(UpdateFourthSlider);
    }
    
    public void UpdateFirstSlider(float _value) {

        if ((sliderSecond.value-_value) < minimumSecondValue) {
            sliderSecond.value = minimumSecondValue + _value;
        }
    }

    public void UpdateSecondSlider(float _value) {

        if ((sliderThird.value-_value) < minimumThirdValue) {
            sliderThird.value = minimumThirdValue + _value;
        }

        if ((sliderSecond.value-sliderFirst.value) < minimumSecondValue) {
            sliderFirst.value = _value - minimumSecondValue;
        }

    }

    public void UpdateThirdSlider(float _value) {

        if ((sliderFourth.value-_value) < minimumFourthValue) {
            sliderFourth.value = minimumFourthValue + _value;
        }

        if ((sliderThird.value-sliderSecond.value) < minimumThirdValue) {
            sliderSecond.value = _value - minimumThirdValue;
        }
    }

    public void UpdateFourthSlider(float _value) {

        if ((sliderFourth.value-sliderThird.value) < minimumFourthValue) {
            sliderThird.value = _value - minimumFourthValue;
        }
    }

}
