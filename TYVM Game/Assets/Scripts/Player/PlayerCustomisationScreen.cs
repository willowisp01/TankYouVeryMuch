using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomisationScreen : MonoBehaviour {

    [System.Serializable]
    public class Screen {
        public Button open;
        public GameObject screen;
        public List<Choice> options;
    }

    [System.Serializable]
    public class Choice {
        public Button button;
        public GameObject display;
    }

    private List<Screen> screens = new List<Screen>();

    [SerializeField]
    private Screen appearanceScreen;

    [SerializeField]
    private Screen projectileScreen;

    [SerializeField]
    private Screen abilityScreen;

    private PlayerCustomisation playerCustomisation;

    private void Awake() {
        screens.Add(appearanceScreen);
        screens.Add(projectileScreen);
        screens.Add(abilityScreen);
        // Adding listeners to the screens manually because Unity inspector is so bad
        appearanceScreen.open.onClick.AddListener(() => OpenScreen(appearanceScreen));
        projectileScreen.open.onClick.AddListener(() => OpenScreen(projectileScreen));
        abilityScreen.open.onClick.AddListener(() => OpenScreen(abilityScreen));
        // Now, adding listeners to the choice buttons in each screen
        appearanceScreen.options[0].button.onClick.AddListener(() => Choose(appearanceScreen.options[0], appearanceScreen));
        appearanceScreen.options[1].button.onClick.AddListener(() => Choose(appearanceScreen.options[1], appearanceScreen));
        projectileScreen.options[0].button.onClick.AddListener(() => Choose(projectileScreen.options[0], projectileScreen));
        projectileScreen.options[1].button.onClick.AddListener(() => Choose(projectileScreen.options[1], projectileScreen));
        abilityScreen.options[0].button.onClick.AddListener(() => Choose(abilityScreen.options[0], abilityScreen));
        abilityScreen.options[1].button.onClick.AddListener(() => Choose(abilityScreen.options[1], abilityScreen));
        abilityScreen.options[2].button.onClick.AddListener(() => Choose(abilityScreen.options[2], abilityScreen));
        playerCustomisation = GetComponent<PlayerCustomisation>();
    }

    private void OpenScreen(Screen screen) {
        foreach (var _screen in screens) {
            CloseScreen(_screen);
        }
        screen.screen.SetActive(true);
        screen.open.interactable = false;
        int choice;
        switch (screen.screen.name) {
            case "AppearanceScreen":
                choice = playerCustomisation.GetAppearance();
                Choose(appearanceScreen.options[choice], screen);
                break;
            case "ProjectileScreen":
                choice = playerCustomisation.GetProjectile();
                Choose(projectileScreen.options[choice], screen);
                break;
            case "AbilityScreen":
                choice = playerCustomisation.GetAbility();
                Choose(abilityScreen.options[choice], screen);
                break;
        }
    }

    private void CloseScreen(Screen screen) {
        screen.screen.SetActive(false);
        screen.open.interactable = true;
    }

    private void Choose(Choice choice, Screen screen) {
        foreach (var option in screen.options) {
            Unchoose(option);
        }
        choice.display.SetActive(true);
        choice.button.interactable = false;
        int index = screen.options.IndexOf(choice);
        switch (screen.screen.name) {
            case "AppearanceScreen":
                playerCustomisation.ChangeAppearance(index);
                break;
            case "ProjectileScreen":
                playerCustomisation.ChangeProjectile(index);
                break;
            case "AbilityScreen":
                playerCustomisation.ChangeAbility(index);
                break;
        }
    }

    private void Unchoose(Choice choice) {
        choice.display.SetActive(false);
        choice.button.interactable = true;
    }
}
