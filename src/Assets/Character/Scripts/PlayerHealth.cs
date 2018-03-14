using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealth : Damage
{
    float regenCooldown;
    float regenTimer;
    int healthRegenAmount;

    public Slider healthSlider;

    public override void OnStart()
    {
        damageEffects = new List<DamageEffect>();

        maxHealth = Profile.current.baseAttributtes.toughness * 10;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        regenCooldown = 2.5f;
        regenTimer = 0f;
        healthRegenAmount = Profile.current.baseAttributtes.toughness / 3;
    }

    void Update()
    {
        regenTimer += Time.deltaTime;
        if (regenTimer >= regenCooldown)
        {
            regenTimer = 0f;
            ApplyDamage(-healthRegenAmount);
        }
    }

    public override void ApplyDamage(int amount)
    {
        currentHealth -= amount * GetDamageMultiplier(); ;

        if (currentHealth <= 0)
        {
            Death();
            healthSlider.value = 0;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthSlider.value = maxHealth;
        }
        else
        {
            healthSlider.value = currentHealth;
        }
    }

    public override void Death()
    {
        Destroy(gameObject, 0.5f);
    }
}
