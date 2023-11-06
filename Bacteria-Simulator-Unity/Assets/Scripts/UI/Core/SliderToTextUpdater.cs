using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderToTextUpdater : MonoBehaviour
{

    public Slider sliderNumberSlider;
    public TextMeshProUGUI sliderNumberText;

    public string prefix;
    public string suffix;
    public string precision;

    // Start is called before the first frame update
    void Start()
    {
        // Keep this if we want to persist/remember the last values used

        // Update if slider value is set in Unity Editor
        sliderNumberText.text = prefix + sliderNumberSlider.value.ToString() + suffix;

        sliderNumberSlider.onValueChanged.AddListener(UpdateSliderNumberText);
    }

    public void UpdateSliderNumberText(float value) {
        sliderNumberText.text =  prefix + value.ToString(precision) + suffix;
    }

    public int GetSliderNumber() {
        return (int)sliderNumberSlider.value;
    }
}
