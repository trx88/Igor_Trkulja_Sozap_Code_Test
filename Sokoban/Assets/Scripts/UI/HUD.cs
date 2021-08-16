using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the HUD UI elements.
/// </summary>
public class HUD : MonoBehaviour
{
    public Text textTimer;
    public Button buttonNext;

    private int currentLevelID;
    private int secondsInLevel;

    // Start is called before the first frame update
    void Start()
    {
        textTimer.text = "Elapsed time: " + new TimeSpan(0,0,0).ToString(@"mm\:ss");
        //Events sent by MapController.
        MapController.OnNotifyUIAboutTime += UpdateTimer;
        MapController.OnLevelCompleted += LevelCompleted;
        ToggleButtonNext(false);
        currentLevelID = LevelController.Instance.SelectedLevel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        MapController.OnNotifyUIAboutTime -= UpdateTimer;
        MapController.OnLevelCompleted -= LevelCompleted;
    }

    /// <summary>
    /// Updates the timer for level time. Avoids using MonoBehaviour Update method.
    /// </summary>
    /// <param name="seconds"></param>
    private void UpdateTimer(int seconds)
    {
        secondsInLevel = seconds;
        TimeSpan timeInLevel = new TimeSpan(0, 0, (int)secondsInLevel);
        textTimer.text = "Elapsed time: " + timeInLevel.ToString(@"mm\:ss");
    }

    /// <summary>
    /// Toggles next level button (if level is completed and next level is available).
    /// </summary>
    /// <param name="nextLevelID"></param>
    private void LevelCompleted(int nextLevelID)
    {
        if(LevelController.Instance.CanLoadNewLevel())
        {
            ToggleButtonNext(true);
        }
        else
        {
            ToggleButtonNext(false);
        }
    }

    /// <summary>
    /// Toggles next level button (if level is completed and next level is available).
    /// </summary>
    private void ToggleButtonNext(bool showButton)
    {
        if(showButton)
        {
            buttonNext.GetComponentInChildren<Text>().enabled = true;
            buttonNext.image.enabled = true;
            buttonNext.enabled = true;
        }
        else
        {
            buttonNext.GetComponentInChildren<Text>().enabled = false;
            buttonNext.image.enabled = false;
            buttonNext.enabled = false;
        }
    }

    /// <summary>
    /// Next level button binding.
    /// </summary>
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelController.Instance.SelectedLevel);//See what happens with level controller
    }

    /// <summary>
    /// Reset level button binding.
    /// </summary>
    public void ResetCurrentLevel()
    {
        LevelController.Instance.SelectedLevel = currentLevelID;
        LevelController.Instance.UpdateLevelStatisticsOnResetLevelOrOnMainMenu(currentLevelID);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Main menu button binding.
    /// </summary>
    public void ToMainMenu()
    {
        LevelController.Instance.UpdateLevelStatisticsOnResetLevelOrOnMainMenu(currentLevelID);
        SceneManager.LoadScene(0);
    }
}
