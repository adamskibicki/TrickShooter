using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Damage : MonoBehaviour, IDamageable
{
    protected List<DamageEffect> damageEffects;

    public float maxHealth { get; protected set; }

    public float currentHealth { get; protected set; }

    public abstract void OnStart();

    protected float GetDamageMultiplier()
    {
        float multiplier = 1f;

        foreach (DamageEffect item in damageEffects)
        {
            multiplier *= item.value;
        }

        return multiplier;
    }

    public void AddDamageEffect(DamageEffect effect)
    {
        for (int i = 0; i < damageEffects.Count; i++)
        {
            if (damageEffects[i].name == effect.name)
            {
                return;
            }
        }
        damageEffects.Add(effect);
    }

    public void RemoveDamageEffect(string name)
    {
        for (int i = 0; i < damageEffects.Count; i++)
        {
            if (damageEffects[i].name == name)
            {
                damageEffects.RemoveAt(i);
                return;
            }
        }
    }

    public abstract void ApplyDamage(int amount);

    public abstract void Death();
}
