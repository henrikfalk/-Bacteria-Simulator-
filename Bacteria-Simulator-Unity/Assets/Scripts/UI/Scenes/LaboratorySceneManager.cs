using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LaboratorySceneManager : MonoBehaviour
{

    /********************
        Current configuration panel
    ********************/
    public List<SimulationConfiguration> simulationConfigurationsList = new List<SimulationConfiguration>();

    public TextMeshProUGUI currentConfigurationsText;
    String currentConfigurationsTextString;

    public TMP_InputField configurationInputField;
    public Button saveButton;
    public Button saveAsButton;

    /********************
        Saved configurations panel
    ********************/

    List<GameObject> buttonConfigurationList = new List<GameObject>();

    public GameObject configurationScrollView;
//    private ScrollRect configurationScrollRect;
    public GameObject configurationsListboxContent;
    public Transform configurationsListboxContentTransform;
    public GameObject configurationItemButtonPrefab;

    public TMP_InputField configurationDescriptionTextField;
//    private TextMeshProUGUI configurationDescriptionTextFieldText;

    string selectedConfigurationName = "";

    public Button loadButton;
    public Button deleteButton;

    /********************
        Environment panel
    ********************/

    // middleTemperatureInfo
    public Slider envMiddleTempSlider;

    // toxicityInfo
    public Slider initialToxicitySlider;
    public Slider yellowToxicitySlider;
    public Slider redToxicitySlider;
    public Slider deadlyLimitToxicitySlider;

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

    /**
    *   Purple bacteria
    **/
    // maxVelocityRed
    public Slider purpleSpeedSlider;

    // temperatureOptimalBacteriaRed
    public Slider purpleOptimalTempSlider;

    // temperatureRangeBacteriaRed
    public Slider purpleRangeTempSlider;

    // maxAgeMinutesBacteriaRed
    public Slider purpleMaxAgeSlider;

    // fertilityPercentBacteriaRed
    public Slider purpleFertilitySlider;

    /********************
        InputPanel
    ********************/
    public GameObject inputPanel;
    private InputPanelController inputPanelController;

    /********************
        OLD savesystem
    ********************/
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

    /********************
        Configurations
    ********************/
   // Default simulation configuration if runing scene inside Unity
    private SimulationConfiguration defaultSimulationConfiguration = new SimulationConfiguration {
        simulationName = "Default simulation",
        simulationDescription = "This is the default simulation.",
        middleTemperatureInfo = 30,
        toxicityInfo = 0,
        yellowWarningToxicityInfo = 500,
        redWarningToxicityInfo = 1500,
        maxLimitToxicityInfo = 2000,
        maxVelocityGreen = "1,00",
        temperatureOptimalBacteriaGreen = 20,
        temperatureRangeBacteriaGreen = 11,
        maxAgeMinutesBacteriaGreen = 2,
        fertilityPercentBacteriaGreen = 75,
        maxVelocityRed = "2,00",
        temperatureOptimalBacteriaRed = 40,
        temperatureRangeBacteriaRed = 10,
        maxAgeMinutesBacteriaRed = 3,
        fertilityPercentBacteriaRed = 50,
        maxVelocityPurple = "3,00",
        temperatureOptimalBacteriaPurple = 40,
        temperatureRangeBacteriaPurple = 10,
        maxAgeMinutesBacteriaPurple = 3,
        fertilityPercentBacteriaPurple = 50
    };
    // Current simulation configuration
    private SimulationConfiguration currentSimulationConfiguration;

    // Is data dirty
    private SimulationConfiguration originalCurrentSimulationConfiguration;
    public GameObject saveDirtyPanel;

    // Start is called before the first frame update
    void Start()
    {

        // OLD Stuff
        if (GameManager.Instance != null) {
            currentLaboratoryInfo = GameManager.Instance.GetCurrentLaboratoryInfo();
            defaultLaboratoryInfo = GameManager.Instance.GetDefaultLaboratoryInfo();
        } else {
            // We run from "LaboratoryScene"
            currentLaboratoryInfo = (LaboratoryInfo)defaultLaboratoryInfo.Clone();
        }

        // ************************ New stuff
        if (GameManager.Instance != null) {
            currentSimulationConfiguration = GameManager.Instance.GetCurrentSimulationConfiguration();
            defaultSimulationConfiguration = GameManager.Instance.GetDefaultSimulationConfiguration();
        } else {
            // We run from "LaboratoryScene"
            currentSimulationConfiguration = (SimulationConfiguration)defaultSimulationConfiguration.Clone();
        }

        if (GameManager.Instance != null) {
            simulationConfigurationsList = GameManager.Instance.GetSimulationConfigurations();
            UpdateConfigurationListUI();
        }

        originalCurrentSimulationConfiguration = (SimulationConfiguration)currentSimulationConfiguration.Clone();

        if (originalCurrentSimulationConfiguration != currentSimulationConfiguration) {
            Debug.Log("originalCurrentSimulationConfiguration != currentSimulationConfiguration");
        }

        currentConfigurationsTextString = currentConfigurationsText.text;

        loadButton.interactable = false;
        deleteButton.interactable = false;

        // Setup InputPanel for SaveAs and Rename functions
        inputPanelController = inputPanel.GetComponent<InputPanelController>();
        inputPanel.SetActive(false);

        // Hide save dirty data confirmation dialog
        saveDirtyPanel.SetActive(false);

        // Update UI
        UpdateUI();
    

    }

    public void QuitLaboratory() {

            if (GameManager.Instance != null) {

                UpdateCurrentSimulationConfigurationFromUI();
                if (IsDataDirty() == true) {
                    saveDirtyPanel.SetActive(true);
                } else {
                    GameManager.Instance.GotoPreviousScene();
                }
            } else {
                #if UNITY_EDITOR
                    EditorApplication.ExitPlaymode();
                #endif
            }
    }

    public void QuitAndSaveLaboratory(Boolean _save) {

        saveDirtyPanel.SetActive(false);

        if (_save == true) {
            SaveCurrentSimulationConfiguration();
        }

        GameManager.Instance.GotoPreviousScene();
    }

    public void SaveAsSimulationConfigurationAction() {

        inputPanelController.Initialize(this, "Save as", InputPanelController.TYPE.SAVEAS);
        inputPanel.SetActive(true);
        
    }

    public void RenameSimulationConfigurationAction() {

        inputPanelController.Initialize(this, "Rename as", InputPanelController.TYPE.RENAME);
        inputPanel.SetActive(true);
        
    }

    public void SaveAsSimulationConfiguration(string newSimulationName) {

        if (GameManager.Instance == null) {
            return;
        }

        inputPanel.SetActive(false);
                
        // Update simulation name in currentSimulationConfiguration
        SimulationConfiguration newSimulation = (SimulationConfiguration)currentSimulationConfiguration.Clone();

        // update new configuration with save name and description
        newSimulation.simulationName = newSimulationName;
        newSimulation.simulationDescription = configurationInputField.text;

        // make configuration current
        currentSimulationConfiguration = newSimulation;
        originalCurrentSimulationConfiguration = (SimulationConfiguration)newSimulation.Clone();

        // save
        SaveCurrentSimulationConfiguration();

        simulationConfigurationsList = GameManager.Instance.GetSimulationConfigurations();

        // Update UI
        UpdateUI();

        // update CurrentConfigurationPanel
        UpdateConfigurationListUI();
    }

    public void RenameSimulationConfiguration(string newSimulationName) {

        if (GameManager.Instance == null) {
            return;
        }

        inputPanel.SetActive(false);

        string removeSimulationName = currentSimulationConfiguration.simulationName;
                
        // Update simulation name in currentSimulationConfiguration
        SimulationConfiguration renameSimulation = (SimulationConfiguration)currentSimulationConfiguration.Clone();

        // update new configuration with save name and description
        renameSimulation.simulationName = newSimulationName;
        originalCurrentSimulationConfiguration = (SimulationConfiguration)newSimulationName.Clone();

        // make configuration current
        currentSimulationConfiguration = renameSimulation;

        // save
        SaveCurrentSimulationConfiguration();
        
        GameManager.Instance.DeleteSimulationConfiguration(removeSimulationName);

        simulationConfigurationsList = GameManager.Instance.GetSimulationConfigurations();

        // Update UI
        UpdateUI();

        // update CurrentConfigurationPanel
        UpdateConfigurationListUI();
    }

    public void SaveCurrentSimulationConfiguration() {

        if (GameManager.Instance == null) {
            return;
        }

        UpdateCurrentSimulationConfigurationFromUI();

        GameManager.Instance.SaveSimulationConfiguration(currentSimulationConfiguration);

        originalCurrentSimulationConfiguration = (SimulationConfiguration)currentSimulationConfiguration.Clone();

    }

    private void UpdateCurrentSimulationConfigurationFromUI() {

            // description
            currentSimulationConfiguration.simulationDescription = configurationInputField.text;

            // middleTemperatureInfo
            currentSimulationConfiguration.middleTemperatureInfo = (int)envMiddleTempSlider.value;

            // toxicityInfo
            currentSimulationConfiguration.toxicityInfo = (int)initialToxicitySlider.value;
            currentSimulationConfiguration.yellowWarningToxicityInfo = (int)yellowToxicitySlider.value;
            currentSimulationConfiguration.redWarningToxicityInfo = (int)redToxicitySlider.value;
            currentSimulationConfiguration.maxLimitToxicityInfo = (int)deadlyLimitToxicitySlider.value;

            /**
            *   Green bacteria
            **/
            // maxVelocityGreen
            currentSimulationConfiguration.maxVelocityGreen = greenSpeedSlider.value.ToString("0.00");

            // temperatureOptimalBacteriaGreen
            currentSimulationConfiguration.temperatureOptimalBacteriaGreen = (int)greenOptimalTempSlider.value;

            // temperatureRangeBacteriaGreen
            currentSimulationConfiguration.temperatureRangeBacteriaGreen = (int)greenRangeTempSlider.value;

            // maxAgeMinutesBacteriaGreen
            currentSimulationConfiguration.maxAgeMinutesBacteriaGreen = (int)greenMaxAgeSlider.value;

            // fertilityPercentBacteriaGreen
            currentSimulationConfiguration.fertilityPercentBacteriaGreen = (int)greenFertilitySlider.value;

            /**
            *   Red bacteria
            **/
            // maxVelocityRed
            currentSimulationConfiguration.maxVelocityRed = redSpeedSlider.value.ToString("0.00");

            // temperatureOptimalBacteriaRed
            currentSimulationConfiguration.temperatureOptimalBacteriaRed = (int)redOptimalTempSlider.value;

            // temperatureRangeBacteriaRed
            currentSimulationConfiguration.temperatureRangeBacteriaRed = (int)redRangeTempSlider.value;

            // maxAgeMinutesBacteriaRed
            currentSimulationConfiguration.maxAgeMinutesBacteriaRed = (int)redMaxAgeSlider.value;

            // fertilityPercentBacteriaRed
            currentSimulationConfiguration.fertilityPercentBacteriaRed = (int)redFertilitySlider.value;

            /**
            *   Purple bacteria
            **/
            // maxVelocityRed
            currentSimulationConfiguration.maxVelocityPurple = purpleSpeedSlider.value.ToString("0.00");

            // temperatureOptimalBacteriaRed
            currentSimulationConfiguration.temperatureOptimalBacteriaPurple = (int)purpleOptimalTempSlider.value;

            // temperatureRangeBacteriaRed
            currentSimulationConfiguration.temperatureRangeBacteriaPurple = (int)purpleRangeTempSlider.value;

            // maxAgeMinutesBacteriaRed
            currentSimulationConfiguration.maxAgeMinutesBacteriaPurple = (int)purpleMaxAgeSlider.value;

            // fertilityPercentBacteriaRed
            currentSimulationConfiguration.fertilityPercentBacteriaPurple = (int)purpleFertilitySlider.value;

    }

    public void RestoreDefaultSettings() {

        // Clone defaults
        currentSimulationConfiguration = (SimulationConfiguration)defaultSimulationConfiguration.Clone();
        originalCurrentSimulationConfiguration = (SimulationConfiguration)currentSimulationConfiguration.Clone();

        // Save default values
        GameManager.Instance.SetCurrentSimulationConfiguration(currentSimulationConfiguration);

        // Update
        UpdateUI();
    }

    public void UpdateUI() {

        currentConfigurationsText.text = currentConfigurationsTextString + currentSimulationConfiguration.simulationName;
        configurationInputField.text = currentSimulationConfiguration.simulationDescription;

        // middleTemperatureInfo
        envMiddleTempSlider.value = currentSimulationConfiguration.middleTemperatureInfo;

        // toxicity Info
        initialToxicitySlider.value = currentSimulationConfiguration.toxicityInfo;
        yellowToxicitySlider.value = currentSimulationConfiguration.yellowWarningToxicityInfo;
        redToxicitySlider.value = currentSimulationConfiguration.redWarningToxicityInfo;
        deadlyLimitToxicitySlider.value = currentSimulationConfiguration.maxLimitToxicityInfo;


        /**
        *   Green bacteria
        **/
        // maxVelocityGreen
        greenSpeedSlider.value = float.Parse(currentSimulationConfiguration.maxVelocityGreen);

        // temperatureOptimalBacteriaGreen
        greenOptimalTempSlider.value = currentSimulationConfiguration.temperatureOptimalBacteriaGreen;

        // temperatureRangeBacteriaGreen
        greenRangeTempSlider.value = currentSimulationConfiguration.temperatureRangeBacteriaGreen;

        // maxAgeMinutesBacteriaGreen
        greenMaxAgeSlider.value = currentSimulationConfiguration.maxAgeMinutesBacteriaGreen;

        // fertilityPercentBacteriaGreen
        greenFertilitySlider.value = currentSimulationConfiguration.fertilityPercentBacteriaGreen;

        /**
        *   Red bacteria
        **/

        // maxVelocityRed
        redSpeedSlider.value = float.Parse(currentSimulationConfiguration.maxVelocityRed);

        // temperatureOptimalBacteriaRed
//        redOptimalTempSlider.value = (float)Math.Round(currentSimulationConfiguration.temperatureOptimalBacteriaRed,2);
        redOptimalTempSlider.value = currentSimulationConfiguration.temperatureOptimalBacteriaRed;

        // temperatureRangeBacteriaRed
        redRangeTempSlider.value = currentSimulationConfiguration.temperatureRangeBacteriaRed;

        // maxAgeMinutesBacteriaRed
        redMaxAgeSlider.value = currentSimulationConfiguration.maxAgeMinutesBacteriaRed;

        // fertilityPercentBacteriaRed
        redFertilitySlider.value = currentSimulationConfiguration.fertilityPercentBacteriaRed;

        /**
        *   Purple bacteria
        **/

        // maxVelocityPurple
        purpleSpeedSlider.value = float.Parse(currentSimulationConfiguration.maxVelocityPurple);

        // temperatureOptimalBacteriaPurple
        purpleOptimalTempSlider.value = currentSimulationConfiguration.temperatureOptimalBacteriaPurple;

        // temperatureRangeBacteriaPurple
        purpleRangeTempSlider.value = currentSimulationConfiguration.temperatureRangeBacteriaPurple;

        // maxAgeMinutesBacteriaPurple
        purpleMaxAgeSlider.value = currentSimulationConfiguration.maxAgeMinutesBacteriaPurple;

        // fertilityPercentBacteriaPurple
        purpleFertilitySlider.value = currentSimulationConfiguration.fertilityPercentBacteriaPurple;

        if (currentSimulationConfiguration.simulationName == defaultSimulationConfiguration.simulationName) {
            saveButton.interactable = false;
        } else {
            saveButton.interactable = true;
        }

    }

    private void UpdateConfigurationListUI() {
        SimulationConfiguration defaultSimulationConfiguration = new SimulationConfiguration();
        if (GameManager.Instance != null) {

            defaultSimulationConfiguration = GameManager.Instance.GetDefaultSimulationConfiguration();
        }

        // Empty buttonConfigurationList
        foreach (GameObject gameObject in buttonConfigurationList) {

            Button button = gameObject.GetComponent<Button>();
            string _name = button.GetComponentInChildren<TextMeshProUGUI>().text;
            Destroy(gameObject);
        }
        buttonConfigurationList.Clear();

        GameObject _obj;
        Button _button;
        foreach (SimulationConfiguration _configuration in simulationConfigurationsList) {

            _obj = Instantiate(configurationItemButtonPrefab);
            _button = _obj.GetComponent<Button>();
            
            ButtonSelectionHandler handler = _button.GetComponent<ButtonSelectionHandler>();
            handler.laboratorySceneManager = this;

            TextMeshProUGUI _buttonText = _obj.GetComponentInChildren<TextMeshProUGUI>();
            _buttonText.text = _configuration.simulationName;
            _obj.transform.localScale = Vector2.one;

            _obj.transform.SetParent(configurationsListboxContent.transform, false);

            buttonConfigurationList.Add(_obj);
            if (defaultSimulationConfiguration.simulationName == _configuration.simulationName){

                ColorBlock colorBlock = _button.colors;
                colorBlock.normalColor = new Color(128,128,128);
                _button.colors = colorBlock;

            }
        }

    }

    public void SimulationSelected(GameObject _gameObject) {

        selectedConfigurationName = _gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        foreach (SimulationConfiguration _configuration in simulationConfigurationsList) {

            if (_configuration.simulationName == selectedConfigurationName){

                configurationDescriptionTextField.text = _configuration.simulationDescription;
            }
        }

        loadButton.interactable = true;

        if (selectedConfigurationName == defaultSimulationConfiguration.simulationName) {
            deleteButton.interactable = false;
        } else {
            deleteButton.interactable = true;
        }
    }

    // Not used
    public void SimulationDeselected(GameObject _gameObject) {

        selectedConfigurationName = "";

        configurationDescriptionTextField.text = "";

        loadButton.interactable = false;
        deleteButton.interactable = false;
       
    }

    public void LoadSimulationConfiguration() {

        if (GameManager.Instance == null) {
            return;
        }

        loadButton.interactable = false;
        deleteButton.interactable = false;

        if (selectedConfigurationName != "") {


            SimulationConfiguration _simulationConfiguration = GameManager.Instance.LoadSimulationConfiguration(selectedConfigurationName);
            currentSimulationConfiguration = (SimulationConfiguration)_simulationConfiguration.Clone();

            originalCurrentSimulationConfiguration = (SimulationConfiguration)currentSimulationConfiguration.Clone();

            UpdateUI();
        }
    }

    public void DeleteSelectedConfiguration() {

        if (GameManager.Instance == null) {
            return;
        }

        if (selectedConfigurationName == "" || selectedConfigurationName == defaultSimulationConfiguration.simulationName) {
            return;
        }

        // if we are deleting the current simulation then load the default simulation
        if (selectedConfigurationName == currentSimulationConfiguration.simulationName) {

            SimulationConfiguration _simulationConfiguration = GameManager.Instance.LoadSimulationConfiguration(defaultSimulationConfiguration.simulationName);
            currentSimulationConfiguration = (SimulationConfiguration)_simulationConfiguration.Clone();

            originalCurrentSimulationConfiguration = (SimulationConfiguration)currentSimulationConfiguration.Clone();

            UpdateUI();
        }

        GameManager.Instance.DeleteSimulationConfiguration(selectedConfigurationName);

        simulationConfigurationsList = GameManager.Instance.GetSimulationConfigurations();
        UpdateConfigurationListUI();

    }

    private Boolean IsDataDirty() {

        if (currentSimulationConfiguration != originalCurrentSimulationConfiguration) {
            return true;
        } else {
            return false;
        }
    }
}
