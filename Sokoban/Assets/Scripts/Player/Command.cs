using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public bool commandCompleted = false;

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
        commandCompleted = false;

        PlayerTile outPlayerTile = null;
        Vector3 outNewPlayerPosition;
        MovableTile outBoxTile = null;
        Vector3 outNewBoxPosition;

        if(mapController.TryMove(EnumMovementDirection.Right, out outPlayerTile, out outNewPlayerPosition, out outBoxTile, out outNewBoxPosition))
        {
            if(outPlayerTile && outBoxTile)
            {
                //Move both
                await outPlayerTile.MoveBoxToAnotherTile(outNewPlayerPosition, outBoxTile, outNewBoxPosition);
            }
            else if(outPlayerTile)
            {
                //Move player
                await outPlayerTile.MoveToAnotherTile(outNewPlayerPosition);
            }
        }

        commandCompleted = true;
    }
}

public class MoveLeft : MoveCommand
{
    public async override void Move(MapController mapController)
    {
        commandCompleted = false;

        PlayerTile outPlayerTile = null;
        Vector3 outNewPlayerPosition;
        MovableTile outBoxTile = null;
        Vector3 outNewBoxPosition;

        if (mapController.TryMove(EnumMovementDirection.Left, out outPlayerTile, out outNewPlayerPosition, out outBoxTile, out outNewBoxPosition))
        {
            if (outPlayerTile && outBoxTile)
            {
                //Move both
                await outPlayerTile.MoveBoxToAnotherTile(outNewPlayerPosition, outBoxTile, outNewBoxPosition);
            }
            else if (outPlayerTile)
            {
                //Move player
                await outPlayerTile.MoveToAnotherTile(outNewPlayerPosition);
            }
        }

        commandCompleted = true;
    }
}

public class MoveUp : MoveCommand
{
    public async override void Move(MapController mapController)
    {
        commandCompleted = false;

        PlayerTile outPlayerTile = null;
        Vector3 outNewPlayerPosition;
        MovableTile outBoxTile = null;
        Vector3 outNewBoxPosition;

        if (mapController.TryMove(EnumMovementDirection.Up, out outPlayerTile, out outNewPlayerPosition, out outBoxTile, out outNewBoxPosition))
        {
            if (outPlayerTile && outBoxTile)
            {
                //Move both
                await outPlayerTile.MoveBoxToAnotherTile(outNewPlayerPosition, outBoxTile, outNewBoxPosition);
            }
            else if (outPlayerTile)
            {
                //Move player
                await outPlayerTile.MoveToAnotherTile(outNewPlayerPosition);
            }
        }

        commandCompleted = true;
    }
}

public class MoveDown : MoveCommand
{
    public async override void Move(MapController mapController)
    {
        commandCompleted = false;

        PlayerTile outPlayerTile = null;
        Vector3 outNewPlayerPosition;
        MovableTile outBoxTile = null;
        Vector3 outNewBoxPosition;

        if (mapController.TryMove(EnumMovementDirection.Down, out outPlayerTile, out outNewPlayerPosition, out outBoxTile, out outNewBoxPosition))
        {
            if (outPlayerTile && outBoxTile)
            {
                //Move both
                await outPlayerTile.MoveBoxToAnotherTile(outNewPlayerPosition, outBoxTile, outNewBoxPosition);
            }
            else if (outPlayerTile)
            {
                //Move player
                await outPlayerTile.MoveToAnotherTile(outNewPlayerPosition);
            }
        }

        commandCompleted = true;
    }
}
