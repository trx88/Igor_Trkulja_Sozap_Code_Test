using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to set the direction in the matrix for finding tile neighbors.
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

//Used to determine player movement direction in TryMove method
public enum EnumMovementDirection
{
    Right,
    Left,
    Up,
    Down
}

/// <summary>
/// A big one...
/// Handles all map related stuff. Player movement, map creation, is level completed, etc.
/// </summary>
public class MapController : MonoBehaviour
{
    //For HUD to show second spent in the level. Avoids using Update method.
    public delegate void NotifyUIAboutTime(int seconds);
    public static event NotifyUIAboutTime OnNotifyUIAboutTime;

    //For HUD to potentially show next level button.
    public delegate void LevelCompleted(int newLevelID);
    public static event LevelCompleted OnLevelCompleted;

    //For Audio controller to start playing level music.
    public delegate void LevelStarted(PlayerTile player);
    public static event LevelStarted OnLevelStarted;

    //Used for positioning map parent game object.
    private const float SCREEN_OFFSET_PERCENT_X = 0.05f;
    private const float SCREEN_OFFSET_PERCENT_Y = 0.9f;
    private const int TILE_SIZE = 1;

    private int secondsPlaying;
    private float timer = 0.0f;

    private int mapWidth;
    private int mapHeight;

    //Tiles placed on the map
    private Dictionary<int, MapTile> processedTiles = new Dictionary<int, MapTile>();
    //Movable tiles (boxes, in other words). Player tile has a separate reference.
    private Dictionary<int, MovableTile> movableTiles = new Dictionary<int, MovableTile>();
    //Each tile neighbors.
    private Dictionary<int, List<MapTile>> tileNeighbors = new Dictionary<int, List<MapTile>>();
    //Target tiles (where boxes should be placed)
    private List<MapTile> targetTiles = new List<MapTile>();

    //Create directions in the matrix for finding neighbors. -1 is used, since map is created from the top-left corner.
    private List<NeighborsDirection> directions = new List<NeighborsDirection>()
    {
        new NeighborsDirection(1, 0), //right
        new NeighborsDirection(-1, 0), //left
        new NeighborsDirection(0, -1), //up
        new NeighborsDirection(0, 1) //down
    };

    //Map parent game object
    public Transform mapParent;

    //Player tile
    private PlayerTile playerReference;

    public PlayerTile GetPlayerTile()
    {
        return playerReference;
    }

    /// <summary>
    /// Calculates offset for movement in the matrix.
    /// </summary>
    /// <param name="movementDirection"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Map if flat. We wouldn't want to fall off. :D
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    bool IsNeighborInsideTheMap(MapTile tile, NeighborsDirection direction)
    {
        return
            tile.ColumnIndex + direction.X >= 0 &&
            tile.RowIndex + direction.Y >= 0 &&
            tile.ColumnIndex + direction.X <= mapWidth - 1 &&
            tile.RowIndex + direction.Y <= mapHeight - 1;
    }

    /// <summary>
    /// Sets the list of tile neighbors.
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Sets the neighbors for each tile when map is created.
    /// </summary>
    void SetNeighborsForEachTile()
    {
        foreach (MapTile tile in processedTiles.Values)
        {
            var neighbours = FindTileNeighbors(tile);
            tileNeighbors.Add(tile.TileID, neighbours);
        }
    }

