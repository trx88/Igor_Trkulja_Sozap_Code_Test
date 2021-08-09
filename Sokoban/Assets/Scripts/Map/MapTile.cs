using System.Collections;
using System.Collections.Generic;

public enum EnumTileType
{
    Wall,
    Grass,
    Box,
    Target,
    Player,
    None
};

public class MapTile
{
    public int TileID;
    public bool IsTraversable = false;
    public bool IsPushable = false;
    public EnumTileType TileType;

    private float positionX;
    public float PositionX
    {
        get => positionX;
        set
        {
            positionX = value;
        }
    }

    private float positionY;
    public float PositionY
    {
        get => positionY;
        set
        {
            positionY = value;
        }
    }

    private float boundryX;
    public float BoundryX
    {
        get => boundryX;
        set
        {
            boundryX = value;
        }
    }

    private float boundryY;
    public float BoundryY
    {
        get => boundryY;
        set
        {
            boundryY = value;
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
            case EnumTileType.None:
                {
                    IsTraversable = false;
                    IsPushable = false;
                }
                break;
        }
    }

    public void SetPosition(float x, float y, float bx, float by)
    {
        PositionX = x;
        PositionY = y;
        BoundryX = bx;
        BoundryY = by;
    }
}
