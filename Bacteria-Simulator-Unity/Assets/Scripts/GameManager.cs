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


    // Playerlist
//    private List<PlayerInfo> playerList = new List<PlayerInfo>();

    // Awake is called when the script instance is being loaded
    private void Awake() {

        // Make GameManager a singleton
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        // Do not destroy this Object when loading a new Scene
        DontDestroyOnLoad(gameObject);

        Instance = this;

        // Load saved stuff
        Load();

    }

    public void GotoScene(string sceneName) {

        // Load specified scene
        SceneManager.LoadScene(sceneName);
    }

    // Quit game from native Linux build
    public void QuitGame() {

        // Save if exit game
        Save();

         // original code to quit Unity player
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }


    // Load and save code between sessions
    [System.Serializable]
    class SaveData {

         // Name of last/current player
//        public string playerNameSave;


        // List of players
//        public List<PlayerInfo> playerListSave = new List<PlayerInfo>();

    }

    public void Save() {
        SaveData data = new SaveData();
//        data.playerNameSave = playerName;

        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Load last playername
  //          playerName = data.playerNameSave;

            // Load playerlist
//            playerList = data.playerListSave;

        }
    }

}