    /// <summary>
    /// Gets the tile neighbor
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    List<MapTile> GetNeighbors(MapTile tile)
    {
        List<MapTile> neighbors = new List<MapTile>();

        if (tileNeighbors.TryGetValue(tile.TileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    /// <summary>
    /// Gets the player tile neighbor. Kinda redundant...
    /// </summary>
    /// <returns></returns>
    List<MapTile> GetPlayerNeighbors()
    {
        List<MapTile> neighbors = new List<MapTile>();

        if(tileNeighbors.TryGetValue(playerReference.CurrentTileID, out neighbors))
        {
            return neighbors;
        }

        return neighbors;
    }

    /// <summary>
    /// Adds a newly created tile correct collections.
    /// </summary>
    /// <param name="tileID"></param>
    /// <param name="tile"></param>
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

    /// <summary>
    /// Loads the map from the JSON file and places the tiles on the map. Uses MapTileSpawner to spawn tiles and processes the tiles.
    /// </summary>
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
        //Debug.Log(string.Format("Selected level: {0}", LevelController.Instance.SelectedLevel));

        PlaceTilesOnMap();

        SetNeighborsForEachTile();

        playerReference = (PlayerTile)processedTiles[MapTileSpawner.Instance.PlayerStartingTileID];

        SetMapParentInTheWorld();

        //Avoids using Update method for counting seconds in level.
        StartCoroutine(CountLevelTime());

        OnLevelStarted(playerReference);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Avoids using Update method for counting seconds in level.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Repositions map parent in the world.
    /// </summary>
    public void SetMapParentInTheWorld()
    {
        Vector3 topLeftPosition = new Vector3(Screen.width * SCREEN_OFFSET_PERCENT_X, Screen.height * SCREEN_OFFSET_PERCENT_Y, 0);
        mapParent.transform.position = Camera.main.ScreenToWorldPoint(topLeftPosition);
        mapParent.transform.position = new Vector3(mapParent.transform.position.x, mapParent.transform.position.y, 0);
    }

    /// <summary>
    /// Win condition.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Complete current level. Update JSON level statistics. Notify other classes to try to load a next level.
    /// </summary>
    public void CompleteLevel()
    {
        LevelController.Instance.UpdateLevelStatistics(LevelController.Instance.SelectedLevel, true, secondsPlaying);
        LevelController.Instance.SelectedLevel++;
        OnLevelCompleted(LevelController.Instance.SelectedLevel);
    }

    /// <summary>
    /// Checks if movement in some direction is possible.
    /// </summary>
    /// <param name="movementDirection"></param>
    /// <param name="playerTile"></param>
    /// <param name="playerDestination"></param>
    /// <param name="boxTile"></param>
    /// <param name="boxDestination"></param>
    /// <returns></returns>
    public bool TryMove(EnumMovementDirection movementDirection, out PlayerTile playerTile, out Vector3 playerDestination, out MovableTile boxTile, out Vector3 boxDestination)
    {
        playerTile = null;
        playerDestination = new Vector3();
        boxTile = null;
        boxDestination = new Vector3();
        bool canMove = false;

        //Gets the matrix offset for chosen movement direction
        int calculatedMovementDirection = CalculateMapOffsetForMovement(movementDirection);

        List<MapTile> playerNeighbors = GetPlayerNeighbors();
        //Check player neighbors
        foreach (MapTile neighbor in playerNeighbors)
        {
            //Can player move without the box to empty space (Grass tile).
            if (neighbor.TileID == playerReference.CurrentTileID + calculatedMovementDirection &&
                neighbor.IsTraversable)
            {
                playerTile = playerReference;
                playerDestination = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                //Change player's current tile ID to grass tile's ID (player will be there when movement is complete).
                playerReference.CurrentTileID = neighbor.TileID;

                canMove = true;
            }
            //Can player move with the box to empty space (Grass tile) or target space (Target tile).
            else if (neighbor.TileID == playerReference.CurrentTileID + calculatedMovementDirection &&
                neighbor.IsPushable)
            {
                List<MapTile> boxNeighbors = GetNeighbors(neighbor);
                foreach (MapTile boxNeighbor in boxNeighbors)
                {
                    //Check if neighboring box can be moved in chosen direction (e.g. there's a grass tile behind the box tile)
                    if (boxNeighbor.TileID == neighbor.TileID + calculatedMovementDirection &&
                        boxNeighbor.IsTraversable)
                    {
                        int boxCurrentID = neighbor.TileID;
                        int boxesNeighborCurrentID = boxNeighbor.TileID;

                        //Change player's current tile ID to box tile's ID (player will be there when movement is complete).
                        playerReference.CurrentTileID = boxCurrentID;

                        playerTile = playerReference;
                        playerDestination = new Vector3(neighbor.PositionX, neighbor.PositionY, -1);

                        boxTile = movableTiles[neighbor.TileID];
                        boxTile.CurrentTileID = boxesNeighborCurrentID;
                        boxDestination = new Vector3(boxNeighbor.PositionX, boxNeighbor.PositionY, -1);

                        //Refresh the movable tile list (ID's of tiles have been changed).
                        movableTiles.Remove(neighbor.TileID);
                        movableTiles.Add(boxTile.CurrentTileID, boxTile);

                        //Changes enum types and movement constraint bools for tiles on the mapp
                        if (processedTiles[boxesNeighborCurrentID].TileType == EnumTileType.Target)
                        {
                            //EXREMLY UGLY :( (Out of time for some kind of normal solution).
                            ((TerrainTile)processedTiles[boxesNeighborCurrentID]).PlayParticles();

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