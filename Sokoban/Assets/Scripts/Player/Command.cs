using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public bool CommandCompleted = false;

    public abstract void Execute(MapController mapController);
}

public abstract class MoveCommand : Command
{
    public override void Execute(MapController mapController)
    {
        Move(mapController);
    }

    public abstract void Move(MapController mapController);
}

public class DoNothing : Command
{
    public override void Execute(MapController mapController)
    {
        Debug.Log("Waiting...");
    }
}

public class MoveRight : MoveCommand
{
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
                //Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if(OutPlayerTile)
            {
                //Move player
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                //Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        CommandCompleted = true;
    }
}

public class MoveLeft : MoveCommand
{
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
                //Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if (OutPlayerTile)
            {
                //Move player
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                //Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        CommandCompleted = true;
    }
}

public class MoveUp : MoveCommand
{
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
                //Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if (OutPlayerTile)
            {
                //Move player
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                //Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        CommandCompleted = true;
    }
}

public class MoveDown : MoveCommand
{
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
                //Debug.Log(string.Format("Player on tile: {0} Box on tile: {1}", OutPlayerTile.CurrentTileID, OutBoxTile.CurrentTileID));
            }
            else if (OutPlayerTile)
            {
                //Move player
                await OutPlayerTile.MoveToAnotherTile(newPlayerPosition);
                //Debug.Log(string.Format("Player on tile: {0}", OutPlayerTile.CurrentTileID));
            }
        }

        //Debug.Log("Command completed");
        CommandCompleted = true;
    }
}
