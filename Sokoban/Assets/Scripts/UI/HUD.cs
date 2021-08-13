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
        StartCoroutine(TimerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TimerCoroutine()
    {
        while (true)//TODO: Exchange with isLevelOver
        {
            secondsPlaying = Time.fixedTime;
            TimeSpan timeInGame = new TimeSpan(0, 0, (int)secondsPlaying);
            TextTimer.text = timeInGame.ToString(@"mm\:ss");
            yield return null;
        }
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(0);
    }
}
