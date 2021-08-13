using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    float secondsPlaying;
    public Text TextTimer;
    public Button ButtonReset;
    public Text TextTest;

    // Start is called before the first frame update
    void Start()
    {
        secondsPlaying = 0;
    }

    // Update is called once per frame
    void Update()
    {
        secondsPlaying = Time.fixedTime;
        TimeSpan timeInGame = new TimeSpan(0, 0, (int)secondsPlaying);
        //TextTimer.text = ((int)secondsPlaying).ToString("");
        TextTimer.text = timeInGame.ToString(@"mm\:ss");

        Vector3 zeroPosition = new Vector3(0, 0, -10);
        Vector3 topLeftPosition = new Vector3(Screen.width * 0.05f, Screen.height * 0.9f, 0);
        TextTest.text = string.Format("{0}\n{1}", topLeftPosition, Camera.main.ScreenToWorldPoint(topLeftPosition).ToString());
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(0);
    }
}
