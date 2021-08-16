using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Basic building block.
/// </summary>
public enum EnumTileType
{
    Wall,
    Grass,
    Box,
    Target,
    Player,
    None
};

public class MapTile : MonoBehaviour
{
    public int TileID;
    public bool IsTraversable = false;
    public bool IsPushable = false;
    public EnumTileType TileType;

    private int currentTileID;
    public int CurrentTileID
    {
        get => currentTileID;
        set
        {
            currentTileID = value;
        }
    }

    protected int positionX;
    public int PositionX
    {
        get => positionX;
        set
        {
            positionX = value;
        }
    }

    protected int positionY;
    public int PositionY
    {
        get => positionY;
        set
        {
            positionY = value;
        }
    }

    protected int boundryX;
    public int BoundryX
    {
        get => boundryX;
        set
        {
            boundryX = value;
        }
    }

    protected int boundryY;
    public int BoundryY
    {
        get => boundryY;
        set
        {
            boundryY = value;
        }
    }

    protected int rowIndex;
    public int RowIndex
    {
        get => rowIndex;
        set
        {
            rowIndex = value;
        }
    }

    protected int columnIndex;
    public int ColumnIndex
    {
        get => columnIndex;
        set
        {
            columnIndex = value;
        }
    }

    public MapTile()
    {
        TileID = -1;
        IsTraversable = false;
        IsPushable = false;
        TileType = EnumTileType.None;
        PositionX = 0;
        PositionY = 0;
        BoundryX = 1;
        BoundryY = -1;
    }

    public MapTile(int tileID, EnumTileType tileType)
    {
        TileID = tileID;
        TileType = tileType;
        switch (tileType)
        {
            case EnumTileType.Wall:
                {
                    IsTraversable = false;
                    IsPushable = false;
                }
                break;
            case EnumTileType.Grass:
                {
                    IsTraversable = true;
                    IsPushable = false;
                }
                break;
            case EnumTileType.Box:
                {
                    IsTraversable = false;
                    IsPushable = true;
                }
                break;
            case EnumTileType.Target:
                {
                    IsTraversable = true;
                    IsPushable = false;
                }
                break;
            case EnumTileType.Player:
                {
                    IsTraversable = true;
                    IsPushable = false;
                }
                break;
            case EnumTileType.None:
                {
                    IsTraversable = false;
                    IsPushable = false;
                }
                break;
        }
    }

    public void SetPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
        BoundryX = x + 1;
        BoundryY = y - 1;
    }

    public void SetPosition(int x, int y, int bx, int by)
    {
        PositionX = x;
        PositionY = y;
        BoundryX = bx;
        BoundryY = by;
    }

    public void TurnIntoTraversable()
    {
        //TileType = EnumTileType.Grass;
        IsTraversable = true;
        IsPushable = false;
    }

    public void TurnIntoPushable()
    {
        //TileType = EnumTileType.Box;
        IsTraversable = false;
        IsPushable = true;
    }

    public void TurnInto(EnumTileType tileType)
    {
        switch (tileType)
        {
            case EnumTileType.Grass:
                {
                    TileType = EnumTileType.Grass;
                    IsTraversable = true;
                    IsPushable = false;
                }
                break;
            case EnumTileType.Box:
                {
                    TileType = EnumTileType.Box;
                    IsTraversable = false;
                    IsPushable = true;
                }
                break;
            case EnumTileType.Target:
                {
                    //TileType = EnumTileType.Target;
                    IsTraversable = true;
                    IsPushable = false;
                }
                break;
            case EnumTileType.Player:
                {
                    TileType = EnumTileType.Grass;
                    IsTraversable = true;
                    IsPushable = false;
                }
                break;
            default:
                {
                    IsTraversable = false;
                    IsPushable = false;
                }
                break;
        }
    }

    public void PrepareTile(MapTileData tileData)
    {
        currentTileID = TileID = tileData.TileID;
        IsTraversable = tileData.IsTraversable;
        IsPushable = tileData.IsPushable;
        TileType = tileData.TileType;
        PositionX = tileData.PositionX;
        PositionY = tileData.PositionY;
        BoundryX = tileData.BoundryX;
        BoundryY = tileData.BoundryY;
        RowIndex = tileData.RowIndex;
        ColumnIndex = tileData.ColumnIndex;
    }
}
