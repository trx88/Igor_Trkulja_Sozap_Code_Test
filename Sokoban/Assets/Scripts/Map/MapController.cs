using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NeighborsDirection
{
    public int X { get; }
    public int Y { get; }

    public NeighborsDirection(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class MapController : MonoBehaviour
{
    private const float tileSize = 1.0f;

    #region Mocked map
    private MapTile[][] mapInput = new MapTile[][]
    {
        new MapTile[] 
        { 
            new MapTile(0, EnumTileType.Wall), new MapTile(1, EnumTileType.Wall), 
            new MapTile(2, EnumTileType.Wall), new MapTile(3, EnumTileType.Wall), 
            new MapTile(4, EnumTileType.Wall), new MapTile(5, EnumTileType.Wall), 
            new MapTile(6, EnumTileType.None), new MapTile(7, EnumTileType.None)
        },

        new MapTile[]
        {
            new MapTile(8, EnumTileType.Wall), new MapTile(9, EnumTileType.Target),
            new MapTile(10, EnumTileType.Grass), new MapTile(11, EnumTileType.Grass),
            new MapTile(12, EnumTileType.Grass), new MapTile(13, EnumTileType.Wall),
            new MapTile(14, EnumTileType.Wall), new MapTile(15, EnumTileType.Wall)
        },

        new MapTile[]
        {
            new MapTile(16, EnumTileType.Wall), new MapTile(17, EnumTileType.Grass),
            new MapTile(18, EnumTileType.Box), new MapTile(19, EnumTileType.Grass),
            new MapTile(20, EnumTileType.Box), new MapTile(21, EnumTileType.Grass),
            new MapTile(22, EnumTileType.Grass), new MapTile(23, EnumTileType.Wall)
        },

        new MapTile[]
        {
            new MapTile(24, EnumTileType.Wall), new MapTile(25, EnumTileType.Grass),
            new MapTile(26, EnumTileType.Target), new MapTile(27, EnumTileType.Wall),
            new MapTile(28, EnumTileType.Player), new MapTile(29, EnumTileType.Grass),
            new MapTile(30, EnumTileType.Grass), new MapTile(31, EnumTileType.Wall)
        },

        new MapTile[]
        {
            new MapTile(32, EnumTileType.Wall), new MapTile(33, EnumTileType.Wall),
            new MapTile(34, EnumTileType.Wall), new MapTile(35, EnumTileType.Wall),
            new MapTile(36, EnumTileType.Wall), new MapTile(37, EnumTileType.Wall),
            new MapTile(38, EnumTileType.Wall), new MapTile(39, EnumTileType.Wall)
        }
    };
    #endregion

    private List<TerrainTile> PlacedTiles = new List<TerrainTile>();
    private Dictionary<int, List<MapTile>> TileNeighbors = new Dictionary<int, List<MapTile>>();
    private List<NeighborsDirection> Directions = new List<NeighborsDirection>()
    {
        new NeighborsDirection(1, 0), //right
        new NeighborsDirection(-1, 0), //left
        new NeighborsDirection(0, -1), //up
        new NeighborsDirection(0, 1) //down
    };

    #region Prefab properties (publicly exposed)
    public TerrainTile wallTile;
    public TerrainTile grassTile;
    public TerrainTile boxTile;
    public TerrainTile targetTile;
    public PlayerTerrainTile playerTile;
    #endregion

    private PlayerTerrainTile PlayerTileReference;

    bool isNeighborInsideTheMap(MapTile tile, NeighborsDirection direction)
    {
        return
            false;
    }

    TerrainTile PlaceTile(TerrainTile tile, int tileID, float positionX, float positionY, float positionZ = 0)
    {
        TerrainTile terrainTile = Instantiate(tile, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
        terrainTile.TileID = tileID;
        PlacedTiles.Add(terrainTile);
        return terrainTile;
    }

    PlayerTerrainTile PlacePlayerTile(PlayerTerrainTile tile, int tileID, float positionX, float positionY, float positionZ = 0)
    {
        PlayerTerrainTile playerTile = Instantiate(tile, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
        playerTile.CurrentTileID = playerTile.TileID = tileID;
        return playerTile;
    }

    void PlaceTilesOnMap()
    {
        for (int heightIndex = 0; heightIndex < mapInput.Length; heightIndex++)
        {
            for (int widthIndex = 0; widthIndex < mapInput[heightIndex].Length; widthIndex++)
            {
                switch (mapInput[heightIndex][widthIndex].TileType)
                {
                    case EnumTileType.Wall:
                        {
                            PlaceTile(wallTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                    case EnumTileType.Grass:
                        {
                            PlaceTile(grassTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                    case EnumTileType.Box:
                        {
                            PlaceTile(boxTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                    case EnumTileType.Target:
                        {
                            PlaceTile(targetTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                    case EnumTileType.Player:
                        {
                            //Place grass tile instead, since player can only start on grass tile. 
                            PlaceTile(grassTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize, 1);
                            //TODO: Add player to the map (player tile should live here, and use player controller to send "signals" to move it???)
                            PlayerTileReference = PlacePlayerTile(playerTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                    case EnumTileType.None:
                        {
                            //PlaceTile(grassTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                }
                Debug.Log(string.Format("[0] x:{0} y:{1} bx:{2} by:{3}",
            mapInput[heightIndex][widthIndex].PositionX,
            mapInput[heightIndex][widthIndex].PositionY,
            mapInput[heightIndex][widthIndex].BoundryX,
            mapInput[heightIndex][widthIndex].BoundryY));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaceTilesOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
