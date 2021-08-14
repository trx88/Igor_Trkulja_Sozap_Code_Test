using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for loading maps from JSON
public class MapLoader : MonoBehaviour
{
    private List<string> MapJSONs = new List<string>()
    {
        "/FirstMapData.json",
        "/SecondMapData.json",
        "/ThirdMapData.json",
        "/ForthMapData.json",
        "/FifthMapData.json"
    };

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
        string jsonAddress = Application.persistentDataPath + MapJSONs[mapIndex];
        //string jsonAddress = Application.persistentDataPath + "/SecondMapData.json";
        //TODO: switch by index

        string jsonData = System.IO.File.ReadAllText(jsonAddress);
        return JsonUtility.FromJson<MapData>(jsonData);
    }
}
