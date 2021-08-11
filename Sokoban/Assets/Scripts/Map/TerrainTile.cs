using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MapTile, ITerrainTile
{
    public void PrepareTile(MapTileData tileData)
    {
        
    }

    public void SayHello()
    {
        Debug.Log("I'm a terrain tile");
    }

    public MapTile SpawnTile(int positionX, int positionY)
    {
        PositionX = positionX;
        PositionY = positionY;
        BoundryX = positionX + 1;
        BoundryY = positionY - 1;
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
