using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelect : MonoBehaviour {

    [SerializeField]
    private Skill skill; // The skill chosen
    private float cooldown; // The cooldown before the skill can be activated again
    private float activeTime; // The time the skill will be active for
    private float uses; // The number of times the skill can be used

    // An enum type to keep track of the different states of the skill
    private enum SkillState {
        canUse, // The skill can be used
        isActive, // The skill is currently active
        onCooldown, // The skill is on cooldown and cannot be used
        noMoreUses // All uses of the skill have been exhausted
    }

    SkillState state = SkillState.canUse; // The skill can be used at the start

    [SerializeField]
    private KeyCode activationKey; // The key to activate the skill

    private void Awake() {
        uses = skill.uses;
    }

    // Update is called once per frame
    private void Update() {
        switch (state) {
            // If the skill can be used, activate it upon pressing the key
            case SkillState.canUse:
                if (Input.GetKeyDown(activationKey)) {
                    skill.Activate(gameObject);
                    uses--; // Number of uses decreases by 1
                    state = SkillState.isActive; // The skill is now active
                    activeTime = skill.activeTime; // The skill is active for activeTime
                }
                break;
            // If the skill is active, it goes into cooldown when activeTime is up
            case SkillState.isActive:
                activeTime -= Time.deltaTime;
                if (activeTime <= 0) {
                    state = SkillState.onCooldown; // The skill is now on cooldown
                    cooldown = skill.cooldown;
                }
                if (uses == 0) {
                    state = SkillState.noMoreUses;
                }
                break;
            // The skill is ready to use again once the cooldown is over
            case SkillState.onCooldown:
                cooldown -= Time.deltaTime;
                if (cooldown <= 0) {
                    state = SkillState.canUse; // The skill can be used again
                }
                break;
            case SkillState.noMoreUses:
                break;
        }
    }
}
