using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseAttributtes
{
    public static BaseAttributtes Soldier = new BaseAttributtes(15, 10, 10, 10);
    public static BaseAttributtes Scout = new BaseAttributtes(10, 15, 10, 10);
    public static BaseAttributtes Assassin = new BaseAttributtes(10, 10, 15, 10);
    public static BaseAttributtes Commando = new BaseAttributtes(10, 10, 10, 15);

    public int attributtePoints { get; private set; }

    //toughness -> life points
    public int toughness { get; private set; }

    //mobility -> movement speed
    public int mobility { get; private set; }

    //technology -> technological skills cooldown, strength and duration (carpet bombing etc.)
    public int technology { get; private set; }

    //mentalStrength -> physical skills cooldown, strength and duration (rage etc.)
    public int mentalStrength { get; private set; }

    public BaseAttributtes(int toughness, int mobility, int technology, int mentalStrength)
    {
        this.toughness = toughness;
        this.mobility = mobility;
        this.technology = technology;
        this.mentalStrength = mentalStrength;
        attributtePoints = 30;
    }

    public void changeAttributtePoints(int amount)
    {
        attributtePoints += amount;
    }

    public void increaseToughness(int amount)
    {
        if (amount > attributtePoints || amount <= 0)
        {
            return;
        }
        attributtePoints -= amount;

        toughness += amount;
    }

    public void increaseMobility(int amount)
    {
        if (amount > attributtePoints || amount <= 0)
        {
            return;
        }
        attributtePoints -= amount;

        mobility += amount;
    }

    public void increaseTechnology(int amount)
    {
        if (amount > attributtePoints || amount <= 0)
        {
            return;
        }
        attributtePoints -= amount;

        technology += amount;
    }

    public void increaseMentalStrength(int amount)
    {
        if (amount > attributtePoints || amount <= 0)
        {
            return;
        }
        attributtePoints -= amount;

        mentalStrength += amount;
    }
}

