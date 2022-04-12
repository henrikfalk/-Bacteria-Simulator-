using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwoSliderPopupManager : MonoBehaviour
{

    public TextMeshProUGUI slider1NumberText;
    public Slider slider1NumberSlider;

    public TextMeshProUGUI slider2NumberText;
    public Slider slider2NumberSlider;

    // Start is called before the first frame update
    void Start() {
        // Keep this if we want to persist/remember the last values used

        // Update if sliders is set in Unity Editor
        slider1NumberText.text = slider1NumberSlider.value.ToString();
        slider2NumberText.text = slider2NumberSlider.value.ToString();
    }

    void Update() {
        // If we press the 'n' key then show "NewSimulationPopup"
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
    }

    public void UpdateSlider1NumberText() {
        slider1NumberText.text = slider1NumberSlider.value.ToString();
    }

    public void UpdateSlider2NumberText() {
        slider2NumberText.text = slider2NumberSlider.value.ToString();
    }

    public int GetSlider1Number() {
        return (int)slider1NumberSlider.value;
    }

    public int GetSlider2Number() {
        return (int)slider2NumberSlider.value;
    }

}
