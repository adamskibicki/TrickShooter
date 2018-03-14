using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameMenuScript : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject PauseGameCanvas;
    public GameObject PauseOptionsCanvas;

    public GameObject[] PauseMenu;
    public GameObject[] QuitChoiceMenu;
    public Slider volumeSlider;

    void Start()
    {
        Time.timeScale = 1f;
    }

    enum ButtonYesClickAction
    {
        Exit,
        Menu
    }

    ButtonYesClickAction buttonYes;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        UICanvas.SetActive(false);
        PauseGameCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UICanvas.SetActive(true);
        PauseGameCanvas.SetActive(false);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ActionYesButton()
    {
        if (buttonYes == ButtonYesClickAction.Exit)
            Application.Quit();
        if (buttonYes == ButtonYesClickAction.Menu)
            SceneManager.LoadScene("scene_menu");
    }

    public void EnablePauseMenu()
    {
        foreach (GameObject item in QuitChoiceMenu)
        {
            item.SetActive(false);
        }
        PauseOptionsCanvas.SetActive(false);
        foreach (GameObject item in PauseMenu)
        {
            item.SetActive(true);
        }
    }

    public void EnableYesNoMenu(string action)
    {
        foreach (GameObject item in QuitChoiceMenu)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in PauseMenu)
        {
            item.SetActive(false);
        }
        if(action == "Exit")
        {
            buttonYes = ButtonYesClickAction.Exit;
            (QuitChoiceMenu[0]).GetComponent<Text>().text = "Are you sure you want exit game?\nAll progress in current level will be lost!";
        }
        else if (action == "Menu")
        {
            buttonYes = ButtonYesClickAction.Menu;
            (QuitChoiceMenu[0]).GetComponent<Text>().text = "Are you sure you want go back to menu?\nAll progress in current level will be lost!";
        }
    }

    public void EnablePauseOptionsMenu()
    {
        foreach (GameObject item in PauseMenu)
        {
            item.SetActive(false);
        }
        PauseOptionsCanvas.SetActive(true);
        volumeSlider.value = AudioListener.volume;

    }


    public void VolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
