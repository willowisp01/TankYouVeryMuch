using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour {

    [System.Serializable]
    public class SaveData {
        public string stagesCompletedTo = "Stage 1";
        public Settings settings;
        public Customisation playerCustomisation;
    }

    [System.Serializable]
    public class Settings {
        public float volume = 1f;
    }

    [System.Serializable]
    public class Customisation {
        public int appearance;
        public int projectile;
        public int ability;
    }

    private static SaveData saveData;
    public static SaveDataManager Instance { get; private set; } // Singleton. We don't want more than 1 global instance of a save data manager.
    private static string saveDataFilePath;

    // Since we want this to be a singleton, we check if there are any other instances present other than this. If there are, destroy this.
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
        saveDataFilePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    public static void Save() {
        using StreamWriter writer = new StreamWriter(saveDataFilePath);
        string data = JsonUtility.ToJson(saveData); // Converts SaveData into json
        writer.WriteLine(data); // Writes to the file as specified by the filepath
    }

    // The game will load existing save data when it first starts (after Awake is called)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Load() {
        // Create a new save file if one does not exist
        if (!File.Exists(saveDataFilePath)) {
            saveData = new SaveData();
            Save();
        }
        using StreamReader reader = new StreamReader(saveDataFilePath);
        string data = reader.ReadToEnd();
        saveData = JsonUtility.FromJson<SaveData>(data); // Converts the data read into SaveData
    }

    public static SaveData GetSaveData() {
        return saveData;
    }

    public static void SaveStages(string stageNum) {
        saveData.stagesCompletedTo = stageNum;
    }

    public static void SaveVolume(float volume) {
        saveData.settings.volume = volume;
    }

    public static void SaveAppearance(int choice) {
        saveData.playerCustomisation.appearance = choice;
    }
    public static void SaveProjectile(int choice) {
        saveData.playerCustomisation.projectile = choice;
    }
    public static void SaveAbility(int choice) {
        saveData.playerCustomisation.ability = choice;
    }

    // This triggers before the application quits
    private void OnApplicationQuit() {
        Save();
    }
}
