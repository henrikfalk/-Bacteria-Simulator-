using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RangeSliderUpdater : MonoBehaviour
{

    public Slider sliderNumberSliderMaster;
    public Slider sliderNumberSlider;

    public TextMeshProUGUI sliderNumberText1;
    public TextMeshProUGUI sliderNumberText2;

    public string prefix;
    public string suffix;
    public string precision;

    // Start is called before the first frame update
    void Start()
    {
        // Update if slider value is set in Unity Editor
//        sliderNumberText.text = prefix + sliderNumberSlider.value.ToString() + suffix;

        sliderNumberSlider.onValueChanged.AddListener(UpdateSliderNumberText);
        sliderNumberSliderMaster.onValueChanged.AddListener(UpdateSliderMasterNumberText);

        UpdateSliderNumberText(sliderNumberSlider.value);
    }

    public void UpdateSliderNumberText(float value) {

        float masterValue = sliderNumberSliderMaster.value;

        sliderNumberText1.text =  prefix + (masterValue - value).ToString(precision) + suffix;
        sliderNumberText2.text =  prefix + (masterValue + value).ToString(precision) + suffix;
    }

    public void UpdateSliderMasterNumberText(float masterValue) {

        float value = sliderNumberSlider.value;

        sliderNumberText1.text =  prefix + (masterValue - value).ToString(precision) + suffix;
        sliderNumberText2.text =  prefix + (masterValue + value).ToString(precision) + suffix;
    }

    public int GetSliderNumber() {
        return (int)sliderNumberSlider.value;
    }

}
