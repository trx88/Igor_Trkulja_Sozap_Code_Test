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
    private Dictionary<int, List<MapTile>> TileNeighbors = new Dictionary<int, List<MapTile>>();

    private List<NeighborsDirection> Directions = new List<NeighborsDirection>()
    {
        new NeighborsDirection(1, 0), //right
        new NeighborsDirection(-1, 0), //left
        new NeighborsDirection(0, -1), //up
        new NeighborsDirection(0, 1) //down
    };

    //#region Prefab properties (publicly exposed)
    //public TerrainTile wallTile;
    //public TerrainTile grassTile;
    //public TerrainTile boxTile;
    //public TerrainTile targetTile;
    //public PlayerTerrainTile playerTile;
    //public TerrainTile emptyTile;
    //#endregion

    private PlayerTerrainTile PlayerTileReference;
    private MovableTile PlayerReference;

    public MovableTile GetPlayerTile()
    {
        return PlayerReference;
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

        if(TileNeighbors.TryGetValue(PlayerReference.CurrentTileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    void ProcessMapTile(int tileID, MapTile tile)
    {
        ProcessedTiles.Add(tileID, tile);
    }

    //TerrainTile PlaceTile(TerrainTile tile, int tileID, float positionX, float positionY, float positionZ = 0)
    //{
    //    TerrainTile terrainTile = Instantiate(tile, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
    //    terrainTile.TileID = tileID;
    //    PlacedTiles.Add(terrainTile);
    //    return terrainTile;
    //}

    //PlayerTerrainTile PlacePlayerTile(PlayerTerrainTile tile, int tileID, float positionX, float positionY, float positionZ = 0)
    //{
    //    PlayerTerrainTile playerTile = Instantiate(tile, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
    //    playerTile.CurrentTileID = playerTile.TileID = tileID;
    //    return playerTile;
    //}

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

                var mapTile = MapTileSpawner.Instance.CreateTileFromData(mapInput[heightIndex][widthIndex], widthIndex * tileSize, -heightIndex * tileSize);
                ProcessMapTile(mapTile.TileID, mapTile);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaceTilesOnMap();

        SetNeighborsForEachTile();

        PlayerReference = (MovableTile)ProcessedTiles[MapTileSpawner.Instance.PlayerStartingTileID];
        Debug.Log("So far so good.");
        //List<MapTile> playerNeighbors = FindTileNeighbors(mapInput[3][4]);
        //foreach (MapTile playerNeighbor in playerNeighbors)
        //{
        //    Debug.Log(string.Format("NeighborID: {0}", playerNeighbor.TileID));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.RightArrow) && !inputBlocked)
        //{
        //    inputBlocked = true;
        //    TryMoveRight(Time.deltaTime);
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftArrow) && !inputBlocked)
        //{
        //    inputBlocked = true;
        //    TryMoveLeft(Time.deltaTime);
        //}
        //else if (Input.GetKeyDown(KeyCode.UpArrow) && !inputBlocked)
        //{
        //    inputBlocked = true;
        //    TryMoveUp(Time.deltaTime);
        //}
        //else if (Input.GetKeyDown(KeyCode.DownArrow) && !inputBlocked)
        //{
        //    inputBlocked = true;
        //    TryMoveDown(Time.deltaTime);
        //}
    }

    public static bool AlmostEqual(Vector3 v1, Vector3 v2, float precision)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision) equal = false;

        return equal;
    }

    bool inputBlocked = false;

    //for testing
    public Vector3 GetNextPositionTest()
    {
        MapTile nextTile = ProcessedTiles[29];
        return new Vector3(nextTile.PositionX, nextTile.PositionY, -1);
    }

    void MovePlayer(Vector3 position)
    {
        PlayerTileReference.transform.position = Vector3.MoveTowards(
                    PlayerTileReference.transform.position,
                    position,
                    0.01f
                    );
    }

    void MoveBox(ref TerrainTile box, Vector3 position, MapTile neighbor, MapTile boxNeighbor, float deltaTime)
    {
        //box.transform.position = Vector3.MoveTowards(
        //    box.transform.position,
        //    position,
        //    0.01f
        //    );
        StartCoroutine(MoveBoxCoroutine(box, position, neighbor, boxNeighbor, 1.0f));
    }

    IEnumerator MovePlayerCoroutine(Vector3 position, MapTile neighbor, float moveTime = 1.5f)
    {
        float elapsedTime = 0.0f;
        while (!AlmostEqual(PlayerReference.transform.position, position, 0.01f))
        {
            PlayerReference.transform.position = Vector3.Lerp(
            PlayerReference.transform.position,
            position,
            (elapsedTime / moveTime)
            );
            //elapsedTime += 0.01f;
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }

        if (AlmostEqual(PlayerReference.transform.position, position, 0.01f))
        {
            //Debug.Log("Change ID");
            PlayerReference.CurrentTileID = neighbor.TileID;
        }
        inputBlocked = false;
        yield return null;
    }

    IEnumerator MoveBoxCoroutine(TerrainTile box, Vector3 position, MapTile neighbor, MapTile boxNeighbor, float moveTime = 1.5f)
    {
        float elapsedTime = 0.0f;
        while (!AlmostEqual(box.transform.position, position, 0.01f))
        {
            box.transform.position = Vector3.Lerp(
            box.transform.position,
            position,
            (elapsedTime / moveTime)
            );
            //elapsedTime += 0.01f;
            elapsedTime += Time.fixedDeltaTime;


            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }

        if (AlmostEqual(box.transform.position, position, 0.01f))
        {
            //boxTile.TileID = boxNeighbor.TileID;
            //update placed tiles (TerrainTile)
            TerrainTile temp = PlacedTiles[neighbor.TileID];
            PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
            PlacedTiles[boxNeighbor.TileID] = temp;

            //update processed tiles (MapTile)
            ProcessedTiles[boxNeighbor.TileID].TurnIntoBox();
            ProcessedTiles[neighbor.TileID].TurnIntoGrass();

            //set neighbors again
            //SetNeighborsForEachTile();
        }
        inputBlocked = false;
        yield return null;
    }

    void TryMoveRight(float deltaTime)
    {
        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach(MapTile neighbor in playerNeighbors)
        {
            if(neighbor.TileID == PlayerReference.CurrentTileID + 1 &&
                neighbor.IsTraversable)
            {
                Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                //while (!AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    MovePlayer(newPosition);
                //}

                //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.02f))
                //{
                //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                //}
                StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));
            }
            else if (neighbor.TileID == PlayerReference.CurrentTileID + 1 &&
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
                        //while (!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    MoveBox(ref boxTile, newBoxPosition, deltaTime);
                        //}
                        MoveBox(ref boxTile, newBoxPosition, neighbor, boxNeighbor, deltaTime);

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        //MovePlayer(newPosition);
                        StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));

                        //if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    //boxTile.TileID = boxNeighbor.TileID;
                        //    //update placed tiles (TerrainTile)
                        //    TerrainTile temp = PlacedTiles[neighbor.TileID];
                        //    PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                        //    PlacedTiles[boxNeighbor.TileID] = temp;

                        //    //update processed tiles (MapTile)
                        //    ProcessedTiles[boxNeighbor.TileID].TurnIntoBox();
                        //    ProcessedTiles[neighbor.TileID].TurnIntoGrass();

                        //    //set neighbors again
                        //    //SetNeighborsForEachTile();
                        //}

                        //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        //{
                        //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                        //}
                    }
                }
            }
            else
            {
                inputBlocked = false;
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

                //while (!AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    MovePlayer(newPosition);
                //}

                //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                //}
                StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));
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
                        //while (!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    MoveBox(ref boxTile, newBoxPosition, deltaTime);
                        //}
                        MoveBox(ref boxTile, newBoxPosition, neighbor, boxNeighbor, deltaTime);

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        //MovePlayer(newPosition);
                        StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));

                        //if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    //boxTile.TileID = boxNeighbor.TileID;
                        //    //update placed tiles (TerrainTile)
                        //    TerrainTile temp = PlacedTiles[neighbor.TileID];
                        //    PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                        //    PlacedTiles[boxNeighbor.TileID] = temp;

                        //    //update processed tiles (MapTile)
                        //    ProcessedTiles[boxNeighbor.TileID].TurnIntoBox();
                        //    ProcessedTiles[neighbor.TileID].TurnIntoGrass();

                        //    //set neighbors again
                        //    //SetNeighborsForEachTile();
                        //}

                        //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        //{
                        //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                        //}
                    }
                }
            }
            else
            {
                inputBlocked = false;
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

                //while (!AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    MovePlayer(newPosition);
                //}

                //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                //}
                StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));
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
                        //while (!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    MoveBox(ref boxTile, newBoxPosition, deltaTime);
                        //}
                        MoveBox(ref boxTile, newBoxPosition, neighbor, boxNeighbor, deltaTime);

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        //MovePlayer(newPosition);
                        StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));

                        //if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    //boxTile.TileID = boxNeighbor.TileID;
                        //    //update placed tiles (TerrainTile)
                        //    TerrainTile temp = PlacedTiles[neighbor.TileID];
                        //    PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                        //    PlacedTiles[boxNeighbor.TileID] = temp;

                        //    //update processed tiles (MapTile)
                        //    ProcessedTiles[boxNeighbor.TileID].TurnIntoBox();
                        //    ProcessedTiles[neighbor.TileID].TurnIntoGrass();

                        //    //set neighbors again
                        //    //SetNeighborsForEachTile();
                        //}

                        //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        //{
                        //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                        //}
                    }
                }
            }
            else
            {
                inputBlocked = false;
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

                //while (!AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    MovePlayer(newPosition);
                //}

                //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                //{
                //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                //}
                StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));
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
                        //while(!AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    MoveBox(ref boxTile, newBoxPosition, deltaTime);
                        //}
                        MoveBox(ref boxTile, newBoxPosition, neighbor, boxNeighbor, deltaTime);

                        Vector3 newPosition = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);
                        //MovePlayer(newPosition);
                        StartCoroutine(MovePlayerCoroutine(newPosition, neighbor));

                        //if (AlmostEqual(boxTile.transform.position, newBoxPosition, 0.01f))
                        //{
                        //    //boxTile.TileID = boxNeighbor.TileID;
                        //    //update placed tiles (TerrainTile)
                        //    TerrainTile temp = PlacedTiles[neighbor.TileID];
                        //    PlacedTiles[neighbor.TileID] = PlacedTiles[boxNeighbor.TileID];
                        //    PlacedTiles[boxNeighbor.TileID] = temp;

                        //    //update processed tiles (MapTile)
                        //    ProcessedTiles[boxNeighbor.TileID].TurnIntoBox();
                        //    ProcessedTiles[neighbor.TileID].TurnIntoGrass();

                        //    //set neighbors again
                        //    //SetNeighborsForEachTile();
                        //}

                        //if (AlmostEqual(PlayerTileReference.transform.position, newPosition, 0.01f))
                        //{
                        //    PlayerTileReference.CurrentTileID = neighbor.TileID;
                        //}
                    }
                }
            }
            else
            {
                inputBlocked = false;
            }
        }
    }
}
