using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StagesUnlocked")]
public class StagesUnlocked : ScriptableObject {

    [System.Serializable]
    private class Stage {
        public string name;
        public bool isUnlocked;
    }

    [SerializeField]
    private List<Stage> stages;

    private void Awake() {
        UnlockTo(SaveDataManager.GetSaveData().stagesCompletedTo);
    }

    public void Unlock(string stageNum) {
        Stage next = stages.Find(stage => stage.name.Equals(stageNum));
        SaveDataManager.SaveStages(stageNum);
        next.isUnlocked = true;
    }

    public bool IsUnlocked(string stageNum) {
        return stages.Find(stage => stage.name.Equals(stageNum)).isUnlocked;
    }

    private void UnlockTo(string stageNum) {
        foreach (var stage in stages) {
            stage.isUnlocked = true;
            if (stage.name.Equals(stageNum)) {
                break;
            }
        }
    }
}
