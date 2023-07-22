using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour {

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField]
    private float waitTime = 0;
    public float cooldown;

    [SerializeField]
    private bool onCooldown = false;

    private void Start() {
        slider.maxValue = cooldown;
        slider.value = slider.maxValue;
    }

    private void Update() {
        Charge();
    }

    public void Charge() {
        if (onCooldown) {
            waitTime += Time.deltaTime;
            slider.value = waitTime;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        if (waitTime >= cooldown) { //skill is ready 
            onCooldown = false;
            waitTime = 0;
        }
    }

    public void UseSkill() {
        onCooldown = true;
    }
}
