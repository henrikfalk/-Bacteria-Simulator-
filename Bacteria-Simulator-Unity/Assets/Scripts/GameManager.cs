using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{

    // Singleton
    public static GameManager Instance { get; private set; }

    // Standard laboratory settings
    private LaboratoryInfo standardLaboratoryInfo = new LaboratoryInfo {
        middleTemperatureInfo = 30,
        toxicityInfo = 0,
        maxVelocityGreen = 1,
        temperatureOptimalBacteriaGreen = 20,
        temperatureRangeBacteriaGreen = 11,
        maxAgeMinutesBacteriaGreen = 2,
        fertilityPercentBacteriaGreen = 75,
        maxVelocityRed = 2,
        temperatureOptimalBacteriaRed = 40,
        temperatureRangeBacteriaRed = 10,
        maxAgeMinutesBacteriaRed = 3,
        fertilityPercentBacteriaRed = 50

    };

    // Current laboratory settings
    private LaboratoryInfo currentLaboratoryInfo;

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

        // Take the current laboratory values from the standard values
        currentLaboratoryInfo = (LaboratoryInfo)standardLaboratoryInfo.Clone();

        // Load saved stuff
        LoadLab();

    }

    public void GotoScene(string sceneName) {

        // Load specified scene
        SceneManager.LoadScene(sceneName);
    }

    // Quit game from native Linux build
    public void QuitGame() {

        // I will properly not save in final vesion
//        Save();

         // original code to quit Unity player
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }


    public LaboratoryInfo getCurrentLaboratoryInfo() {
        return currentLaboratoryInfo;
    }

    // Load and save code between sessions
    [System.Serializable]
    class SaveData {

         // Name of last/current player
//        public string playerNameSave;


        // List of players
//        public List<PlayerInfo> playerListSave = new List<PlayerInfo>();

    }

    public void SaveLab() {
//        SaveData data = new SaveData();
//        data.playerNameSave = playerName;

        string json = JsonUtility.ToJson(currentLaboratoryInfo);
  
        File.WriteAllText(Application.persistentDataPath + "/labSaveFile.json", json);
    }

    public void LoadLab() {
        string path = Application.persistentDataPath + "/labSaveFile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            currentLaboratoryInfo = JsonUtility.FromJson<LaboratoryInfo>(json);

            // Load last playername
  //          playerName = data.playerNameSave;

            // Load playerlist
//            playerList = data.playerListSave;

        }
    }

}
