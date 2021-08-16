using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for loading maps from JSON by MapController
/// </summary>
public class MapLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Load the desired map from corresponding JSON file.
    /// </summary>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public MapData LoadMapFromJSON(int mapIndex)
    {
        string FileAddress = GameFilePaths.MapFileName(mapIndex);
        string jsonData = System.IO.File.ReadAllText(FileAddress);
        return JsonUtility.FromJson<MapData>(jsonData);
    }
}
