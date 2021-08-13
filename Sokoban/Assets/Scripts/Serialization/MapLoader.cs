using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for loading maps from JSON
public class MapLoader : MonoBehaviour
{  
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MapData LoadMapFromJSON(int mapIndex)
    {
        //string jsonAddress = Application.persistentDataPath + "/FirstMapData.json";
        string jsonAddress = Application.persistentDataPath + "/SecondMapData.json";
        //TODO: switch by index

        string jsonData = System.IO.File.ReadAllText(jsonAddress);
        return JsonUtility.FromJson<MapData>(jsonData);
    }
}
