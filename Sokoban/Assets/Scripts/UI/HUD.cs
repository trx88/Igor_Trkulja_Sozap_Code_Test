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
    public Button ButtonReset;
    public Text TextTest;

    // Start is called before the first frame update
    void Start()
    {
        secondsPlaying = 0;
        timer = 0.0f;
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
            timer += Time.deltaTime;
            secondsPlaying = timer % 60;
            TimeSpan timeInGame = new TimeSpan(0, 0, (int)secondsPlaying);
            TextTimer.text = timeInGame.ToString(@"mm\:ss");
            yield return null;
        }
    }

    public void ResetCurrentLevel()
    {
        StopCoroutine(TimerCoroutine());
        TimeSpan timeInGame = new TimeSpan(0, 0, 0);
        TextTimer.text = timeInGame.ToString(@"mm\:ss");
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
