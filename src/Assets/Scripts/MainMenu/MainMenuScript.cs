using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject LoadingCanvas;
    public GameObject OptionsCanvas;
    public GameObject ProfileChooseCanvas;
    public GameObject ProfileCreateCanvas;
    public GameObject YesNoCanvas;

    public Text YesNoInfoText;

    public Slider volumeSlider;

    public List<Button> ProfileButtons;
    public List<GameObject> DeleteButtons;

    public Dropdown ClassDropdown;
    public Text ClassAttributtesText;
    public InputField NameInput;

    public static int currentProfileIndex { get; private set; }
    public static string currentEnabledCanvas { get; private set; }
    public static string previousEnabledCanvas { get; private set; }

    void LoadProfiles()
    {
        SaveLoad.Load();

        for (int i = 0; i < 3; i++)
        {
            if (SaveLoad.savedProfiles[i] == null)
            {
                ProfileButtons[i].GetComponentInChildren<Text>().text = "Empty slot";
                DeleteButtons[i].SetActive(false);
            }
            else
            {
                ProfileButtons[i].GetComponentInChildren<Text>().text = CreateProfileDescription(i);
                DeleteButtons[i].SetActive(true);
            }
        }
    }

    string yesButtonAction = string.Empty;
    public void YesButtonMakeAction()
    {
        if (yesButtonAction == "Abort")
        {
            ClassDropdown.value = 0;

            NameInput.text = "Profile name...";

            SetEnabledCanvas("ProfileChoose");
        }
        else if (yesButtonAction == "Create")
        {
            CreateProfile();

            ClassDropdown.value = 0;

            NameInput.text = "Profile name...";

            SetEnabledCanvas("Loading");

            SceneManager.LoadScene("scene_missions");
        }
        else if(yesButtonAction == "Delete")
        {
            SaveLoad.savedProfiles[profileIndexToDelete] = null;

            SaveLoad.Save(profileIndexToDelete);

            LoadProfiles();

            SetEnabledCanvas("ProfileChoose");
        }
    }

    public void NoButtonMakeAction()
    {
        SetEnabledCanvas(previousEnabledCanvas);
    }

    public void AbortButtonMakeAction()
    {
        yesButtonAction = "Abort";

        YesNoInfoText.text = "Are yo sure you want abort creaing profile?";

        SetEnabledCanvas("YesNo");
    }

    public void CreateButtonMakeAction()
    {
        yesButtonAction = "Create";

        YesNoInfoText.text = "Name: " + NameInput.text + "\nClass: " + ClassDropdown.options[ClassDropdown.value].text + "\n" + ClassAttributtesText.text;

        SetEnabledCanvas("YesNo");
    }

    int profileIndexToDelete;
    public void DeleteButtonMakeAction(int profileIndex)
    {
        yesButtonAction = "Delete";

        YesNoInfoText.text = "Are you sure you want delete '" + SaveLoad.savedProfiles[profileIndex].name + "' profile?";

        profileIndexToDelete = profileIndex;

        SetEnabledCanvas("YesNo");
    }

    void Start()
    {
        //TODO: save sound value in binary file
        volumeSlider.maxValue = 1f;
        volumeSlider.value = AudioListener.volume;

        ClassDropdownOnValueChanged();

        Profile.current = null;

        LoadProfiles();
    }

    string CreateProfileDescription(int profileIndex)
    {
        string description = string.Empty;

        description = string.Format("Name: {0}\nClass: {1}\nLevel: {2}\nExperience: {3}/{4}\nToughness: {5}\nMobility: {6}\nTechnology: {7}\nMental strength: {8}", SaveLoad.savedProfiles[profileIndex].name,
            SaveLoad.savedProfiles[profileIndex].playerClass,
            SaveLoad.savedProfiles[profileIndex].currentLevel,
            SaveLoad.savedProfiles[profileIndex].currentExperience,
            SaveLoad.savedProfiles[profileIndex].experienceToNextLevel,
            SaveLoad.savedProfiles[profileIndex].baseAttributtes.toughness,
            SaveLoad.savedProfiles[profileIndex].baseAttributtes.mobility,
            SaveLoad.savedProfiles[profileIndex].baseAttributtes.technology,
            SaveLoad.savedProfiles[profileIndex].baseAttributtes.mentalStrength);

        return description;
    }

    public void StartGame(int profileIndex)
    {
        currentProfileIndex = profileIndex;

        if (SaveLoad.savedProfiles[currentProfileIndex] == null)
        {
            SetEnabledCanvas("ProfileCreate");
            return;
        }

        Profile.current = SaveLoad.savedProfiles[currentProfileIndex];

        SetEnabledCanvas("Loading");

        SceneManager.LoadScene("scene_missions");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && MainMenuCanvas.activeInHierarchy == true)
        {
            Application.Quit();
        }
    }

    public void VolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void ClassDropdownOnValueChanged()
    {
        switch (ClassDropdown.value)
        {
            case 0:               
                ClassAttributtesText.text = string.Format("Base class attributtes:\nToughness: {0}\nMobility: {1}\nTechnology: {2}\nMental Strength: {3}", BaseAttributtes.Soldier.toughness, BaseAttributtes.Soldier.mobility, BaseAttributtes.Soldier.technology, BaseAttributtes.Soldier.mentalStrength);
                break;
            case 1:
                ClassAttributtesText.text = string.Format("Base class attributtes:\nToughness: {0}\nMobility: {1}\nTechnology: {2}\nMental Strength: {3}", BaseAttributtes.Scout.toughness, BaseAttributtes.Scout.mobility, BaseAttributtes.Scout.technology, BaseAttributtes.Scout.mentalStrength);
                break;
            case 2:
                ClassAttributtesText.text = string.Format("Base class attributtes:\nToughness: {0}\nMobility: {1}\nTechnology: {2}\nMental Strength: {3}", BaseAttributtes.Assassin.toughness, BaseAttributtes.Assassin.mobility, BaseAttributtes.Assassin.technology, BaseAttributtes.Assassin.mentalStrength);
                break;
            case 3:               
                ClassAttributtesText.text = string.Format("Base class attributtes:\nToughness: {0}\nMobility: {1}\nTechnology: {2}\nMental Strength: {3}", BaseAttributtes.Commando.toughness, BaseAttributtes.Commando.mobility, BaseAttributtes.Commando.technology, BaseAttributtes.Commando.mentalStrength);
                break;
        }
    }

    public void CreateProfile()
    {
        switch (ClassDropdown.value)
        {
            case 0:
                Profile.current = new Profile(NameInput.text, "Soldier");
                break;
            case 1:
                Profile.current = new Profile(NameInput.text, "Scout");
                break;
            case 2:
                Profile.current = new Profile(NameInput.text, "Commando");
                break;
            case 3:
                Profile.current = new Profile(NameInput.text, "Assassin");
                break;
        }

        SaveLoad.Save(currentProfileIndex);
    }

    public void SetEnabledCanvas(string canvas)
    {
        MainMenuCanvas.SetActive(false);
        LoadingCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        ProfileChooseCanvas.SetActive(false);
        ProfileCreateCanvas.SetActive(false);
        YesNoCanvas.SetActive(false);

        switch (canvas)
        {
            case "MainMenu":
                MainMenuCanvas.SetActive(true);
                break;
            case "Loading":
                LoadingCanvas.SetActive(true);
                break;
            case "Options":
                OptionsCanvas.SetActive(true);
                break;
            case "ProfileChoose":
                ProfileChooseCanvas.SetActive(true);
                break;
            case "ProfileCreate":
                ProfileCreateCanvas.SetActive(true);
                break;
            case "YesNo":
                YesNoCanvas.SetActive(true);
                break;
        }

        previousEnabledCanvas = currentEnabledCanvas;
        currentEnabledCanvas = canvas;
    }
}
