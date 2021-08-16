using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //private PlayerSettings playerSettings;

    //public LevelController levelController;
    public DropdownController dropdownController;
    public Dropdown dropdownLevelSelector;
    public Text TextBestTime;
    public Slider sliderMusic;
    public Slider sliderEffects;

    //private void Awake()
    //{
    //    playerSettings = GetComponent<PlayerSettings>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //levelDataCollection = LevelController.Instance.LoadLevelStatistics();
        TrySetSlidersFromPlayerPrefs();

        SetDropDownValues(LevelController.Instance.LoadedLevelDataCollection);

        SetBestTimeUIValue(LevelController.Instance.GetBestLevelTime(dropdownLevelSelector.value));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelectorValueChanged()
    {
        SetBestTimeUIValue(LevelController.Instance.GetBestLevelTime(dropdownLevelSelector.value));
    }

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

    void SetBestTimeUIValue(int bestLevelTime)
    {
        if(bestLevelTime < 0)
        {
            TextBestTime.text = "Best time: 00:00";
        }
        else
        {
            TextBestTime.text = "Best time: " + 
                new TimeSpan(0, 0, bestLevelTime).ToString(@"mm\:ss");
        }
    }

    public void PlayLevel()
    {
        LevelController.Instance.SelectedLevel = dropdownLevelSelector.value;
        SceneManager.LoadScene(1);
    }

    void TrySetSlidersFromPlayerPrefs()
    {
        sliderMusic.value = PlayerSettings.Instance.GetMusicLevel();
        sliderEffects.value = PlayerSettings.Instance.GetEffectsLevel();
    }

    public void OnSliderMusicChange()
    {
        PlayerSettings.Instance.SetMusicLevel(sliderMusic.value);
    }

    public void OnSliderEffectsChange()
    {
        PlayerSettings.Instance.SetMusicLevel(sliderEffects.value);
    }
}
