using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to create scriptable object that contains map data (tiles). 
/// Tiles should be created like matrix elements (empty tile included).
/// Matrix width and height are mandatory.
/// Every subsequently created element should have ID increased by one, since I'm using array of MapTileData to create a matrix for the map.
/// </summary>
[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 1)]
public class ScriptableObjectMapData : ScriptableObject
{
    public MapData mapData;
}
