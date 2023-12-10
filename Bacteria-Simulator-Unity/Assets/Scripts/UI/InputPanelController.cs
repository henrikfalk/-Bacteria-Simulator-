using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputPanelController : MonoBehaviour
{

    // Initialize
    private LaboratorySceneManager sceneManager;
    private string title;
    private InputPanelController.TYPE type;

    // UI elements
    public TextMeshProUGUI inputPanelTitleText;
    public TMP_InputField inputPanelInputField;
    public Button okButton;

    public enum TYPE {
        SAVEAS,
        RENAME
    };


    // Start is called before the first frame update
    void Start()
    {

        okButton.interactable = false;
        inputPanelInputField.onValueChanged.AddListener(ConfigurationInputFieldChanged);
       
    }

    public void Initialize(LaboratorySceneManager _sceneManager, string _title, InputPanelController.TYPE _type) {

        sceneManager = _sceneManager;
        title = _title;
        type = _type;

        inputPanelTitleText.text = title;
        inputPanelInputField.text = "";
    }

    private void ConfigurationInputFieldChanged(string value) {


        if (IsValidName(value) == false) {

            okButton.interactable = false;
        } else {
            okButton.interactable = true;
        }
    }

    public void InputAction(){

        if (type == TYPE.SAVEAS) {

            SaveAction();
        }

        if (type == TYPE.RENAME) {
            RenameAction();
        }
    }

    public void SaveAction() {

        string newName = inputPanelInputField.text;
        sceneManager.SaveAsSimulationConfiguration(newName.Trim());

    }

    public void RenameAction() {

        string newName = inputPanelInputField.text;
        sceneManager.RenameSimulationConfiguration(newName.Trim());

    }

    private Boolean IsValidName(String name) {

        // If no input
        if (inputPanelInputField.text.Length == 0) {
            return false;
        }


        // If the name is the same af one of the saved configurations, then return false
        List<SimulationConfiguration> simulationConfigurationsList = sceneManager.simulationConfigurationsList;

        Boolean nameExists = false;
        foreach (SimulationConfiguration simulationConfiguration in simulationConfigurationsList) {

            if (simulationConfiguration.simulationName == name.Trim()) {
                nameExists = true;
            }
        }

        if (nameExists == true) {
            return false;
        }

        return true;
    }
}
