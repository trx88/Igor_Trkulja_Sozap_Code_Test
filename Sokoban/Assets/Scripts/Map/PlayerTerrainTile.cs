using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrainTile : MapTile, IMoveableTile
{
    private int currentTileID;
    public int CurrentTileID
    {
        get => currentTileID;
        set
        {
            currentTileID = value;
        }
    }

    public void PrepareTile(MapTileData tileData)
    {
        throw new System.NotImplementedException();
    }

    public void SayHello()
    {
        Debug.Log("I'm a movable tile");
    }

    public MapTile SpawnTile(int positionX, int positionY)
    {
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
