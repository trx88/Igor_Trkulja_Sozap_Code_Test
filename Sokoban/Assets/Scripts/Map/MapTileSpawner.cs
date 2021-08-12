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

    TileSpawner Spawner;

    public MapTile CreateTileFromData(MapTileData tileData, int positionX, int positionY)
    {
        TileType = tileData.TileType;

        switch (TileType)
        {
            case EnumTileType.Wall:
                {
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<TerrainTileSpawner>());
                    return Spawner.AnyTileSpawner.SpawnTile(tileData, wallTile, positionX, positionY, wallTileZ);
                }
            case EnumTileType.Grass:
                {
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<TerrainTileSpawner>());
                    return Spawner.AnyTileSpawner.SpawnTile(tileData, grassTile, positionX, positionY, grassTileZ);
                }
            case EnumTileType.Box:
                {
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<TerrainTileSpawner>());
                    Spawner.AnyTileSpawner.SpawnTile(tileData, grassTile, positionX, positionY, grassTileZ);
                    
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<MovableTileSpawner>());
                    return Spawner.AnyTileSpawner.SpawnTile(tileData, boxTile, positionX, positionY, boxTileZ);
                }
            case EnumTileType.Target:
                {
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<TerrainTileSpawner>());
                    return Spawner.AnyTileSpawner.SpawnTile(tileData, targetTile, positionX, positionY, targetTileZ);
                }
            case EnumTileType.Player:
                {
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<TerrainTileSpawner>());
                    Spawner.AnyTileSpawner.SpawnTile(tileData, grassTile, positionX, positionY, grassTileZ);

                    playerStartingTileID = tileData.TileID;
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<PlayerTileSpawner>());
                    return Spawner.AnyTileSpawner.SpawnTile(tileData, playerTile, positionX, positionY, playerTileZ);
                }
            case EnumTileType.None:
                {
                    Spawner = new TileSpawner(ScriptableObject.CreateInstance<TerrainTileSpawner>());
                    return Spawner.AnyTileSpawner.SpawnTile(tileData, emptyTile, positionX, positionY, 0);
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
    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY, int positionZ);

    public MapTile SpawnTile(MapTileData tileData, MovableTile movableTilePrefab, int positionX, int positionY, int positionZ);

    public MapTile SpawnTile(MapTileData tileData, PlayerTerrainTile playerTilePrefab, int positionX, int positionY, int positionZ);
}

public interface ITerrainTileSpawner : ITileSpawner
{
    public new MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY, int positionZ);
}

public interface IMovableTileSpawner : ITileSpawner
{
    public new MapTile SpawnTile(MapTileData tileData, MovableTile movableTilePrefab, int positionX, int positionY, int positionZ);
}

public interface IPlayerTileSpawner : ITileSpawner
{
    public new MapTile SpawnTile(MapTileData tileData, PlayerTerrainTile playerTilePrefab, int positionX, int positionY, int positionZ);
}

public class TileSpawner
{
    private ITileSpawner anyTileSpawner;
    public ITileSpawner AnyTileSpawner
    {
        get => anyTileSpawner;
    }


    public TileSpawner()
    {

    }

    public TileSpawner(ITileSpawner tileSpawner)
    {
        anyTileSpawner = tileSpawner;
    }
}

public class TerrainTileSpawner : ScriptableObject, ITerrainTileSpawner
{
    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY, int positionZ)
    {
        var terrainTile = Instantiate(terrainTilePrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
        terrainTile.PrepareTile(tileData);
        return terrainTile;
    }

    public MapTile SpawnTile(MapTileData tileData, MovableTile movableTilePrefab, int positionX, int positionY, int positionZ)
    {
        throw new System.NotImplementedException();
    }
    public MapTile SpawnTile(MapTileData tileData, PlayerTerrainTile playerTilePrefab, int positionX, int positionY, int positionZ)
    {
        throw new System.NotImplementedException();
    }
}

public class MovableTileSpawner : ScriptableObject, IMovableTileSpawner
{
    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY, int positionZ)
    {
        throw new System.NotImplementedException();
    }

    public MapTile SpawnTile(MapTileData tileData, MovableTile movableTilePrefab, int positionX, int positionY, int positionZ)
    {
        var movableTile = Instantiate(movableTilePrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
        movableTile.PrepareTile(tileData);
        return movableTile;
    }
    public MapTile SpawnTile(MapTileData tileData, PlayerTerrainTile playerTilePrefab, int positionX, int positionY, int positionZ)
    {
        throw new System.NotImplementedException();
    }
}

public class PlayerTileSpawner : ScriptableObject, IPlayerTileSpawner
{
    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY, int positionZ)
    {
        throw new System.NotImplementedException();
    }

    public MapTile SpawnTile(MapTileData tileData, MovableTile movableTilePrefab, int positionX, int positionY, int positionZ)
    {
        throw new System.NotImplementedException();
    }

    public MapTile SpawnTile(MapTileData tileData, PlayerTerrainTile playerTilePrefab, int positionX, int positionY, int positionZ)
    {
        var playerTile = Instantiate(playerTilePrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
        playerTile.PrepareTile(tileData);
        return playerTile;
    }
}