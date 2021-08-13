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

public enum EnumMovementDirection
{
    Right,
    Left,
    Up,
    Down
}

public class MapController : MonoBehaviour
{
    private const int tileSize = 1;

    #region Mocked map
    private int mapWidth = 8;
    private int mapHeight = 5;

    private MapTileData[][] mapInput = new MapTileData[][]
    {
        new MapTileData[] 
        { 
            new MapTileData(0, EnumTileType.Wall), new MapTileData(1, EnumTileType.Wall), 
            new MapTileData(2, EnumTileType.Wall), new MapTileData(3, EnumTileType.Wall), 
            new MapTileData(4, EnumTileType.Wall), new MapTileData(5, EnumTileType.Wall), 
            new MapTileData(6, EnumTileType.None), new MapTileData(7, EnumTileType.None)
        },

        new MapTileData[]
        {
            new MapTileData(8, EnumTileType.Wall), new MapTileData(9, EnumTileType.Target),
            new MapTileData(10, EnumTileType.Grass), new MapTileData(11, EnumTileType.Grass),
            new MapTileData(12, EnumTileType.Grass), new MapTileData(13, EnumTileType.Wall),
            new MapTileData(14, EnumTileType.Wall), new MapTileData(15, EnumTileType.Wall)
        },

        new MapTileData[]
        {
            new MapTileData(16, EnumTileType.Wall), new MapTileData(17, EnumTileType.Grass),
            new MapTileData(18, EnumTileType.Box), new MapTileData(19, EnumTileType.Grass),
            new MapTileData(20, EnumTileType.Box), new MapTileData(21, EnumTileType.Grass),
            new MapTileData(22, EnumTileType.Grass), new MapTileData(23, EnumTileType.Wall)
        },

        new MapTileData[]
        {
            new MapTileData(24, EnumTileType.Wall), new MapTileData(25, EnumTileType.Grass),
            new MapTileData(26, EnumTileType.Target), new MapTileData(27, EnumTileType.Wall),
            new MapTileData(28, EnumTileType.Player), new MapTileData(29, EnumTileType.Grass),
            new MapTileData(30, EnumTileType.Grass), new MapTileData(31, EnumTileType.Wall)
        },

        new MapTileData[]
        {
            new MapTileData(32, EnumTileType.Wall), new MapTileData(33, EnumTileType.Wall),
            new MapTileData(34, EnumTileType.Wall), new MapTileData(35, EnumTileType.Wall),
            new MapTileData(36, EnumTileType.Wall), new MapTileData(37, EnumTileType.Wall),
            new MapTileData(38, EnumTileType.Wall), new MapTileData(39, EnumTileType.Wall)
        }
    };
    #endregion

    private Dictionary<int, MapTile> ProcessedTiles = new Dictionary<int, MapTile>();
    private List<TerrainTile> PlacedTiles = new List<TerrainTile>();
    private List<MapTile> MapTiles = new List<MapTile>();
    private Dictionary<int, MovableTile> MovableTiles = new Dictionary<int, MovableTile>();
    private Dictionary<int, List<MapTile>> TileNeighbors = new Dictionary<int, List<MapTile>>();

    private List<NeighborsDirection> Directions = new List<NeighborsDirection>()
    {
        new NeighborsDirection(1, 0), //right
        new NeighborsDirection(-1, 0), //left
        new NeighborsDirection(0, -1), //up
        new NeighborsDirection(0, 1) //down
    };

    public Transform mapParent;

    private PlayerTerrainTile PlayerReference;

    public PlayerTerrainTile GetPlayerTile()
    {
        return PlayerReference;
    }

    int CalculateMapOffsetForMovement(EnumMovementDirection movementDirection)
    {
        switch (movementDirection)
        {
            case EnumMovementDirection.Right:
                return 1;
                break;
            case EnumMovementDirection.Left:
                return -1;
                break;
            case EnumMovementDirection.Up:
                return -mapWidth;
                break;
            case EnumMovementDirection.Down:
                return mapWidth;
                break;
            default: return 0;
                break;
        }
    }

    bool IsNeighborInsideTheMap(MapTile tile, NeighborsDirection direction)
    {
        return
            tile.ColumnIndex + direction.X >= 0 &&
            tile.RowIndex + direction.Y >= 0 &&
            tile.ColumnIndex + direction.X <= mapWidth - 1 &&
            tile.RowIndex + direction.Y <= mapHeight - 1;
    }

    List<MapTile> FindTileNeighbors(MapTile tile)
    {
        List<MapTile> neighbors = new List<MapTile>();

        foreach(NeighborsDirection direction in Directions)
        {
            if(IsNeighborInsideTheMap(tile, direction))
            {
                int widthIndex = tile.PositionX + direction.X;
                int heightIndex = Mathf.Abs(tile.PositionY - direction.Y);

                //TODO: Comment on this (explain the "magic number")
                int CalculatedTileID = heightIndex * mapWidth + widthIndex;
                neighbors.Add(ProcessedTiles[CalculatedTileID]);
            }
        }

        return neighbors;
    }

    void SetNeighborsForEachTile()
    {
        foreach (MapTile tile in ProcessedTiles.Values)
        {
            var neighbours = FindTileNeighbors(tile);
            TileNeighbors.Add(tile.TileID, neighbours);
        }
    }

