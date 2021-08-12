using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public bool CommandCompleted = false;

    public abstract void Execute(PlayerTerrainTile tile, MapController mapController);

    public async virtual void Move(MapController mapController)
    {

    }
}

public class DoNothing : Command
{
    public override void Execute(PlayerTerrainTile tile, MapController mapController)
    {
        Move(mapController);
    }

    public override void Move(MapController mapController)
    {
        Debug.Log("Waiting...");
    }
}

public class MoveRight : Command
{
    public override void Execute(PlayerTerrainTile tile, MapController mapController)
    {
        Move(mapController);
    }

    public async override void Move(MapController mapController)
    {
        CommandCompleted = false;

        PlayerTerrainTile OutPlayerTile = null;
        Vector3 newPlayerPosition;
        MovableTile OutBoxTile = null;
        Vector3 newBoxPosition;

        if(mapController.TryMove(EnumMovementDirection.Right, out OutPlayerTile, out newPlayerPosition, out OutBoxTile, out newBoxPosition))
        {
            if(OutPlayerTile && OutBoxTile)
            {
                //Move both
                await OutPlayerTile.MoveBoxToAnotherTile(newPlayerPosition, OutBoxTile, newBoxPosition);
                //await OutBoxTile.MoveToAnotherTile(newBoxPosition);
                Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if(OutPlayerTile)
            {
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        //call map controller method for replacing tiles
        CommandCompleted = true;
    }
}

public class MoveLeft : Command
{
    public override void Execute(PlayerTerrainTile tile, MapController mapController)
    {
        Move(mapController);
    }

    public async override void Move(MapController mapController)
    {
        CommandCompleted = false;

        PlayerTerrainTile OutPlayerTile = null;
        Vector3 newPlayerPosition;
        MovableTile OutBoxTile = null;
        Vector3 newBoxPosition;

        if (mapController.TryMove(EnumMovementDirection.Left, out OutPlayerTile, out newPlayerPosition, out OutBoxTile, out newBoxPosition))
        {
            if (OutPlayerTile && OutBoxTile)
            {
                //Move both
                await OutPlayerTile.MoveBoxToAnotherTile(newPlayerPosition, OutBoxTile, newBoxPosition);
                //await OutBoxTile.MoveToAnotherTile(newBoxPosition);
                Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if (OutPlayerTile)
            {
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        //call map controller method for replacing tiles
        CommandCompleted = true;
    }
}

public class MoveUp : Command
{
    public override void Execute(PlayerTerrainTile tile, MapController mapController)
    {
        Move(mapController);
    }

    public async override void Move(MapController mapController)
    {
        CommandCompleted = false;

        PlayerTerrainTile OutPlayerTile = null;
        Vector3 newPlayerPosition;
        MovableTile OutBoxTile = null;
        Vector3 newBoxPosition;

        if (mapController.TryMove(EnumMovementDirection.Up, out OutPlayerTile, out newPlayerPosition, out OutBoxTile, out newBoxPosition))
        {
            if (OutPlayerTile && OutBoxTile)
            {
                //Move both
                await OutPlayerTile.MoveBoxToAnotherTile(newPlayerPosition, OutBoxTile, newBoxPosition);
                //await OutBoxTile.MoveToAnotherTile(newBoxPosition);
                Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if (OutPlayerTile)
            {
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        //call map controller method for replacing tiles
        CommandCompleted = true;
    }
}

public class MoveDown : Command
{
    public override void Execute(PlayerTerrainTile tile, MapController mapController)
    {
        Move(mapController);
    }

    public async override void Move(MapController mapController)
    {
        CommandCompleted = false;

        PlayerTerrainTile OutPlayerTile = null;
        Vector3 newPlayerPosition;
        MovableTile OutBoxTile = null;
        Vector3 newBoxPosition;

        if (mapController.TryMove(EnumMovementDirection.Down, out OutPlayerTile, out newPlayerPosition, out OutBoxTile, out newBoxPosition))
        {
            if (OutPlayerTile && OutBoxTile)
            {
                //Move both
                await OutPlayerTile.MoveBoxToAnotherTile(newPlayerPosition, OutBoxTile, newBoxPosition);
                //await OutBoxTile.MoveToAnotherTile(newBoxPosition);
                Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if (OutPlayerTile)
            {
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        //call map controller method for replacing tiles
        CommandCompleted = true;
    }
}
