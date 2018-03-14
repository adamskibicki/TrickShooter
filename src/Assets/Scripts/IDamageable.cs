using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void ApplyDamage(int amount);

    void AddDamageEffect(DamageEffect damageEffect);

    void RemoveDamageEffect(string name);
}
