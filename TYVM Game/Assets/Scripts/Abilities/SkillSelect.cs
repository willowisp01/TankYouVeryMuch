using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelect : MonoBehaviour {

    [SerializeField]
    public Skill skill; // The skill chosen
    private float cooldown; // The cooldown before the skill can be activated again
    private float activeTime; // The time the skill will be active for
    private float uses; // The number of times the skill can be used

    // An enum type to keep track of the different states of the skill
    private enum SkillState {
        CanUse, // The skill can be used
        CannotUse, // The skill cannot be used
    }

    [SerializeField]
    SkillState state = SkillState.CanUse; // The skill can be used at the start

    [SerializeField]
    private KeyCode activationKey; // The key to activate the skill

    private void Awake() {
        uses = skill.uses;
        cooldown = skill.cooldown;
        activeTime = skill.activeTime;
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(activationKey) && state == SkillState.CanUse && uses > 0) {
            Coroutine skillCycle = StartCoroutine(SkillCycle());
        }
    }

    private IEnumerator SkillCycle() {
        state = SkillState.CannotUse;
        skill.Activate(gameObject);
        uses--;
        yield return new WaitForSeconds(cooldown);
        if (uses > 0) {
            state = SkillState.CanUse;
        }
    }
}
