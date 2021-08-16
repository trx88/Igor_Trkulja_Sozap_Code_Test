using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of what level should be loaded. Also uses LevelStatistics to update the JSON for level progression.
/// </summary>
public class LevelController : MonoBehaviour
{
    private int selectedLevel;
    public int SelectedLevel
    {
        get => selectedLevel;
        set
        {
            selectedLevel = value;
        }
    }

    private LevelStatistics levelStatistics = new LevelStatistics();

    private LevelDataCollection loadedLevelDataCollection;
    public LevelDataCollection LoadedLevelDataCollection
    {
        get => loadedLevelDataCollection;
    }

    //TODO: Does this need to be a Singleton???
    private static LevelController instance;

    public static LevelController Instance { get { return instance; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;

        if (levelStatistics.LevelStatisticsJSONExists())
        {
            loadedLevelDataCollection = levelStatistics.LoadLevelStatistics();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Checks if there's a statistic already saved, and saves it if it doesn't. 
    /// </summary>
    /// <param name="levelCollection">LevelDataCollection object that contains levels progress</param>
    public void CreateInitialLevelStatisticsFromMaps(LevelDataCollection levelCollection)
    {
        if (!levelStatistics.LevelStatisticsJSONExists())
        {
            levelStatistics.CreateInitialLevelStatistics(levelCollection);
            loadedLevelDataCollection = levelStatistics.LoadLevelStatistics();
        }
    }

    /// <summary>
    /// Updates times level is played, if it's completed and records the best level time (if latest time is the best)
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="isCompleted"></param>
    /// <param name="lastTime">Latest time that level was completed</param>
    public void UpdateLevelStatistics(int levelID, bool isCompleted, int lastTime)
    {
        levelStatistics.UpdateLevelStatistics(levelID, isCompleted, lastTime);
    }

    /// <summary>
    /// Updates only times level has been played (use-cases: on reset level, on main menu)
    /// </summary>
    /// <param name="levelID"></param>
    public void UpdateLevelStatisticsOnResetLevelOrOnMainMenu(int levelID)
    {
        levelStatistics.UpdateTimesPlayedOnly(levelID);
    }

    /// <summary>
    /// Checks if there are any more levels to play.
    /// </summary>
    /// <returns></returns>
    public bool CanLoadNewLevel()
    {
        return levelStatistics.GetNumberOfLevels() - 1 >= selectedLevel;
    }

    /// <summary>
    /// Gets the best time. Used in UI.
    /// </summary>
    /// <param name="levelID"></param>
    /// <returns></returns>
    public int GetBestLevelTime(int levelID)
    {
        return loadedLevelDataCollection.LevelsData[levelID].BestCompletedTimeInSeconds;
    }
}
