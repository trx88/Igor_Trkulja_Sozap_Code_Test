using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatistics
{
    private LevelDataCollection levelStatisticsData;

    //public LevelDataCollection LevelStatisticsData
    //{
    //    get => levelStatisticsData;
    //}

    public bool LevelCollectionJSONExists()
    {
        return System.IO.File.Exists(GameFilePaths.LevelStatisticsFileName);
    }

    public void CreateInitialLevelCollection(LevelDataCollection initialLevelData)
    {
        string levelCollection = JsonUtility.ToJson(initialLevelData);
        System.IO.File.WriteAllText(GameFilePaths.LevelStatisticsFileName, levelCollection);
    }

    public LevelDataCollection LoadLevelCollection()
    {
        string levelCollectionJSON = System.IO.File.ReadAllText(GameFilePaths.LevelStatisticsFileName);
        levelStatisticsData = JsonUtility.FromJson<LevelDataCollection>(levelCollectionJSON);
        return levelStatisticsData;
    }

    public void SaveLevelCollection(LevelDataCollection levelCollectionToSave)
    {
        string levelCollection = JsonUtility.ToJson(levelCollectionToSave);
        System.IO.File.WriteAllText(GameFilePaths.LevelStatisticsFileName, levelCollection);
    }

    public void SaveLevelCollection()
    {
        string levelCollection = JsonUtility.ToJson(levelStatisticsData);
        System.IO.File.WriteAllText(GameFilePaths.LevelStatisticsFileName, levelCollection);
    }

    public void UpdateLevelCollection(int levelID, bool isCompleted, int lastTime)
    {
        if (LevelCollectionJSONExists())
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

            SaveLevelCollection();
        }
    }

    public void UpdateTimesPlayedOnly(int levelID)
    {
        levelStatisticsData.LevelsData[levelID].NumbersPlayed++;

        SaveLevelCollection();
    }

    public int GetNumberOfLevels()
    {
        return levelStatisticsData.LevelsData.Count;
    }
}
