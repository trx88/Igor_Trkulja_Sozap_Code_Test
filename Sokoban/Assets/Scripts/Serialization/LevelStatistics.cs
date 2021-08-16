using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles level statistics JSON file operations.
/// </summary>
public class LevelStatistics
{
    private LevelDataCollection levelStatisticsData;

    /// <summary>
    /// Checks if level statistics file exist.
    /// </summary>
    /// <returns></returns>
    public bool LevelStatisticsJSONExists()
    {
        return System.IO.File.Exists(GameFilePaths.LevelStatisticsFileName);
    }

    /// <summary>
    /// Saves the initial level statistics file. Can be written better. 
    /// </summary>
    /// <param name="initialLevelData"></param>
    public void CreateInitialLevelStatistics(LevelDataCollection initialLevelData)
    {
        string levelCollection = JsonUtility.ToJson(initialLevelData);
        System.IO.File.WriteAllText(GameFilePaths.LevelStatisticsFileName, levelCollection);
    }

    /// <summary>
    /// Loads the level statistics.
    /// </summary>
    /// <returns></returns>
    public LevelDataCollection LoadLevelStatistics()
    {
        string levelCollectionJSON = System.IO.File.ReadAllText(GameFilePaths.LevelStatisticsFileName);
        levelStatisticsData = JsonUtility.FromJson<LevelDataCollection>(levelCollectionJSON);
        return levelStatisticsData;
    }

    /// <summary>
    /// Saves changes to level statistics file. Used on private variable when updating statistics (e.g. level completed, etc.)
    /// </summary>
    public void SaveLevelStatistics()
    {
        string levelCollection = JsonUtility.ToJson(levelStatisticsData);
        System.IO.File.WriteAllText(GameFilePaths.LevelStatisticsFileName, levelCollection);
    }

    /// <summary>
    /// Updates statistics (e.g. level completed, etc.)
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="isCompleted"></param>
    /// <param name="lastTime"></param>
    public void UpdateLevelStatistics(int levelID, bool isCompleted, int lastTime)
    {
        if (LevelStatisticsJSONExists())
        {
            levelStatisticsData.LevelsData[levelID].NumbersPlayed++;
            if (!levelStatisticsData.LevelsData[levelID].IsCompleted)//If it's already completed, don't change this bool
            {
                levelStatisticsData.LevelsData[levelID].IsCompleted = isCompleted;
            }

            if (levelStatisticsData.LevelsData[levelID].BestCompletedTimeInSeconds > -1)
            {
                TimeSpan bestTimeSoFar = new TimeSpan(0, 0, levelStatisticsData.LevelsData[levelID].BestCompletedTimeInSeconds);
                TimeSpan latestTime = new TimeSpan(0, 0, lastTime);
                if (latestTime < bestTimeSoFar)
                {
                    levelStatisticsData.LevelsData[levelID].BestCompletedTimeInSeconds = lastTime;
                }
            }
            else
            {
                levelStatisticsData.LevelsData[levelID].BestCompletedTimeInSeconds = lastTime;
            }

            SaveLevelStatistics();
        }
    }

    /// <summary>
    /// Updates only times played (e.g. reset level, main menu, etc.)
    /// </summary>
    /// <param name="levelID"></param>
    public void UpdateTimesPlayedOnly(int levelID)
    {
        levelStatisticsData.LevelsData[levelID].NumbersPlayed++;

        SaveLevelStatistics();
    }

    /// <summary>
    /// Gets the total number of levels
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfLevels()
    {
        return levelStatisticsData.LevelsData.Count;
    }
}
