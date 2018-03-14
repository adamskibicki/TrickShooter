using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BladeWave : MonoBehaviour, ISkill
{
    public readonly static string skillName = "Blade wave";

    public readonly static List<Feature> features = new List<Feature>
    {
        new Feature { name = "Blade count", valuesForLevels = new int[] { 10, 15, 20, 25 } },
        new Feature { name = "Blade damage", valuesForLevels = new int[] { 15, 22, 30, 40 } },
        new Feature { name = "Cooldown", valuesForLevels = new int[] { 12, 10, 8, 6 } },
        new Feature { name = "Range", valuesForLevels = new int[] { 6, 8, 10, 12 } },
        new Feature { name = "Throw angle", valuesForLevels = new int[] { 90, 180, 270, 360 } },
    };


    public GameObject Blade;

    public int currentBladeCount { get; private set; }
    public int currentBladeDamage { get; private set; }
    public int currentCooldown { get; private set; }
    public int currentRange { get; private set; }
    public int currentThrowAngle { get; private set; }

    public float GetCooldown()
    {
        return currentCooldown;
    }

    public void LoadSkill(SkillSave skillSave)
    {
        currentBladeCount = features[0].valuesForLevels[3];
        currentBladeDamage = features[1].valuesForLevels[3];
        currentCooldown = features[2].valuesForLevels[3];
        currentRange = features[3].valuesForLevels[3];
        currentThrowAngle = features[4].valuesForLevels[2];
    }

    public void Use()
    {
        float angleBetweenBlades = currentThrowAngle / (currentBladeCount - 1);
        float startAngle = transform.eulerAngles.z + currentThrowAngle / 2;

        for (int i = 0; i < currentBladeCount; i++)
        {
            GameObject GM = Instantiate(Blade, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

            GM.GetComponent<BladeHitScript>().Throw(startAngle - i * angleBetweenBlades, currentRange, currentBladeDamage);
        }
    }
}