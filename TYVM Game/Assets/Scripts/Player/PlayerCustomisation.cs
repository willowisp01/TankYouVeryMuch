using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomisation : MonoBehaviour {

    [SerializeField]
    private List<TankAppearance> appearanceChoices;

    [SerializeField]
    private List<GameObject> projectileChoices;

    [SerializeField]
    private List<Skill> abilityChoices;

    [SerializeField]
    private GameObject player;
    private AppearanceSelect appearance;
    private Shooting shooting;
    private Trajectory trajectory;
    private SkillSelect ability;
    private int appearanceChoice;
    private int projectileChoice;
    private int abilityChoice;

    private void Awake() {
        appearance = player.GetComponent<AppearanceSelect>();
        shooting = player.GetComponent<Shooting>();
        trajectory = player.GetComponent<Trajectory>();
        ability = player.GetComponent<SkillSelect>();
    }

    private void Start() {
        LoadData();
    }

    public void ChangeAppearance(int appearanceChoice) {
        this.appearanceChoice = appearanceChoice;
        appearance.Change(appearanceChoices[appearanceChoice]);
        SaveDataManager.SaveAppearance(appearanceChoice);
    }

    public void ChangeProjectile(int projectileChoice) {
        this.projectileChoice = projectileChoice;
        shooting.ChangeProjectile(projectileChoices[projectileChoice]);
        trajectory.ChangeTrajectory(projectileChoice);
        SaveDataManager.SaveProjectile(projectileChoice);
    }

    public void ChangeAbility(int abilityChoice) {
        this.abilityChoice = abilityChoice;
        ability.skill = abilityChoices[abilityChoice];
        SaveDataManager.SaveAbility(abilityChoice);
    }

    public int GetAppearance() {
        return appearanceChoice;
    }

    public int GetProjectile() {
        return projectileChoice;
    }

    public int GetAbility() {
        return abilityChoice;
    }

    private void LoadData() {
        SaveDataManager.SaveData saveData = SaveDataManager.GetSaveData();
        appearanceChoice = saveData.playerCustomisation.appearance;
        projectileChoice = saveData.playerCustomisation.projectile;
        abilityChoice = saveData.playerCustomisation.ability;
        ChangeAppearance(appearanceChoice);
        ChangeProjectile(projectileChoice);
        ChangeAbility(abilityChoice);
    }
}
