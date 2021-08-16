using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the UI elements on the main menu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    public DropdownController dropdownController;
    public Dropdown dropdownLevelSelector;
    public Text textBestTime;
    public Slider sliderMusic;
    public Slider sliderEffects;

    // Start is called before the first frame update
    void Start()
    {
        SetSlidersFromPlayerPrefs();

        SetDropDownValues(LevelController.Instance.LoadedLevelDataCollection);

        SetBestTimeUIValue(LevelController.Instance.GetBestLevelTime(dropdownLevelSelector.value));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Level selection dropdown binding.
    /// </summary>
    public void LevelSelectorValueChanged()
    {
        SetBestTimeUIValue(LevelController.Instance.GetBestLevelTime(dropdownLevelSelector.value));
    }

    /// <summary>
    /// Disables/enables dropdown options, based on isCompleted field in level statistics JSON. 
    /// IMPORTANT: Requirements are saying the only completed levels should be enabled.
    /// However, if player has made it to level 2, but hasn't completed it, he needs to play level 1 again.
    /// So I changed the logic to enable all completed levels + 1. 
    /// This shouldn't be Ghosts & Goblins. :D
    /// </summary>
    /// <param name="levelDataCollection">Loaded levels statistics from JSON</param>
    void SetDropDownValues(LevelDataCollection levelDataCollection)
    {
        for (int disableIndex = 1; disableIndex < 5; disableIndex++)
        {
            dropdownController.indexesToDisable.Add(disableIndex);
        }

        for (int levelIndex = 0; levelIndex < levelDataCollection.LevelsData.Count - 1; levelIndex++)
        {
            if (levelDataCollection.LevelsData[levelIndex].IsCompleted)
            {
                dropdownController.indexesToDisable.Remove(levelIndex + 1);
            }
        }
    }

    /// <summary>
    /// Sets the best time for selected level.
    /// </summary>
    /// <param name="bestLevelTime"></param>
    void SetBestTimeUIValue(int bestLevelTime)
    {
        //Default value (when level hasn't been completed) is -1, that's why I'm checking for <0.
        if(bestLevelTime < 0)
        {
            textBestTime.text = "Best time: 00:00";
        }
        else
        {
            textBestTime.text = "Best time: " + 
                new TimeSpan(0, 0, bestLevelTime).ToString(@"mm\:ss");
        }
    }

    /// <summary>
    /// Loads selected level. Scene is always the same, but selected level ID in level controller is changing.
    /// </summary>
    public void PlayLevel()
    {
        LevelController.Instance.SelectedLevel = dropdownLevelSelector.value;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Sets the slider values from PlayerPrefs.
    /// </summary>
    void SetSlidersFromPlayerPrefs()
    {
        sliderMusic.value = PlayerSettings.Instance.GetMusicLevel();
        sliderEffects.value = PlayerSettings.Instance.GetEffectsLevel();
    }

    /// <summary>
    /// Slide value change binding
    /// </summary>
    public void OnSliderMusicChange()
    {
        PlayerSettings.Instance.SetMusicLevel(sliderMusic.value);
    }

    /// <summary>
    /// Slide value change binding
    /// </summary>
    public void OnSliderEffectsChange()
    {
        PlayerSettings.Instance.SetEffectsLevel(sliderEffects.value);
    }

    /// <summary>
    /// Quit button binding
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
