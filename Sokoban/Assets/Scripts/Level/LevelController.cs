using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelDataCollection levelDataCollection;

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
        //if (instance != null && instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    instance = this;
        //}
        DontDestroyOnLoad(gameObject);
        instance = this;

        //if (!levelStatistics.LevelCollectionJSONExists())
        //{
        //    levelStatistics.CreateInitialLevelCollection(levelDataCollection);
        //}

        if (levelStatistics.LevelCollectionJSONExists())
        {
            loadedLevelDataCollection = levelStatistics.LoadLevelCollection();
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

    public void CreateInitialLevelCollectionFromMaps(LevelDataCollection levelCollection)
    {
        if (!levelStatistics.LevelCollectionJSONExists())
        {
            levelStatistics.CreateInitialLevelCollection(levelCollection);
            loadedLevelDataCollection = levelStatistics.LoadLevelCollection();
        }
    }

    public void UpdateLevelStatistics(int levelID, bool isCompleted, int lastTime)
    {
        levelStatistics.UpdateLevelCollection(levelID, isCompleted, lastTime);
    }

    public void UpdateLevelStatisticsOnResetLevel(int levelID)
    {
        levelStatistics.UpdateTimesPlayedOnly(levelID);
    }

    public bool CanLoadNewLevel()
    {
        return levelStatistics.GetNumberOfLevels() - 1 >= selectedLevel;
    }

    public int GetBestLevelTime(int levelID)
    {
        return loadedLevelDataCollection.LevelsData[levelID].BestCompletedTimeInSeconds;
    }
}


public static class GameFilePaths
{
    public static string LevelStatisticsFileName => Application.persistentDataPath + "/LevelStatistics.json";

    public static string MapFileName(int mapID)
    {
        return Application.persistentDataPath + string.Format("/MapData{0}.json", mapID);
    }
}