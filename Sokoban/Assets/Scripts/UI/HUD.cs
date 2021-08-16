using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void UpdateTimer(int seconds)
    {
        secondsInLevel = seconds;
        TimeSpan timeInLevel = new TimeSpan(0, 0, (int)secondsInLevel);
        textTimer.text = "Elapsed time: " + timeInLevel.ToString(@"mm\:ss");
    }

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

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelController.Instance.SelectedLevel);//See what happens with level controller
    }

    public void ResetCurrentLevel()
    {
        LevelController.Instance.SelectedLevel = currentLevelID;
        LevelController.Instance.UpdateLevelStatisticsOnResetLevelOrOnMainMenu(currentLevelID);
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        LevelController.Instance.UpdateLevelStatisticsOnResetLevelOrOnMainMenu(currentLevelID);
        SceneManager.LoadScene(0);
    }
}
