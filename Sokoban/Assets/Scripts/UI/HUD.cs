using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    float secondsPlaying;
    float timer = 0.0f;
    public Text TextTimer;
    public Button ButtonNext;

    private int currentLevelID;

    // Start is called before the first frame update
    void Start()
    {
        secondsPlaying = 0;
        timer = 0.0f;
        //StartCoroutine(TimerCoroutine());
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
        TimeSpan timeInLevel = new TimeSpan(0, 0, (int)seconds);
        TextTimer.text = "Elapsed time: " + timeInLevel.ToString(@"mm\:ss");
    }

    private void LevelCompleted(int nextLevelID)
    {
        ToggleButtonNext(true);
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
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
