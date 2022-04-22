using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderToTextUpdater : MonoBehaviour
{

    public TextMeshProUGUI sliderNumberText;
    public Slider sliderNumberSlider;

    // Start is called before the first frame update
    void Start()
    {
        // Keep this if we want to persist/remember the last values used

        // Update if slider value is set in Unity Editor
        sliderNumberText.text = sliderNumberSlider.value.ToString();

        sliderNumberSlider.onValueChanged.AddListener(UpdateSliderNumberText);
    }

    public void UpdateSliderNumberText(float value) {
        sliderNumberText.text = value.ToString();
    }

    public int GetSliderNumber() {
        return (int)sliderNumberSlider.value;
    }
}
