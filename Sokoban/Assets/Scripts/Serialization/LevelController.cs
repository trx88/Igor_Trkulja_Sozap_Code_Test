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

        if (!LevelCollectionJSONExists())
        {
            SaveInitialLevelCollection();
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

    bool LevelCollectionJSONExists()
    {
        return System.IO.File.Exists(Application.persistentDataPath + "/LevelDataCollection.json");
    }

    void SaveInitialLevelCollection()
    {
        string levelCollection = JsonUtility.ToJson(levelDataCollection);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/LevelDataCollection.json", levelCollection);
    }

    public void SaveLevelCollection(LevelDataCollection levelCollectionToSave)
    {
        string levelCollection = JsonUtility.ToJson(levelCollectionToSave);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/LevelDataCollection.json", levelCollection);
    }

    public void UpdateLevelCollection(int levelID, int numberPlayed, bool isCompleted, int lastTime)
    {
        if(LevelCollectionJSONExists())
        {
            LevelDataCollection levelColection = LoadLevelCollection();
            levelColection.LevelsData[levelID].NumbersPlayed++;
            if(!levelColection.LevelsData[levelID].IsCompleted)//If it's already completed, don't change this bool
            {
                levelColection.LevelsData[levelID].IsCompleted = isCompleted;
            }
            TimeSpan bestTimeSoFar = new TimeSpan(0, 0, levelColection.LevelsData[levelID].BestCompletedTimeInSeconds);
            TimeSpan latestTime = new TimeSpan(0, 0, lastTime);
            if(latestTime < bestTimeSoFar)
            {
                levelColection.LevelsData[levelID].BestCompletedTimeInSeconds = lastTime;
            }

            SaveLevelCollection(levelColection);
        }
    }

    public LevelDataCollection LoadLevelCollection()
    {
        string levelCollectionJSON = System.IO.File.ReadAllText(Application.persistentDataPath + "/LevelDataCollection.json");
        return JsonUtility.FromJson<LevelDataCollection>(levelCollectionJSON);
    }
}
