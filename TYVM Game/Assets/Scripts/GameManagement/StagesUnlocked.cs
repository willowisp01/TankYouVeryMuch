using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StagesUnlocked")]
public class StagesUnlocked : ScriptableObject {
    public Dictionary<string, bool> unlockedStages = new Dictionary<string, bool>();
    public bool stage1;

    public void Unlock() {

    }
}
