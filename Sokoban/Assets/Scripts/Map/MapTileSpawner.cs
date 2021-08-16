using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileSpawner : MonoBehaviour
{
    #region Publicly exposed properties

    public int boxTileZ = -1;
    public int playerTileZ = -1;
    public int wallTileZ = 0;
    public int grassTileZ = 1;
    public int targetTileZ = 0;

    public MovableTile boxTile;
    public PlayerTerrainTile playerTile;
    public TerrainTile wallTile;
    public TerrainTile grassTile;
    public TerrainTile targetTile;
    public TerrainTile emptyTile;
    #endregion

    //TODO: Does this need to be a Singleton???
    private static MapTileSpawner instance;

    public static MapTileSpawner Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private int playerStartingTileID;
    public int PlayerStartingTileID
    {
        get => playerStartingTileID;
    }

    EnumTileType TileType;

    public MapTile CreateTileFromData(MapTileData tileData, int positionX, int positionY, Transform mapParent)
    {
        TileType = tileData.TileType;

        ITileSpawner spawner = ScriptableObject.CreateInstance<TileSpawner>();

        switch (TileType)
        {
            case EnumTileType.Wall:
                {
                    return spawner.SpawnTile(tileData, wallTile, positionX, positionY, wallTileZ, mapParent);
                }
            case EnumTileType.Grass:
                {
                    return spawner.SpawnTile(tileData, grassTile, positionX, positionY, grassTileZ, mapParent);
                }
            case EnumTileType.Box:
                {
                    spawner.SpawnTile(tileData, grassTile, positionX, positionY, grassTileZ, mapParent);
                    return spawner.SpawnTile(tileData, boxTile, positionX, positionY, boxTileZ, mapParent);
                }
            case EnumTileType.Target:
                {
                    return spawner.SpawnTile(tileData, targetTile, positionX, positionY, targetTileZ, mapParent);
                }
            case EnumTileType.Player:
                {
                    spawner.SpawnTile(tileData, grassTile, positionX, positionY, grassTileZ, mapParent);
                    playerStartingTileID = tileData.TileID;
                    return spawner.SpawnTile(tileData, playerTile, positionX, positionY, playerTileZ, mapParent);
                }
            case EnumTileType.None:
                {
                    return spawner.SpawnTile(tileData, emptyTile, positionX, positionY, 0, mapParent);
                }
            default:
                {
                    return new MapTile();
                }
        }
    }
}

public interface ITileSpawner
{
    public MapTile SpawnTile(MapTileData tileData, MapTile tilePrefab, int positionX, int positionY, int positionZ, Transform mapParent);
}

public class TileSpawner : ScriptableObject, ITileSpawner
{
    public MapTile SpawnTile(MapTileData tileData, MapTile tilePrefab, int positionX, int positionY, int positionZ, Transform mapParent)
    {
        var terrainTile = Instantiate(tilePrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity, mapParent);
        terrainTile.PrepareTile(tileData);
        return terrainTile;
    }
}