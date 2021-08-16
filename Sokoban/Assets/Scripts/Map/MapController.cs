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
    public delegate void NotifyUIAboutTime(int seconds);
    public static event NotifyUIAboutTime OnNotifyUIAboutTime;

    public delegate void LevelCompleted(int newLevelID);
    public static event LevelCompleted OnLevelCompleted;

    public delegate void LevelStarted(PlayerTile player);
    public static event LevelStarted OnLevelStarted;

    private const float SCREEN_OFFSET_PERCENT_X = 0.05f;
    private const float SCREEN_OFFSET_PERCENT_Y = 0.9f;
    private const int TILE_SIZE = 1;

    private int secondsPlaying;
    private float timer = 0.0f;

    private int mapWidth;
    private int mapHeight;

    private Dictionary<int, MapTile> processedTiles = new Dictionary<int, MapTile>();
    private Dictionary<int, MovableTile> movableTiles = new Dictionary<int, MovableTile>();
    private Dictionary<int, List<MapTile>> tileNeighbors = new Dictionary<int, List<MapTile>>();
    private List<MapTile> targetTiles = new List<MapTile>();

    private List<NeighborsDirection> directions = new List<NeighborsDirection>()
    {
        new NeighborsDirection(1, 0), //right
        new NeighborsDirection(-1, 0), //left
        new NeighborsDirection(0, -1), //up
        new NeighborsDirection(0, 1) //down
    };

    public Transform mapParent;

    private PlayerTile playerReference;

    public PlayerTile GetPlayerTile()
    {
        return playerReference;
    }

    int CalculateMapOffsetForMovement(EnumMovementDirection movementDirection)
    {
        switch (movementDirection)
        {
            case EnumMovementDirection.Right:
                return 1;
            case EnumMovementDirection.Left:
                return -1;
            case EnumMovementDirection.Up:
                return -mapWidth;
            case EnumMovementDirection.Down:
                return mapWidth;
            default: return 0;
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

        foreach(NeighborsDirection direction in directions)
        {
            if(IsNeighborInsideTheMap(tile, direction))
            {
                int widthIndex = tile.PositionX + direction.X;
                int heightIndex = Mathf.Abs(tile.PositionY - direction.Y);//Minus, because map tiles are being place from top-left corner

                //TODO: Comment on this 
                //(explain the "magic number") -> Get the row start index (heightIndex * mapWidth) and add current widthIndex
                int CalculatedTileID = heightIndex * mapWidth + widthIndex;
                neighbors.Add(processedTiles[CalculatedTileID]);
            }
        }

        return neighbors;
    }

    void SetNeighborsForEachTile()
    {
        foreach (MapTile tile in processedTiles.Values)
        {
            var neighbours = FindTileNeighbors(tile);
            tileNeighbors.Add(tile.TileID, neighbours);
        }
    }

    List<MapTile> GetNeighbors(MapTile tile)
    {
        List<MapTile> neighbors = new List<MapTile>();

        if (tileNeighbors.TryGetValue(tile.TileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    List<MapTile> GetPlayerNeighbors()
    {
        List<MapTile> neighbors = new List<MapTile>();

        if(tileNeighbors.TryGetValue(playerReference.CurrentTileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    void ProcessMapTile(int tileID, MapTile tile)
    {
        processedTiles.Add(tileID, tile);
        if(tile.GetType() == typeof(MovableTile))
        {
            movableTiles.Add(tileID, (MovableTile)tile);
        }
        if(tile.TileType == EnumTileType.Target)
        {
            targetTiles.Add(tile);
        }
    }

    void PlaceTilesOnMap()
    {
        MapData mapData = GetComponent<MapLoader>().LoadMapFromJSON(LevelController.Instance.SelectedLevel);
        mapWidth = mapData.mapWidth;
        mapHeight = mapData.mapHeight;

        for (int heightIndex = 0; heightIndex < mapData.mapHeight; heightIndex++)
        {
            for (int widthIndex = 0; widthIndex < mapData.mapWidth; widthIndex++)
            {
                int CalculatedTileID = heightIndex * mapWidth + widthIndex;

                var mapTile = MapTileSpawner.Instance.CreateTileFromData(mapData.mapTilesData[CalculatedTileID], widthIndex * TILE_SIZE, -heightIndex * TILE_SIZE, mapParent);
                mapTile.SetPosition(widthIndex * TILE_SIZE, -heightIndex * TILE_SIZE);
                mapTile.RowIndex = heightIndex;
                mapTile.ColumnIndex = widthIndex;
                ProcessMapTile(mapTile.TileID, mapTile);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(string.Format("Selected level: {0}", LevelController.Instance.SelectedLevel));

        PlaceTilesOnMap();

        SetNeighborsForEachTile();

        playerReference = (PlayerTile)processedTiles[MapTileSpawner.Instance.PlayerStartingTileID];

        SetMapParentInTheWorld();

        StartCoroutine(CountLevelTime());

        OnLevelStarted(playerReference);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CountLevelTime()
    {
        while(!AreBoxesInPlace())
        {
            timer += Time.deltaTime;
            secondsPlaying = (int)(timer % 60);
            if (OnNotifyUIAboutTime != null)
            {
                OnNotifyUIAboutTime(secondsPlaying);
            }
            yield return null;
        }
    }

    public void SetMapParentInTheWorld()
    {
        Vector3 topLeftPosition = new Vector3(Screen.width * SCREEN_OFFSET_PERCENT_X, Screen.height * SCREEN_OFFSET_PERCENT_Y, 0);
        mapParent.transform.position = Camera.main.ScreenToWorldPoint(topLeftPosition);
        mapParent.transform.position = new Vector3(mapParent.transform.position.x, mapParent.transform.position.y, 0);
    }

    public bool AreBoxesInPlace()
    {
        int boxesInPlace = 0;

        for(int targetTileIndex=0; targetTileIndex < targetTiles.Count; targetTileIndex++)
        {
            if(movableTiles.ContainsKey(targetTiles[targetTileIndex].TileID))
            {
                boxesInPlace++;
            }
        }

        return boxesInPlace == targetTiles.Count;
    }

    public void CompleteLevel()
    {
        LevelController.Instance.UpdateLevelStatistics(LevelController.Instance.SelectedLevel, true, secondsPlaying);
        LevelController.Instance.SelectedLevel++;
        OnLevelCompleted(LevelController.Instance.SelectedLevel);
    }

    public bool TryMove(EnumMovementDirection movementDirection, out PlayerTile playerTile, out Vector3 playerDestination, out MovableTile boxTile, out Vector3 boxDestination)
    {
        playerTile = null;
        playerDestination = new Vector3();
        boxTile = null;
        boxDestination = new Vector3();
        bool canMove = false;

        int calculatedMovementDirection = CalculateMapOffsetForMovement(movementDirection);

        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        foreach (MapTile neighbor in playerNeighbors)
        {
            if (neighbor.TileID == playerReference.CurrentTileID + calculatedMovementDirection &&
                neighbor.IsTraversable)
            {
                playerTile = playerReference;
                playerDestination = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                playerReference.CurrentTileID = neighbor.TileID;

                canMove = true;
            }
            else if (neighbor.TileID == playerReference.CurrentTileID + calculatedMovementDirection &&
                neighbor.IsPushable)
            {
                //Check if neighboring box can be moved in chosen direction
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach (MapTile boxNeighbor in boxNeighbors)
                {
                    if (boxNeighbor.TileID == neighbor.TileID + calculatedMovementDirection &&
                        boxNeighbor.IsTraversable)
                    {
                        int boxCurrentID = neighbor.TileID;
                        int boxesNeighborCurrentID = boxNeighbor.TileID;

                        playerReference.CurrentTileID = boxCurrentID;

                        playerTile = playerReference;
                        playerDestination = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                        boxTile = movableTiles[neighbor.TileID];
                        boxTile.CurrentTileID = boxesNeighborCurrentID;
                        boxDestination = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);
                        movableTiles.Remove(neighbor.TileID);
                        movableTiles.Add(boxTile.CurrentTileID, boxTile);

                        if (processedTiles[boxesNeighborCurrentID].TileType == EnumTileType.Target)
                        {
                            //TEST
                            var particles = processedTiles[boxesNeighborCurrentID].GetComponent<ParticleSystem>();
                            if (particles != null)
                            {
                                if (!particles.isPlaying)
                                {
                                    particles.Play();
                                }
                            }

                            processedTiles[boxCurrentID].TurnInto(playerReference.TileType);
                            processedTiles[boxesNeighborCurrentID].TurnIntoPushable();
                        }
                        else
                        {
                            processedTiles[boxCurrentID].TurnInto(playerReference.TileType);
                            processedTiles[boxesNeighborCurrentID].TurnInto(EnumTileType.Box);
                        }

                        canMove = true;
                    }
                }
            }
        }

        return canMove;
    }
}