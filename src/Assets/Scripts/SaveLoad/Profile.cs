using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Profile
{
    public static Profile current;

    public string name { get; private set; }
    public string playerClass { get; private set; }
    public BaseAttributtes baseAttributtes { get; private set; }

    public int currentLevel { get; private set; }
    public int experienceToNextLevel { get; private set; }
    public int currentExperience { get; private set; }

    public int skillPoints { get; private set; }

    public Profile(string name, string playerClass)
    {
        switch (playerClass)
        {
            case "Commando":
                baseAttributtes = BaseAttributtes.Commando;

                break;
            case "Soldier":
                baseAttributtes = BaseAttributtes.Soldier;

                break;
            case "Assassin":
                baseAttributtes = BaseAttributtes.Assassin;

                break;
            case "Scout":
                baseAttributtes = BaseAttributtes.Scout;
                break;
        }
        this.name = name;
        this.playerClass = playerClass;
        currentLevel = 0;
        experienceToNextLevel = 100;
        currentExperience = 0;
        skillPoints = 0;

        current = this;
    }

    public void LevelUp()
    {
        if (currentExperience < experienceToNextLevel)
            return;
        currentLevel++;
        experienceToNextLevel = 100 + currentLevel * currentLevel * 10;
        currentExperience -= experienceToNextLevel;
        skillPoints += 1;
        baseAttributtes.changeAttributtePoints(3);
    }
}
