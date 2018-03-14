using UnityEngine;
using System.Collections;

public interface ISkill
{
    float GetCooldown();

    void LoadSkill(SkillSave skillSave);

    void Use();
}
