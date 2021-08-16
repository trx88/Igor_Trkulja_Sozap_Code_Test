using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level statistics data
/// </summary>
[System.Serializable]
public class LevelData
{
    public string LevelName;

    public int NumbersPlayed;

    public bool IsCompleted;

    public int BestCompletedTimeInSeconds;

    public int MapID;
}
