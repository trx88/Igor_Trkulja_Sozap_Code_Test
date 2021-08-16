using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text TextTimer;
    public Button ButtonNext;

    private int currentLevelID;
    private int secondsInLevel;

    // Start is called before the first frame update
    void Start()
    {
        TextTimer.text = "Elapsed time: " + new TimeSpan(0,0,0).ToString(@"mm\:ss");
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
        TextTimer.text = "Elapsed time: " + timeInLevel.ToString(@"mm\:ss");
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
            ButtonNext.GetComponentInChildren<Text>().enabled = true;
            ButtonNext.image.enabled = true;
            ButtonNext.enabled = true;
        }
        else
        {
            ButtonNext.GetComponentInChildren<Text>().enabled = false;
            ButtonNext.image.enabled = false;
            ButtonNext.enabled = false;
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelController.Instance.SelectedLevel);//See what happens with level controller
    }

    public void ResetCurrentLevel()
    {
        LevelController.Instance.SelectedLevel = currentLevelID;
        LevelController.Instance.UpdateLevelStatisticsOnResetLevel(currentLevelID);
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        LevelController.Instance.UpdateLevelStatisticsOnResetLevel(currentLevelID);
        SceneManager.LoadScene(0);
    }
}
