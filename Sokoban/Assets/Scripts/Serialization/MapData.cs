using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for holding map data used in creation.
/// </summary>
[System.Serializable]
public class MapData
{
    public int mapWidth;
    public int mapHeight;
    public List<MapTileData> mapTilesData;
}
