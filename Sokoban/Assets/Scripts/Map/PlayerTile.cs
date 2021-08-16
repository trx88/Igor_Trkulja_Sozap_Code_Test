using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTile : MapTile
{
    private const float MOVE_INCREMENT = 0.01f;

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

    public async Task MoveToAnotherTile(Vector3 tilePosition)
    {
        OnMoveMade();
        await StartCoroutine(MovePlayerCoroutine(tilePosition));
    }

    public async Task MoveBoxToAnotherTile(Vector3 tilePosition, MovableTile box, Vector3 boxNewPosition)
    {
        OnMoveMade();
        await StartCoroutine(PlayerPushesBoxCoroutine(tilePosition, box, boxNewPosition));
    }
}

public static class AwaitExtensions
{
    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this Coroutine coroutine)
    {
        return Task.Delay(800).GetAwaiter();
    }
}

public static class Vector3Extensions
{
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
