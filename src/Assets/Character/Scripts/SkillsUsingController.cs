using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillsUsingController : MonoBehaviour
{
    public Slider skillOneCooldownSlider;
    public Slider skillTwoCooldownSlider;


    ISkill skillOne;
    ISkill skillTwo;

    bool canUseSkillOne;
    float skillOneCooldown;
    float skillOneTimer;

    bool canUseSkillTwo;
    float skillTwoCooldown;
    float skillTwoTimer;

    public void OnStart()
    {
        if (BladeWave.skillName == "Blade wave")
        {
            skillOne = GetComponent<BladeWave>();
            skillOne.LoadSkill(null);
            skillOneCooldown = skillOne.GetCooldown();
            
        }

        canUseSkillOne = true;

        skillOneCooldownSlider.maxValue = skillOneCooldown;
        skillOneCooldownSlider.value = skillOneCooldownSlider.maxValue;
        skillOneTimer = skillOneCooldown;



        if (IntimidatingPresence.skillName == "Intimidating presence")
        {
            skillTwo = GetComponent<IntimidatingPresence>();
            skillTwo.LoadSkill(null);
            skillTwoCooldown = skillTwo.GetCooldown();
        }

        canUseSkillTwo = true;

        skillTwoCooldownSlider.maxValue = skillTwoCooldown;
        skillTwoCooldownSlider.value = skillTwoCooldownSlider.maxValue;
        skillTwoTimer = skillTwoCooldown;
    }

    void Update()
    {
        skillOneTimer += Time.deltaTime;

        if (skillOneTimer >= skillOneCooldown)
        {
            canUseSkillOne = true;
            skillOneCooldownSlider.value = skillOneCooldownSlider.maxValue;
        }
        else
        {
            skillOneCooldownSlider.value = skillOneTimer;
        }

        skillTwoTimer += Time.deltaTime;

        if (skillTwoTimer >= skillTwoCooldown)
        {
            canUseSkillTwo = true;
            skillTwoCooldownSlider.value = skillTwoCooldownSlider.maxValue;
        }
        else
        {
            skillTwoCooldownSlider.value = skillTwoTimer;
        }
    }

    public void UseSkillOne()
    {
        if (canUseSkillOne == true)
        {
            skillOne.Use();
            skillOneTimer = 0f;
            canUseSkillOne = false;
        }
        else
        {
            return;
        }
    }

    public void UseSkillTwo()
    {
        if (canUseSkillTwo == true)
        {
            skillTwo.Use();
            skillTwoTimer = 0f;
            canUseSkillTwo = false;
        }
        else
        {
            return;
        }
    }
}
