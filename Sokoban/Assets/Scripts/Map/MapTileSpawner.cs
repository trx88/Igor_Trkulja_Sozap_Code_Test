using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileSpawner : MonoBehaviour
{
    private static MapTileSpawner instance;

    public static MapTileSpawner Instance { get { return instance; } }

    public TerrainTile boxTile;
    public PlayerTerrainTile playerTile;
    public TerrainTile wallTile;
    public TerrainTile grassTile;
    public TerrainTile targetTile;
    public TerrainTile emptyTile;

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

    EnumTileType TileType;

    TerrainTileSpawner Spawner;

    public void CreateTile(ITile tile)
    {
        tile.SayHello();
    }

    public MapTile CreateTileFromData(MapTileData tileData, int positionX, int positionY)
    {
        TileType = tileData.TileType;

        switch(TileType)
        {
            case EnumTileType.Wall:
                {
                    Spawner = new TerrainTileSpawner(ScriptableObject.CreateInstance<WallTileSpawner>());
                    return Spawner.SpawnTile(tileData, wallTile, positionX, positionY);
                }
                break;
            case EnumTileType.Grass:
                {
                    Spawner = new TerrainTileSpawner(ScriptableObject.CreateInstance<GrassTileSpawner>());
                    return Spawner.SpawnTile(tileData, grassTile, positionX, positionY);
                }
                break;
            //case EnumTileType.Box:
            //    {
                    
            //    }
            //    break;
            case EnumTileType.Target:
                {
                    Spawner = new TerrainTileSpawner(ScriptableObject.CreateInstance<TargetTileSpawner>());
                    return Spawner.SpawnTile(tileData, targetTile, positionX, positionY);
                }
                break;
            //case EnumTileType.Player:
            //    {
                    
            //    }
            //    break;
            //case EnumTileType.None:
            //    {
                    
            //    }
            //    break;
        }

        return new MapTile();
    }
}

public class TerrainTileSpawner
{
    ITileSpawner TileSpawner;

    public TerrainTileSpawner()
    {

    }

    public TerrainTileSpawner(ITileSpawner tileSpawner)
    {
        TileSpawner = tileSpawner;
    }

    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY)
    {
        return TileSpawner.SpawnTile(tileData, terrainTilePrefab, positionX, positionY);
    }
}

public interface ITileSpawner
{
    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY);
}

public class GrassTileSpawner : ScriptableObject, ITileSpawner
{
    private const int tileZHeigh = 1;

    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY)
    {
        var grassTile = Instantiate(terrainTilePrefab, new Vector3(positionX, positionY, 1), Quaternion.identity);
        grassTile.PrepareTile(tileData);
        return grassTile;
    }
}

public class WallTileSpawner : ScriptableObject, ITileSpawner
{
    private const int tileZHeigh = 0;

    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY)
    {
        var wallTile = Instantiate(terrainTilePrefab, new Vector3(positionX, positionY, 0), Quaternion.identity);
        wallTile.PrepareTile(tileData);
        return wallTile;
    }
}

public class TargetTileSpawner : ScriptableObject, ITileSpawner
{
    private const int tileZHeigh = 0;

    public MapTile SpawnTile(MapTileData tileData, TerrainTile terrainTilePrefab, int positionX, int positionY)
    {
        var targetTile = Instantiate(terrainTilePrefab, new Vector3(positionX, positionY, tileZHeigh), Quaternion.identity);
        targetTile.PrepareTile(tileData);
        return targetTile;
    }
}