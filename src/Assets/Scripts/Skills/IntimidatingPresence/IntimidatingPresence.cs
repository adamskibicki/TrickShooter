using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class IntimidatingPresence : MonoBehaviour, ISkill
{
    public readonly static string skillName = "Intimidating presence";

    public readonly static List<Feature> features = new List<Feature>
    {
        new Feature { name = "Cooldown", valuesForLevels = new int[] { 15, 13, 11, 8 } },
        new Feature { name = "Damage increase", valuesForLevels = new int[] { 20, 30, 40, 50 } },
        new Feature { name = "Duration", valuesForLevels = new int[] { 4, 5, 6, 7 } },
        new Feature { name = "Radius", valuesForLevels = new int[] { 4, 6, 8, 10 } },
        new Feature { name = "Slow effect", valuesForLevels = new int[] { 30, 45, 60, 75 } },
    };


    public GameObject Aura;

    public int currentCooldown { get; private set; }
    public int currentDamageIncrease { get; private set; }
    public int currentDuration { get; private set; }
    public int currentRadius { get; private set; }
    public int currentSlowEffect { get; private set; }

    public float GetCooldown()
    {
        return currentCooldown;
    }

    public void LoadSkill(SkillSave skillSave)
    {
        currentCooldown = features[0].valuesForLevels[3];
        currentDamageIncrease = features[1].valuesForLevels[3];
        currentDuration = features[2].valuesForLevels[3];
        currentRadius = features[3].valuesForLevels[3];
        currentSlowEffect = features[4].valuesForLevels[3];
    }

    public void Use()
    {
        GameObject gm = Instantiate(Aura, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
        gm.transform.parent = transform;
        gm.transform.localScale = new Vector3(currentRadius / 2.25f, currentRadius / 2.25f, 0f);
        gm.GetComponent<IntimidatingPresenceAura>().SetSkillData(1f + (float)currentDamageIncrease / 100,(float)currentSlowEffect / 100);
        Destroy(gm, currentDuration);
    }
}