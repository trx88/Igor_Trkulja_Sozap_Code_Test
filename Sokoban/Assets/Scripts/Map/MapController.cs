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
    //Move this to some level class
    public delegate void NotifyUIAboutTime(int seconds);
    public static event NotifyUIAboutTime OnNotifyUIAboutTime;
    private int secondsPlaying;
    private float timer = 0.0f;

    //Move this to some level class
    public delegate void LevelCompleted(int newLevelID);
    public static event LevelCompleted OnLevelCompleted;

    private const float SCREEN_OFFSET_PERCENT_X = 0.05f;
    private const float SCREEN_OFFSET_PERCENT_Y = 0.9f;
    private const int TILE_SIZE = 1;

    private int mapWidth;
    private int mapHeight;

    private Dictionary<int, MapTile> ProcessedTiles = new Dictionary<int, MapTile>();
    private Dictionary<int, MovableTile> MovableTiles = new Dictionary<int, MovableTile>();
    private Dictionary<int, List<MapTile>> TileNeighbors = new Dictionary<int, List<MapTile>>();
    private List<MapTile> TargetTiles = new List<MapTile>();

    private List<NeighborsDirection> Directions = new List<NeighborsDirection>()
    {
        new NeighborsDirection(1, 0), //right
        new NeighborsDirection(-1, 0), //left
        new NeighborsDirection(0, -1), //up
        new NeighborsDirection(0, 1) //down
    };

    public Transform mapParent;

    public AudioSource AudioLevelPlayMusic;
    public AudioSource AudioLevelCompletedMusic;

    private PlayerTerrainTile PlayerReference;

    //private PlayerSettings playerSettings;

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
        if(tile.TileType == EnumTileType.Target)
        {
            TargetTiles.Add(tile);
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

        PlayerReference = (PlayerTerrainTile)ProcessedTiles[MapTileSpawner.Instance.PlayerStartingTileID];

        //playerSettings = GetComponent<PlayerSettings>();
        AudioLevelPlayMusic.volume = PlayerSettings.Instance.GetMusicLevel();
        AudioLevelCompletedMusic.volume = PlayerSettings.Instance.GetMusicLevel() / 2;
        PlayerReference.GetComponent<AudioSource>().volume = PlayerSettings.Instance.GetEffectsLevel();
        AudioLevelCompletedMusic.Stop();

        SetMapParentInTheWorld();

        StartCoroutine(CountLevelTime());
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

        for(int targetTileIndex=0; targetTileIndex < TargetTiles.Count; targetTileIndex++)
        {
            if(MovableTiles.ContainsKey(TargetTiles[targetTileIndex].TileID))
            {
                boxesInPlace++;
            }
        }

        return boxesInPlace == TargetTiles.Count;
    }

    public void CompleteLevel()
    {
        AudioLevelPlayMusic.Stop();
        AudioLevelCompletedMusic.time = 4.0f;
        AudioLevelCompletedMusic.Play();

        LevelController.Instance.UpdateLevelStatistics(LevelController.Instance.SelectedLevel, true, secondsPlaying);
        LevelController.Instance.SelectedLevel++;
        OnLevelCompleted(LevelController.Instance.SelectedLevel);
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
                //Check if neighboring box can be moved in chosen direction
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

                        boxTile = MovableTiles[neighbor.TileID];
                        boxTile.CurrentTileID = boxesNeighborCurrentID;
                        boxDestination = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);
                        MovableTiles.Remove(neighbor.TileID);
                        MovableTiles.Add(boxTile.CurrentTileID, boxTile);

                        if (ProcessedTiles[boxesNeighborCurrentID].TileType == EnumTileType.Target)
                        {
                            //TEST
                            var particles = ProcessedTiles[boxesNeighborCurrentID].GetComponent<ParticleSystem>();
                            if (particles != null)
                            {
                                if (!particles.isPlaying)
                                {
                                    particles.Play();
                                }
                            }

                            ProcessedTiles[boxCurrentID].TurnInto(PlayerReference.TileType);
                            ProcessedTiles[boxesNeighborCurrentID].TurnIntoPushable();
                        }
                        else
                        {
                            ProcessedTiles[boxCurrentID].TurnInto(PlayerReference.TileType);
                            ProcessedTiles[boxesNeighborCurrentID].TurnInto(EnumTileType.Box);
                        }

                        canMove = true;
                    }
                }
            }
        }

        return canMove;
    }
}