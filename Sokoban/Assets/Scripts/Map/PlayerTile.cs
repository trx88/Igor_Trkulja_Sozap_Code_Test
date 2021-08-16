using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Player tile. Moves itself and box (when needed).
/// </summary>
public class PlayerTile : MapTile
{
    private const float MOVE_INCREMENT = 0.01f;

    //Create for AudioController to subscribe to play the movement sound effect.
    public delegate void MoveMade();
    public static event MoveMade OnMoveMade;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Used for moving the player. Avoids using Update method.
    /// </summary>
    /// <param name="position">New player position</param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    IEnumerator MovePlayerCoroutine(Vector3 position, float moveTime = 2.0f)
    {
        float elapsedTime = 0.0f;
        while (!transform.localPosition.AreAlmostEqual(position))
        {
            transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            position,
            (elapsedTime / moveTime)
            );
            elapsedTime += MOVE_INCREMENT;

            yield return null;
        }
    }

    /// <summary>
    /// Used for moving the player and the box. Avoids using Update method.
    /// </summary>
    /// <param name="position">New player position</param>
    /// <param name="box">Box tile</param>
    /// <param name="boxNewPosition">New box position</param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    IEnumerator PlayerPushesBoxCoroutine(Vector3 position, MovableTile box, Vector3 boxNewPosition, float moveTime = 2.0f)
    {
        float elapsedTime = 0.0f;
        while (!transform.localPosition.AreAlmostEqual(position))
        {
            transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            position,
            (elapsedTime / moveTime)
            );
            elapsedTime += MOVE_INCREMENT;

            box.transform.localPosition = Vector3.Lerp(
            box.transform.localPosition,
            boxNewPosition,
            (elapsedTime / moveTime)
            );
            elapsedTime += MOVE_INCREMENT;

            yield return null;
        }
    }

    /// <summary>
    /// Method for moving the player. It's async in order to await for courotine to be completed
    /// </summary>
    /// <param name="tilePosition">New player position</param>
    /// <returns></returns>
    public async Task MoveToAnotherTile(Vector3 tilePosition)
    {
        OnMoveMade();
        await StartCoroutine(MovePlayerCoroutine(tilePosition));
    }

    /// <summary>
    /// Method for moving the player with the box. It's async in order to await for courotine to be completed
    /// </summary>
    /// <param name="tilePosition">New player position</param>
    /// <param name="box">Box tile</param>
    /// <param name="boxNewPosition">New box position</param>
    /// <returns></returns>
    public async Task MoveBoxToAnotherTile(Vector3 tilePosition, MovableTile box, Vector3 boxNewPosition)
    {
        OnMoveMade();
        await StartCoroutine(PlayerPushesBoxCoroutine(tilePosition, box, boxNewPosition));
    }
}

/// <summary>
/// Extends awaiters in order to be able to await for coroutines.
/// </summary>
public static class AwaitExtensions
{
    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this Coroutine coroutine)
    {
        return Task.Delay(800).GetAwaiter();//Fixed to 800 milliseconds for other command can't be pass for execution.
    }
}

/// <summary>
/// Vector 3 extensions
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// Check if Vectors are "equal".
    /// </summary>
    /// <param name="position">Vector that call the method</param>
    /// <param name="newPosition">Vector for comparison</param>
    /// <param name="precision">Margin for error</param>
    /// <returns></returns>
    public static bool AreAlmostEqual(this Vector3 position, Vector3 newPosition, float precision = 0.01f)
    {
        bool equal = true;

        if (Mathf.Abs(position.x - newPosition.x) > precision)
        { 
            equal = false; 
        }
        if (Mathf.Abs(position.y - newPosition.y) > precision)
        {
            equal = false;
        }
        if (Mathf.Abs(position.z - newPosition.z) > precision)
        {
            equal = false;
        }

        return equal;
    }
}
