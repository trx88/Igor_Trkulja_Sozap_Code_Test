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

        TextTest.text = string.Format("{0}\n{1}", Input.mousePosition, Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString());
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(0);
    }
}
