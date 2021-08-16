using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for creating maps. Will be empty when all maps are created.
[System.Serializable]
public class MapData
{
    public int mapWidth;
    public int mapHeight;
    public List<MapTileData> mapTilesData;
}
