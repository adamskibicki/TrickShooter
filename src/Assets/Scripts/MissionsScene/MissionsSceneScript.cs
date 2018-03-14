using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionsSceneScript : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject LoadingCanvas;

    public GameObject MissionChoosePanel;
    public GameObject MissionDetailsPanel;
    public GameObject AttributtesPanel;
    public GameObject SkillsPanel;
    public GameObject EquipmentPanel;
    public GameObject ShopPanel;
    public GameObject EnemiesPanel;
    public GameObject StatisticsPanel;

    public List<Button> MissionButtons;

    public Text MissionCodenameText;
    public Text MissionGoalsText;
    public Text MissionDescriptionText;

    public string currentActivePanel { get; private set; }
    public string previousActivePanel { get; private set; }

    public string currentActiveCanvas { get; private set; }
    public string previousActiveCanvas { get; private set; }

    class mission
    {
        public string codename;
        public string description;
        public string goals;
    }

    #region mission list
    List<mission> MissionsInfo = new List<mission>
    {
        new mission { codename="Can you shoot?", description="Small training before real action.", goals="Show your skills." },
        new mission { codename="First contact", description="Enemy leaders send some scouts. Show them no mercy.", goals="Kil enemy scouts." },
        new mission { codename="Hell's army!", description="An enemy army is attacking your base.", goals="Survive enemy attack." }
    };
    #endregion

    void Start()
    {
        for (int i = 0; i < MissionButtons.Count; i++)
        {
            MissionButtons[i].GetComponentInChildren<Text>().text = string.Format("Mission {0}:\n{1}", i, MissionsInfo[i].codename);
        }
    }

    public void ShowMissionDetails(int number)
    {
        MissionCodenameText.text = MissionsInfo[number].codename;
        MissionDescriptionText.text = MissionsInfo[number].description;
        MissionGoalsText.text ="Goals:\n" + MissionsInfo[number].goals;

        SetActivePanel("MissionDetails");
    }

    public void StartMission()
    {
        SetActiveCanvas("Loading");

        SceneManager.LoadScene("scene_mission_1");
    }

    public void SetActivePanel(string panel)
    {
        MissionChoosePanel.SetActive(false);
        MissionDetailsPanel.SetActive(false);
        AttributtesPanel.SetActive(false);
        SkillsPanel.SetActive(false);
        EquipmentPanel.SetActive(false);
        ShopPanel.SetActive(false);
        EnemiesPanel.SetActive(false);
        StatisticsPanel.SetActive(false);

        switch (panel)
        {
            case "MissionChoose":
                MissionChoosePanel.SetActive(true);

                break;
            case "MissionDetails":
                MissionDetailsPanel.SetActive(true);

                break;
            case "Attributtes":
                AttributtesPanel.GetComponent<AttributtePointsScript>().OnActivate();
                AttributtesPanel.SetActive(true);

                break;
            case "Skills":
                SkillsPanel.SetActive(true);

                break;
            case "Equipment":
                EquipmentPanel.SetActive(true);

                break;
            case "Shop":
                ShopPanel.SetActive(true);

                break;
            case "Enemies":
                EnemiesPanel.SetActive(true);

                break;
            case "Statistics":
                StatisticsPanel.SetActive(true);

                break;
            default:
                return;
        }

        previousActivePanel = currentActivePanel;
        currentActivePanel = panel;
    }

    public void SetActiveCanvas(string canvas)
    {
        MainCanvas.SetActive(false);
        LoadingCanvas.SetActive(false);
        switch (canvas)
        {
            case "Main":
                MainCanvas.SetActive(true);

                break;
            case "Loading":
                LoadingCanvas.SetActive(true);

                break;
            default:
                return;
        }

        previousActiveCanvas = currentActiveCanvas;
        currentActiveCanvas = canvas;
    }

    public void GoBackToMenu()
    {
        SetActiveCanvas("Loading");
        SceneManager.LoadScene("scene_menu");
    }
}
