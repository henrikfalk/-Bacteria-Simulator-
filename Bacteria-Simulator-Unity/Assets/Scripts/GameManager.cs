using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
//using System.Linq;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{

    // Singleton
    public static GameManager Instance { get; private set; }

    // Game settings
    private GameSettings gameSettings;
    private GameSettings defaultGameSettings = new GameSettings {
        currentSimulationConfigurationName = "Default simulation",
    };

    // Standard laboratory settings
    private LaboratoryInfo defaultLaboratoryInfo = new LaboratoryInfo {
        middleTemperatureInfo = 30,
        toxicityInfo = 0,
        yellowWarningToxicityInfo = 500,
        redWarningToxicityInfo = 1000,
        maxLimitToxicityInfo = 2000,
        maxVelocityGreen = 1,
        temperatureOptimalBacteriaGreen = 20,
        temperatureRangeBacteriaGreen = 11,
        maxAgeMinutesBacteriaGreen = 2,
        fertilityPercentBacteriaGreen = 75,
        maxVelocityRed = 2,
        temperatureOptimalBacteriaRed = 40,
        temperatureRangeBacteriaRed = 10,
        maxAgeMinutesBacteriaRed = 3,
        fertilityPercentBacteriaRed = 50,
        maxVelocityPurple = 3,
        temperatureOptimalBacteriaPurple = 40,
        temperatureRangeBacteriaPurple = 10,
        maxAgeMinutesBacteriaPurple = 3,
        fertilityPercentBacteriaPurple = 50
    };

   // Default simulation configuration
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

    string configurationPath;


    // Current laboratory settings
    private LaboratoryInfo currentLaboratoryInfo;

    // Current simulation configuration
    private SimulationConfiguration currentSimulationConfiguration;

    // List of simulation configurations
    List<SimulationConfiguration> simulationConfigurationList = new List<SimulationConfiguration>();
    Boolean isSimulationConfigurationListDirty = false;

    private string currentScene = "MainMenuScene";
    private string previousScene = "MainMenuScene";

    // Awake is called when the script instance is being loaded
    private void Awake() {

        // Make GameManager a singleton
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        
        // Do not destroy this Object when loading a new Scene
        DontDestroyOnLoad(gameObject);

        // Make reference to self
        Instance = this;

        // Set up path to simulator configurations 
        configurationPath = Application.persistentDataPath + "/configurations";        

        // Take the current laboratory values from the standard values
        currentLaboratoryInfo = (LaboratoryInfo)defaultLaboratoryInfo.Clone();

        currentSimulationConfiguration = (SimulationConfiguration)defaultSimulationConfiguration.Clone();

        // Load game settings
        LoadGameSettings();

        // save default simulation configuration if not saved
        SaveDefaultSimulationConfiguration();

        // Load all saved configurations
        simulationConfigurationList = LoadSimulationConfigurations();

//        Debug.Log("Initialize: gameSettings.currentSimulationConfigurationName = " + gameSettings.currentSimulationConfigurationName);

        // Initialize current configuration
        foreach (SimulationConfiguration _configuration in simulationConfigurationList) {

//            Debug.Log("Initialize: _configuration.simulationName = " + _configuration.simulationName);

            if (_configuration.simulationName == gameSettings.currentSimulationConfigurationName){

//                Debug.Log("gameSettings.currentSimulationConfigurationName = " + gameSettings.currentSimulationConfigurationName);
                currentSimulationConfiguration = (SimulationConfiguration)_configuration.Clone();
            }
        }

        // Load saved stuff
        LoadLab();
    }

    public void GotoScene(string sceneName) {

        previousScene = currentScene;
        currentScene = sceneName;

        // Load specified scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GotoPreviousScene() {

        GotoScene(previousScene);
    }

    // Exit game
    public void ExitGame() {

         // original code to quit Unity player
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    // ENCAPSULATION
    public LaboratoryInfo GetCurrentLaboratoryInfo() {
        return currentLaboratoryInfo;
    }

    // ENCAPSULATION
    public void SetCurrentLaboratoryInfo(LaboratoryInfo info) {

        currentLaboratoryInfo = (LaboratoryInfo)info.Clone();

        // Save
        SaveLab();
    }

    public LaboratoryInfo GetDefaultLaboratoryInfo() {
        return defaultLaboratoryInfo;
    }

    public SimulationConfiguration GetCurrentSimulationConfiguration() {
        return currentSimulationConfiguration;
    }

    // ENCAPSULATION
    public void SetCurrentSimulationConfiguration(SimulationConfiguration info) {

        currentSimulationConfiguration = (SimulationConfiguration)info.Clone();

        // Save
        SaveLab();
    }

    public SimulationConfiguration GetSimulationConfigurationData() {
        return defaultSimulationConfiguration;
    }

    /********************
        Load and save game settings between sessions
    ********************/
    [System.Serializable]
    class SaveGameSettingsData {

        public GameSettings saveGameSettings;

    }

    public void LoadGameSettings() {

        string path = Path.Combine(Application.persistentDataPath, "GameSettings.json");

        if (File.Exists(path)) {

            try {

                string json = File.ReadAllText(path);
                SaveGameSettingsData _gameSettingsData = JsonUtility.FromJson<SaveGameSettingsData>(json);
                gameSettings = (GameSettings)_gameSettingsData.saveGameSettings.Clone();
            } catch (System.Exception e) {
                Debug.Log("The process failed: {0}: " + e.ToString());
            }

        } else {
//        Debug.Log("Not finding GameSettings!");
            gameSettings = (GameSettings)defaultGameSettings.Clone();

            SaveGameSettings();
        }

//        Debug.Log("gameSettings.currentSimulationConfigurationName = " + gameSettings.currentSimulationConfigurationName);
    }

    public void SaveGameSettings() {
        SaveGameSettingsData data = new SaveGameSettingsData();

        data.saveGameSettings = (GameSettings)gameSettings.Clone();

        string json = JsonUtility.ToJson(data);
  
        try {
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "GameSettings.json"), json);
        } catch (System.Exception e) {
            Debug.Log("The process failed: {0}: " + e.ToString());
        }
    }

    /**********
    // Load and save code between sessions
    **********/
    [System.Serializable]
    class SaveData {

        public LaboratoryInfo saveCurrentLaboratoryInfo;

    }

    public void SaveLab() {
        SaveData data = new SaveData();

        data.saveCurrentLaboratoryInfo = currentLaboratoryInfo;

        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/labSaveFile.json", json);
    }

    public void LoadLab() {
        string path = Application.persistentDataPath + "/labSaveFile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (currentLaboratoryInfo != null) {
                currentLaboratoryInfo = (LaboratoryInfo)data.saveCurrentLaboratoryInfo.Clone();
            }
        }
    }

    /********************
        Load and save a simulation configuration
    ********************/
    [System.Serializable]
    class SaveSimulationConfigurationData {

        public SimulationConfiguration saveCurrentSimulationConfiguration;

    }

    public void SaveSimulationConfiguration(SimulationConfiguration _simulationConfiguration) {

        if (CreateConfigurationDirectory() == false) {

            // Could not create configuration directory
            return;
        }

        SaveSimulationConfigurationData data = new SaveSimulationConfigurationData();

        data.saveCurrentSimulationConfiguration = _simulationConfiguration;

        string json = JsonUtility.ToJson(data);
  
        try {
            File.WriteAllText(Path.Combine(configurationPath, _simulationConfiguration.simulationName + ".json"), json);
        } catch (System.Exception e) {
            Debug.Log("The process failed: {0}: " + e.ToString());
        }

        currentSimulationConfiguration = (SimulationConfiguration)_simulationConfiguration.Clone();

        isSimulationConfigurationListDirty = true;
        gameSettings.currentSimulationConfigurationName = currentSimulationConfiguration.simulationName;

        SaveGameSettings();
    }

    public SimulationConfiguration LoadSimulationConfiguration(String _simulationConfigurationName) {

        string path = Path.Combine(configurationPath,_simulationConfigurationName + ".json");
        if (File.Exists(path)) {

            try {
                string json = File.ReadAllText(path);

                SaveSimulationConfigurationData data = JsonUtility.FromJson<SaveSimulationConfigurationData>(json);

                currentSimulationConfiguration = (SimulationConfiguration)data.saveCurrentSimulationConfiguration.Clone();

                gameSettings.currentSimulationConfigurationName = currentSimulationConfiguration.simulationName;
                SaveGameSettings();
            } catch (System.Exception e) {
                Debug.Log("The process failed: {0}: " + e.ToString());
            }
        } else {
            Debug.Log("Could not find File to load: " + path);
        }

        return currentSimulationConfiguration;
    }

    public List<SimulationConfiguration> GetSimulationConfigurations() {

        if (isSimulationConfigurationListDirty == true) {

            simulationConfigurationList = LoadSimulationConfigurations(); 

            isSimulationConfigurationListDirty = false;
        }

        return simulationConfigurationList;
    }

    public List<SimulationConfiguration> LoadSimulationConfigurations() {

        List<SimulationConfiguration> _simulationConfigurationList = new List<SimulationConfiguration>();
        
        if (CreateConfigurationDirectory() == false) {

            // Could not create configuration directory
            return _simulationConfigurationList;
        }

        IEnumerable<string> configurationsFiles = Directory.EnumerateFiles(configurationPath, "*.json");

        foreach (string currentConfigurationFile in configurationsFiles) {

//            Debug.Log("Configuration file: " + currentConfigurationFile);

            // read configuration file
            try {
                string json = File.ReadAllText(currentConfigurationFile);
                SaveSimulationConfigurationData data = JsonUtility.FromJson<SaveSimulationConfigurationData>(json);

                SimulationConfiguration simulationConfiguration = (SimulationConfiguration)data.saveCurrentSimulationConfiguration.Clone();

                _simulationConfigurationList.Add(simulationConfiguration);
            } catch (System.Exception e) {
                Debug.Log("The process failed: {0}: " + e.ToString());
            }
        }

        return _simulationConfigurationList;
    }

    private Boolean CreateConfigurationDirectory () {

        try {

            // check if "configurations" directory exist
            if (Directory.Exists(configurationPath) == true) {

                return true;
            } else {

                // Create "configurations" directory
                DirectoryInfo dir = Directory.CreateDirectory(configurationPath);
                return true;
            }

        } catch (System.Exception e) {
            Debug.Log("The process failed: {0}: " + e.ToString());
            return false;
        }
    }

    public void SaveDefaultSimulationConfiguration() {

        if (CreateConfigurationDirectory() == false) {

            // Could not create configuration directory
            return;
        }

        string defaultSimulationConfigurationFileName = Path.Combine(configurationPath,defaultSimulationConfiguration.simulationName + ".json");

        try {
            // check if defaultSimulationConfiguration exists
            if (File.Exists(defaultSimulationConfigurationFileName) == true) {
                return;
            }
        } catch (System.Exception e) {
            Debug.Log("The process failed: {0}: " + e.ToString());
            return;
        }

        SaveSimulationConfigurationData data = new SaveSimulationConfigurationData();

        data.saveCurrentSimulationConfiguration = defaultSimulationConfiguration;

        string json = JsonUtility.ToJson(data);
  
        try {
            File.WriteAllText(defaultSimulationConfigurationFileName, json);
        } catch (System.Exception e) {
            Debug.Log("The process failed: {0}: " + e.ToString());
        }
    }

    public SimulationConfiguration GetDefaultSimulationConfiguration() {
        return defaultSimulationConfiguration;
    }

    public void DeleteSimulationConfiguration(string _configurationName) {

        if (_configurationName == defaultSimulationConfiguration.simulationName) {
            return;
        }

        string deleteConfigurationFileName = Path.Combine(configurationPath,_configurationName + ".json");

        // Delete configuration
        try {
            File.Delete(deleteConfigurationFileName);
        } catch (System.Exception e) {
            Debug.Log("The process failed: {0}: " + e.ToString());
        }

        isSimulationConfigurationListDirty = true;
        
    }
}
