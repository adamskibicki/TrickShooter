using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AttributtePointsScript : MonoBehaviour
{
    public List<Text> NumberTexts;

    public Text attributtePointsText;
    public Text attributeInfoText;

    public GameObject confirmYesButton;
    public GameObject confirmNoButton;


    int toughness;
    int mobility;
    int technology;
    int mentalStrength;

    int toughnessChange;
    int mobilityChange;
    int technologyChange;
    int mentalStrengthChange;

    int attributtePoints;

    public void OnActivate()
    {
        toughness = Profile.current.baseAttributtes.toughness;
        mobility = Profile.current.baseAttributtes.mobility;
        technology = Profile.current.baseAttributtes.technology;
        mentalStrength = Profile.current.baseAttributtes.mentalStrength;
        attributtePoints = Profile.current.baseAttributtes.attributtePoints;

        NumberTexts[0].text = Convert.ToString(toughness);
        NumberTexts[1].text = Convert.ToString(mobility);
        NumberTexts[2].text = Convert.ToString(technology);
        NumberTexts[3].text = Convert.ToString(mentalStrength);

        attributtePointsText.text = Convert.ToString(Profile.current.baseAttributtes.attributtePoints);

        attributtePoints = Profile.current.baseAttributtes.attributtePoints;

        toughnessChange = 0;
        mobilityChange = 0;
        technologyChange = 0;
        mentalStrengthChange = 0;
    }

    void LateUpdate()
    {
        NumberTexts[0].text = Convert.ToString(toughness);
        NumberTexts[1].text = Convert.ToString(mobility);
        NumberTexts[2].text = Convert.ToString(technology);
        NumberTexts[3].text = Convert.ToString(mentalStrength);

        attributtePointsText.text = "Attributte Points: " + Convert.ToString(attributtePoints);

    }

    public void incrementToughness()
    {
        if (attributtePoints > 0)
        {
            toughness++;
            toughnessChange++;
            attributtePoints--;
        }
    }

    public void decrementToughness()
    {
        if (toughnessChange > 0)
        {
            toughness--;
            toughnessChange--;
            attributtePoints++;
        }
    }

    public void incrementMobility()
    {
        if (attributtePoints > 0)
        {
            mobility++;
            mobilityChange++;
            attributtePoints--;
        }
    }

    public void decrementMobility()
    {
        if (mobilityChange > 0)
        {
            mobility--;
            mobilityChange--;
            attributtePoints++;
        }
    }

    public void incrementTechnology()
    {
        if (attributtePoints > 0)
        {
            technology++;
            technologyChange++;
            attributtePoints--;
        }
    }

    public void decrementTechnology()
    {
        if (technologyChange > 0)
        {
            technology--;
            technologyChange--;
            attributtePoints++;
        }
    }

    public void incrementMentalStrength()
    {
        if (attributtePoints > 0)
        {
            mentalStrength++;
            mentalStrengthChange++;
            attributtePoints--;
        }
    }

    public void decrementMentalStrength()
    {
        if (mentalStrengthChange > 0)
        {
            mentalStrength--;
            mentalStrengthChange--;
            attributtePoints++;
        }
    }

    public void Confirm()
    {
        Profile.current.baseAttributtes.increaseToughness(toughness - Profile.current.baseAttributtes.toughness);
        Profile.current.baseAttributtes.increaseMobility(mobility - Profile.current.baseAttributtes.mobility);
        Profile.current.baseAttributtes.increaseTechnology(technology - Profile.current.baseAttributtes.technology);
        Profile.current.baseAttributtes.increaseMentalStrength(mentalStrength - Profile.current.baseAttributtes.mentalStrength);

        toughnessChange = 0;
        mobilityChange = 0;
        technologyChange = 0;
        mentalStrengthChange = 0;
    }

    public void ShowConfirmationButtonsAndText()
    {
        confirmYesButton.SetActive(true);
        confirmNoButton.SetActive(true);
        attributeInfoText.text = "Do you want confirm your attributte points choice?";
    }

    public void ConfirmYesButtonAction()
    {
        Confirm();
        confirmYesButton.SetActive(false);
        confirmNoButton.SetActive(false);
        attributeInfoText.text = "";
    }

    public void ConfirmNoButtonAction()
    {
        confirmYesButton.SetActive(false);
        confirmNoButton.SetActive(false);
        attributeInfoText.text = "";
    }

    public void ShowAttributteInfo(string attributte)
    {
        switch(attributte)
        {
            case "Toughness":
                attributeInfoText.text = "Number of life points and regeneration depends on toughness.";

                break;
            case "Mobility":
                attributeInfoText.text = "Hgher mobility will increase your movement speed.";

                break;
            case "Technology":
                attributeInfoText.text = "Higher technology will increase damage, duration and reduce cooldown of your technology skills.";

                break;
            case "MentalStrength":
                attributeInfoText.text = "Higher Mental Strength will increase effect, duration and reduce cooldown of your mental skills.";

                break;
        }
    }
}
