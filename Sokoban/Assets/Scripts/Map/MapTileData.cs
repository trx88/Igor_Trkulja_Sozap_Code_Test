using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapTileData
{
    public int TileID;
    public bool IsTraversable = false;
    public bool IsPushable = false;
    public EnumTileType TileType;

    private int positionX;
    public int PositionX
    {
        get => positionX;
        set
        {
            positionX = value;
        }
    }

    private int positionY;
    public int PositionY
    {
        get => positionY;
        set
        {
            positionY = value;
        }
    }

    private int boundryX;
    public int BoundryX
    {
        get => boundryX;
        set
        {
            boundryX = value;
        }
    }

    private int boundryY;
    public int BoundryY
    {
        get => boundryY;
        set
        {
            boundryY = value;
        }
    }

    private int rowIndex;
    public int RowIndex
    {
        get => rowIndex;
        set
        {
            rowIndex = value;
        }
    }

    private int columnIndex;
    public int ColumnIndex
    {
        get => columnIndex;
        set
        {
            columnIndex = value;
        }
    }

    public MapTileData()
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

    public MapTileData(int tileID, EnumTileType tileType)
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
}