    List<MapTile> GetNeighbors(MapTile tile)
    {
        List<MapTile> neighbors = new List<MapTile>();

        if (TileNeighbors.TryGetValue(tile.TileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    List<MapTile> GetPlayerNeighbors()
    {
        List<MapTile> neighbors = new List<MapTile>();

        if(TileNeighbors.TryGetValue(PlayerReference.CurrentTileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    void ProcessMapTile(int tileID, MapTile tile)
    {
        ProcessedTiles.Add(tileID, tile);
        if(tile.GetType() == typeof(MovableTile))
        {
            MovableTiles.Add(tileID, (MovableTile)tile);
        }
    }

    void PlaceTilesOnMap()
    {
        for (int heightIndex = 0; heightIndex < mapInput.Length; heightIndex++)
        {
            for (int widthIndex = 0; widthIndex < mapInput[heightIndex].Length; widthIndex++)
            {
                //TODO: get rid of this
                mapInput[heightIndex][widthIndex].SetPosition(widthIndex * tileSize, -heightIndex * tileSize);
                mapInput[heightIndex][widthIndex].RowIndex = heightIndex;
                mapInput[heightIndex][widthIndex].ColumnIndex = widthIndex;

                var mapTile = MapTileSpawner.Instance.CreateTileFromData(mapInput[heightIndex][widthIndex], widthIndex * tileSize, -heightIndex * tileSize, mapParent);
                ProcessMapTile(mapTile.TileID, mapTile);
                //MapTiles.Add(mapTile);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaceTilesOnMap();

        SetNeighborsForEachTile();

        PlayerReference = (PlayerTerrainTile)ProcessedTiles[MapTileSpawner.Instance.PlayerStartingTileID];

        Vector3 topLeftPosition = new Vector3(Screen.width * 0.05f, Screen.height * 0.9f, 0);
        mapParent.transform.position = Camera.main.ScreenToWorldPoint(topLeftPosition);
        mapParent.transform.position = new Vector3(mapParent.transform.position.x, mapParent.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(AreBoxesInPlace())
        {
            Debug.Log("LEVEL COMPLETED!");
        }
    }

    //TEST
    bool AreBoxesInPlace()
    {
        int[] targetIDs = new int[2] { 9, 26 };
        int boxesInPlace = 0;
        for(int i=0; i<2; i++)
        {
            if(MovableTiles.ContainsKey(targetIDs[i]))
            {
                boxesInPlace++;
            }
        }

        return boxesInPlace == 2;
    }

    public bool TryMove(EnumMovementDirection movementDirection, out PlayerTerrainTile playerTile, out Vector3 playerDestination, out MovableTile boxTile, out Vector3 boxDestination)
    {
        playerTile = null;
        playerDestination = new Vector3();
        boxTile = null;
        boxDestination = new Vector3();
        bool canMove = false;

        int CalculatedMovementDirection = CalculateMapOffsetForMovement(movementDirection);

        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach (MapTile neighbor in playerNeighbors)
        {
            if (neighbor.TileID == PlayerReference.CurrentTileID + CalculatedMovementDirection &&
                neighbor.IsTraversable)
            {
                playerTile = PlayerReference;
                playerDestination = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                PlayerReference.CurrentTileID = neighbor.TileID;

                canMove = true;
            }
            else if (neighbor.TileID == PlayerReference.CurrentTileID + CalculatedMovementDirection &&
                neighbor.IsPushable)
            {
                //Check if neighbor can be moved up
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach (MapTile boxNeighbor in boxNeighbors)
                {
                    if (boxNeighbor.TileID == neighbor.TileID + CalculatedMovementDirection &&
                        boxNeighbor.IsTraversable)
                    {
                        int boxCurrentID = neighbor.TileID;
                        int boxesNeighborCurrentID = boxNeighbor.TileID;

                        PlayerReference.CurrentTileID = boxCurrentID;

                        playerTile = PlayerReference;
                        playerDestination = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                        //Using this, because everything is a pointer in C#...
                        //boxTile = (MovableTile)MapTiles[neighbor.TileID];
                        //boxTile.CurrentTileID = boxesNeighborCurrentID;
                        //boxDestination = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);

                        //MapTile temp = MapTiles[neighbor.TileID];
                        //MapTiles[neighbor.TileID] = MapTiles[boxNeighbor.TileID];
                        //MapTiles[boxNeighbor.TileID] = temp;

                        boxTile = MovableTiles[neighbor.TileID];
                        boxTile.CurrentTileID = boxesNeighborCurrentID;
                        boxDestination = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);
                        MovableTiles.Remove(neighbor.TileID);
                        MovableTiles.Add(boxTile.CurrentTileID, boxTile);

                        EnumTileType boxCurrentType = ProcessedTiles[boxCurrentID].TileType;
                        //EnumTileType boxesNeighborCurrentType = ProcessedTiles[boxesNeighborCurrentID].TileType;

                        //ProcessedTiles[boxCurrentID].TurnInto(PlayerReference.TileType);//box to grass/target
                        //ProcessedTiles[boxesNeighborCurrentID].TurnInto(EnumTileType.Box);//grass/target to box

                        if (ProcessedTiles[boxesNeighborCurrentID].TileType == EnumTileType.Target)
                        {
                            ProcessedTiles[boxCurrentID].TurnInto(PlayerReference.TileType);//box to grass/target
                            ProcessedTiles[boxesNeighborCurrentID].TurnIntoPushable();
                            //ProcessedTiles[boxCurrentID].TurnIntoTraversable();
                        }
                        else
                        {
                            ProcessedTiles[boxCurrentID].TurnInto(PlayerReference.TileType);//box to grass/target
                            ProcessedTiles[boxesNeighborCurrentID].TurnInto(EnumTileType.Box);//grass/target to box
                        }

                        canMove = true;
                    }
                }
            }
        }

        return canMove;
    }
}