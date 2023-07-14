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

    public void Unlock(string stageNum) {
        Stage next = stages.Find(stage => stage.name.Equals(stageNum));
        next.isUnlocked = true;
    }

    public bool IsUnlocked(string stageNum) {
        return stages.Find(stage => stage.name.Equals(stageNum)).isUnlocked;
    }
}
