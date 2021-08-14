using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string LevelName;

    public int NumbersPlayed;

    public bool IsCompleted;

    public int BestCompletedTimeInSeconds;

    public string MapJSONName;
}
