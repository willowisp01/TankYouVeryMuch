using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float currentHealth;
    public float maxHealth;

    public void SetMaxHealth(float health) {
        maxHealth = health;
        currentHealth = health;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        SetHealth(currentHealth);
    }
}
