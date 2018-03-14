using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyHealth : Damage
{
    public Slider healthSlider;
    public Text damageText;

    int recentDamage;
    float damageTextDisappearTimer;

    EnemyStatistics ES;

    public override void OnStart()
    {
        damageEffects = new List<DamageEffect>();

        ES = GetComponent<EnemyStatistics>();
        maxHealth = ES.toughness * 10;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        recentDamage = 0;
        damageTextDisappearTimer = 0f;

        damageText.text = "";
    }

    void Update()
    {
        if (recentDamage != 0)
        {
            damageTextDisappearTimer += Time.deltaTime;
            damageText.text = Convert.ToString(recentDamage);
        }
        if (damageTextDisappearTimer >= 1f)
        {
            recentDamage = 0;
            damageTextDisappearTimer = 0f;
            damageText.text = "";
        }
    }

    public override void ApplyDamage(int amount)
    {
        int damageTaken = (int)(amount * GetDamageMultiplier());

        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
        recentDamage -= damageTaken;
        damageTextDisappearTimer = 0f;
        healthSlider.value = currentHealth;
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
