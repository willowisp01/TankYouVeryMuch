using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject {
    public string skillName; // The name of the skill
    public float cooldown; // The cooldown before the skill can be activated again
    public float activeTime; // The time the skill will be active for
    public float uses; // The number of times the skill can be used

    public virtual void Activate(GameObject player) {
        // This method will be called to activate the ability
    }
}
