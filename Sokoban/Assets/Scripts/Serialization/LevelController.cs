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
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!LevelCollectionJSONExists())
        {
            SaveInitialLevelCollection();
        }
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

    public LevelDataCollection LoadLevelCollection()
    {
        string levelCollectionJSON = System.IO.File.ReadAllText(Application.persistentDataPath + "/LevelDataCollection.json");
        return JsonUtility.FromJson<LevelDataCollection>(levelCollectionJSON);
    }
}
