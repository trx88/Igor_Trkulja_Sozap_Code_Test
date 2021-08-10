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
    private const int tileSize = 1;

    #region Mocked map
    private int mapWidth = 8;
    private int mapHeight = 5;

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

    private Dictionary<int, MapTile> ProcessedTiles = new Dictionary<int, MapTile>();
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
    public TerrainTile emptyTile;
    #endregion

    private PlayerTerrainTile PlayerTileReference;

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
                neighbors.Add(mapInput[heightIndex][widthIndex]);
            }
        }

        return neighbors;
    }

    void SetNeighborsForEachTile()
    {
        foreach(MapTile tile in ProcessedTiles.Values)
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

        if(TileNeighbors.TryGetValue(PlayerTileReference.CurrentTileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    void ProcessMapTile(int tileID, MapTile tile)
    {
        ProcessedTiles.Add(tileID, tile);
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
                            Instantiate(grassTile, new Vector3(widthIndex * tileSize, -heightIndex * tileSize, 0), Quaternion.identity);
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
                            PlayerTileReference = PlacePlayerTile(playerTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize, -1);
                        }
                        break;
                    case EnumTileType.None:
                        {
                            PlaceTile(emptyTile, mapInput[heightIndex][widthIndex].TileID, widthIndex * tileSize, -heightIndex * tileSize);
                        }
                        break;
                }
                mapInput[heightIndex][widthIndex].SetPosition(widthIndex * tileSize, -heightIndex * tileSize);
                mapInput[heightIndex][widthIndex].RowIndex = heightIndex;
                mapInput[heightIndex][widthIndex].ColumnIndex = widthIndex;
                ProcessMapTile(mapInput[heightIndex][widthIndex].TileID, mapInput[heightIndex][widthIndex]);

            //    Debug.Log(string.Format("[{0}] x:{1} y:{2} bx:{3} by:{4}",
            //mapInput[heightIndex][widthIndex].TileID,
            //mapInput[heightIndex][widthIndex].PositionX,
            //mapInput[heightIndex][widthIndex].PositionY,
            //mapInput[heightIndex][widthIndex].BoundryX,
            //mapInput[heightIndex][widthIndex].BoundryY));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaceTilesOnMap();

        SetNeighborsForEachTile();

        //List<MapTile> playerNeighbors = FindTileNeighbors(mapInput[3][4]);
        //foreach (MapTile playerNeighbor in playerNeighbors)
        //{
        //    Debug.Log(string.Format("NeighborID: {0}", playerNeighbor.TileID));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMoveRight(Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMoveLeft(Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryMoveUp(Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryMoveDown(Time.deltaTime);
        }
    }

    public static bool AlmostEqual(Vector3 v1, Vector3 v2, float precision)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision) equal = false;

        return equal;
    }

    void MovePlayer(Vector3 position)
    {
        PlayerTileReference.transform.position = Vector3.MoveTowards(
                    PlayerTileReference.transform.position,
                    position,
                    1.0f
                    );
    }

    void MoveBox(ref TerrainTile box, Vector3 position)
    {
        box.transform.position = Vector3.MoveTowards(
            box.transform.position,
            position,
            1.0f
            );
    }

    void TryMoveRight(float deltaTime)
    {
        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach(MapTile neighbor in playerNeighbors)
        {
            if(neighbor.TileID == PlayerTileReference.CurrentTileID + 1 &&
                neighbor.IsTraversable)
            {
                Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                //PlayerTileReference.transform.position = Vector3.Lerp(
                //    PlayerTileReference.transform.position,
                //    newPosition, 
                //    deltaTime * 5
                //    );

                //PlayerTileReference.transform.position = Vector3.MoveTowards(
                //    PlayerTileReference.transform.position,
                //    newPosition,
                //    1.0f
                //    );
                MovePlayer(newPosition);

                if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.05f))
                {
                    PlayerTileReference.CurrentTileID = neighbor.TileID;
                }
            }
            else if (neighbor.TileID == PlayerTileReference.CurrentTileID + 1 &&
                neighbor.IsPushable)
            {
                //Check if neighbor can be moved up
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach (MapTile boxNeighbor in boxNeighbors)
                {
                    if (boxNeighbor.TileID == neighbor.TileID + 1 &&
                        boxNeighbor.IsTraversable)
                    {
                        Vector3 newBoxPosition = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);

                        TerrainTile boxTile = PlacedTiles[neighbor.TileID];
                        while (!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            MoveBox(ref boxTile, newBoxPosition);
                        }

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        MovePlayer(newPosition);

                        if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            //boxTile.TileID = boxNeighbor.TileID;
                            //update placed tiles (TerrainTile)
                            TerrainTile temp = PlacedTiles[neighbor.TileID];
                            PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                            PlacedTiles[boxNeighbor.TileID] = temp;

                            //update processed tiles (MapTile)
                            ProcessedTiles[boxNeighbor.TileID].TileType = EnumTileType.Box;
                            ProcessedTiles[boxNeighbor.TileID].IsPushable = true;
                            ProcessedTiles[boxNeighbor.TileID].IsTraversable = false;
                            ProcessedTiles[neighbor.TileID].TileType = EnumTileType.Grass;
                            ProcessedTiles[neighbor.TileID].IsPushable = false;
                            ProcessedTiles[neighbor.TileID].IsTraversable = true;

                            //set neighbors again
                            //SetNeighborsForEachTile();
                        }

                        if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        {
                            PlayerTileReference.CurrentTileID = neighbor.TileID;
                        }
                    }
                }
            }
        }
    }

    void TryMoveLeft(float deltaTime)
    {
        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach (MapTile neighbor in playerNeighbors)
        {
            if (neighbor.TileID == PlayerTileReference.CurrentTileID - 1 &&
                neighbor.IsTraversable)
            {
                Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                //PlayerTileReference.transform.position = Vector3.Lerp(
                //    PlayerTileReference.transform.position,
                //    newPosition,
                //    deltaTime * 5
                //    );

                MovePlayer(newPosition);

                if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.05f))
                {
                    PlayerTileReference.CurrentTileID = neighbor.TileID;
                }
            }
            else if (neighbor.TileID == PlayerTileReference.CurrentTileID - 1 &&
                neighbor.IsPushable)
            {
                //Check if neighbor can be moved up
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach (MapTile boxNeighbor in boxNeighbors)
                {
                    if (boxNeighbor.TileID == neighbor.TileID - 1 &&
                        boxNeighbor.IsTraversable)
                    {
                        Vector3 newBoxPosition = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);

                        TerrainTile boxTile = PlacedTiles[neighbor.TileID];
                        while (!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            MoveBox(ref boxTile, newBoxPosition);
                        }

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        MovePlayer(newPosition);

                        if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            //boxTile.TileID = boxNeighbor.TileID;
                            //update placed tiles (TerrainTile)
                            TerrainTile temp = PlacedTiles[neighbor.TileID];
                            PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                            PlacedTiles[boxNeighbor.TileID] = temp;

                            //update processed tiles (MapTile)
                            ProcessedTiles[boxNeighbor.TileID].TileType = EnumTileType.Box;
                            ProcessedTiles[boxNeighbor.TileID].IsPushable = true;
                            ProcessedTiles[boxNeighbor.TileID].IsTraversable = false;
                            ProcessedTiles[neighbor.TileID].TileType = EnumTileType.Grass;
                            ProcessedTiles[neighbor.TileID].IsPushable = false;
                            ProcessedTiles[neighbor.TileID].IsTraversable = true;

                            //set neighbors again
                            //SetNeighborsForEachTile();
                        }

                        if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        {
                            PlayerTileReference.CurrentTileID = neighbor.TileID;
                        }
                    }
                }
            }
        }
    }

    void TryMoveUp(float deltaTime)
    {
        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach (MapTile neighbor in playerNeighbors)
        {
            if (neighbor.TileID == PlayerTileReference.CurrentTileID - mapWidth &&
                neighbor.IsTraversable)
            {
                Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                //PlayerTileReference.transform.position = Vector3.Lerp(
                //    PlayerTileReference.transform.position,
                //    newPosition,
                //    deltaTime * 5
                //    );

                MovePlayer(newPosition);

                if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.05f))
                {
                    PlayerTileReference.CurrentTileID = neighbor.TileID;
                }
            }
            else if (neighbor.TileID == PlayerTileReference.CurrentTileID - mapWidth &&
                neighbor.IsPushable)
            {
                //Check if neighbor can be moved up
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach(MapTile boxNeighbor in boxNeighbors)
                {
                    if(boxNeighbor.TileID == neighbor.TileID - mapWidth && 
                        boxNeighbor.IsTraversable)
                    {
                        Vector3 newBoxPosition = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, 0);

                        TerrainTile boxTile = PlacedTiles[neighbor.TileID];
                        while (!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            MoveBox(ref boxTile, newBoxPosition);
                        }

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        MovePlayer(newPosition);

                        if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            //boxTile.TileID = boxNeighbor.TileID;
                            //update placed tiles (TerrainTile)
                            TerrainTile temp = PlacedTiles[neighbor.TileID];
                            PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                            PlacedTiles[boxNeighbor.TileID] = temp;

                            //update processed tiles (MapTile)
                            ProcessedTiles[boxNeighbor.TileID].TileType = EnumTileType.Box;
                            ProcessedTiles[boxNeighbor.TileID].IsPushable = true;
                            ProcessedTiles[boxNeighbor.TileID].IsTraversable = false;
                            ProcessedTiles[neighbor.TileID].TileType = EnumTileType.Grass;
                            ProcessedTiles[neighbor.TileID].IsPushable = false;
                            ProcessedTiles[neighbor.TileID].IsTraversable = true;

                            //set neighbors again
                            //SetNeighborsForEachTile();
                        }

                        if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        {
                            PlayerTileReference.CurrentTileID = neighbor.TileID;
                        }
                    }
                }
            }
        }
    }
    void TryMoveDown(float deltaTime)
    {
        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach (MapTile neighbor in playerNeighbors)
        {
            if (neighbor.TileID == PlayerTileReference.CurrentTileID + mapWidth &&
                neighbor.IsTraversable)
            {
                Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                //PlayerTileReference.transform.position = Vector3.Lerp(
                //    PlayerTileReference.transform.position,
                //    newPosition,
                //    deltaTime * 5
                //    );

                MovePlayer(newPosition);

                if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.05f))
                {
                    PlayerTileReference.CurrentTileID = neighbor.TileID;
                }
            }
            else if (neighbor.TileID == PlayerTileReference.CurrentTileID + mapWidth &&
                neighbor.IsPushable)
            {
                //Check if neighbor can be moved up
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach (MapTile boxNeighbor in boxNeighbors)
                {
                    if (boxNeighbor.TileID == neighbor.TileID + mapWidth &&
                        boxNeighbor.IsTraversable)
                    {
                        Vector3 newBoxPosition = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);

                        TerrainTile boxTile = PlacedTiles[neighbor.TileID];
                        while(!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            MoveBox(ref boxTile, newBoxPosition);
                        }

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        MovePlayer(newPosition);

                        if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.05f))
                        {
                            //boxTile.TileID = boxNeighbor.TileID;
                            //update placed tiles (TerrainTile)
                            TerrainTile temp = PlacedTiles[neighbor.TileID];
                            PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                            PlacedTiles[boxNeighbor.TileID] = temp;

                            //update processed tiles (MapTile)
                            ProcessedTiles[boxNeighbor.TileID].TileType = EnumTileType.Box;
                            ProcessedTiles[boxNeighbor.TileID].IsPushable = true;
                            ProcessedTiles[boxNeighbor.TileID].IsTraversable = false;
                            ProcessedTiles[neighbor.TileID].TileType = EnumTileType.Grass;
                            ProcessedTiles[neighbor.TileID].IsPushable = false;
                            ProcessedTiles[neighbor.TileID].IsTraversable = true;

                            //set neighbors again
                            //SetNeighborsForEachTile();
                        }

                        if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        {
                            PlayerTileReference.CurrentTileID = neighbor.TileID;
                        }
                    }
                }
            }
        }
    }
}
