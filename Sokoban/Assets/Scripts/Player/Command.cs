using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(MovableTile tile, Vector3 newPosition);

    public abstract void ExecuteTest();

    public virtual void Move(MovableTile tile, Vector3 newPosition)
    {

    }

    public virtual bool MoveBool(MovableTile tile, Vector3 newPosition)
    {
        return true;
    }
}

public class DoNothing : Command
{
    public override void ExecuteTest()
    {
        //StartCoroutine(ExecuteCouroutine());
        Debug.Log("Waiting...");
    }

    public override void Execute(MovableTile tile, Vector3 newPosition)
    {
        Move(tile, newPosition);
    }

    public override void Move(MovableTile tile, Vector3 newPosition)
    {
        Debug.Log("Waiting...");
    }
}

public class MoveRight : Command
{
    public override void ExecuteTest()
    {
        //StartCoroutine(ExecuteCouroutine());
        Debug.Log("Moving right...");
    }

    public override void Execute(MovableTile tile, Vector3 newPosition)
    {
        //Move(tile, newPosition);
        MoveBool(tile, newPosition);
    }

    public override void Move(MovableTile tile, Vector3 newPosition)
    {
        Debug.Log("Moving right...");
        while(!MovableTile.AlmostEqual(tile.transform.position, newPosition, 0.01f))
        {
            tile.MoveToAnotherTile(newPosition);
        }
        Debug.Log("Movement done");
    }

    public override bool MoveBool(MovableTile tile, Vector3 newPosition)
    {
        Debug.Log("Moving right...");
        while (!MovableTile.AlmostEqual(tile.transform.position, newPosition, 0.01f))
        {
            tile.MoveToAnotherTile(newPosition);
        }
        Debug.Log("Movement done");
        return true;
    }
}

public class MoveLeft : Command
{
    public override void ExecuteTest()
    {
        //StartCoroutine(ExecuteCouroutine());
        Debug.Log("Moving left...");
    }

    public override void Execute(MovableTile tile, Vector3 newPosition)
    {
        Move(tile, newPosition);
    }

    public override void Move(MovableTile tile, Vector3 newPosition)
    {
        Debug.Log("Moving left...");
    }
}

public class MoveUp : Command
{
    public override void ExecuteTest()
    {
        //StartCoroutine(ExecuteCouroutine());
        Debug.Log("Moving up...");
    }

    public override void Execute(MovableTile tile, Vector3 newPosition)
    {
        Move(tile, newPosition);
    }

    public override void Move(MovableTile tile, Vector3 newPosition)
    {
        Debug.Log("Moving up...");
    }
}

public class MoveDown : Command
{
    public override void ExecuteTest()
    {
        //StartCoroutine(ExecuteCouroutine());
        Debug.Log("Moving down...");
    }

    public override void Execute(MovableTile tile, Vector3 newPosition)
    {
        Move(tile, newPosition);
    }

    public override void Move(MovableTile tile, Vector3 newPosition)
    {
        Debug.Log("Moving down...");
    }
}
