using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTerrainTile : MapTile, IMoveableTile
{
    public void PrepareTile(MapTileData tileData)
    {
        base.PrepareTile(tileData);
        CurrentTileID = tileData.TileID;
    }

    public static bool AlmostEqual(Vector3 v1, Vector3 v2, float precision)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision) equal = false;

        return equal;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool movementDone = false;

    IEnumerator MovePlayerCoroutine(Vector3 position, float moveTime = 2.0f)
    {
        //movementDone = false;
        //float elapsedTime = 0.0f;
        //while (!AlmostEqual(transform.position, position, 0.01f))
        //{
        //    transform.position = Vector3.Lerp(
        //    transform.position,
        //    position,
        //    (elapsedTime / moveTime)
        //    );
        //    elapsedTime += 0.01f;
        //    //elapsedTime += Time.fixedDeltaTime;
        //    yield return new WaitForSeconds(0.01f);
        //    //yield return new WaitForSeconds(0.01f);
        //}
        ////Debug.Log("Stopped moving.");
        //movementDone = true;
        //yield return null;

        movementDone = false;
        float elapsedTime = 0.0f;
        while (!AlmostEqual(transform.localPosition, position, 0.01f))
        {
            transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            position,
            (elapsedTime / moveTime)
            );
            elapsedTime += 0.01f;
            //elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForSeconds(0.01f);
            //yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log("Stopped moving.");
        movementDone = true;
        yield return null;
    }

    IEnumerator PlayerPushesBoxCoroutine(Vector3 position, MovableTile box, Vector3 boxNewPosition, float moveTime = 2.0f)
    {
        //movementDone = false;
        //float elapsedTime = 0.0f;
        //while (!AlmostEqual(transform.position, position, 0.01f))
        //{
        //    transform.position = Vector3.Lerp(
        //    transform.position,
        //    position,
        //    (elapsedTime / moveTime)
        //    );
        //    elapsedTime += 0.01f;

        //    box.transform.position = Vector3.Lerp(
        //    box.transform.position,
        //    boxNewPosition,
        //    (elapsedTime / moveTime)
        //    );
        //    elapsedTime += 0.01f;

        //    yield return new WaitForSeconds(0.01f);
        //}
        ////Debug.Log("Stopped moving.");
        //movementDone = true;
        //yield return null;

        movementDone = false;
        float elapsedTime = 0.0f;
        while (!AlmostEqual(transform.localPosition, position, 0.01f))
        {
            transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            position,
            (elapsedTime / moveTime)
            );
            elapsedTime += 0.01f;

            box.transform.localPosition = Vector3.Lerp(
            box.transform.localPosition,
            boxNewPosition,
            (elapsedTime / moveTime)
            );
            elapsedTime += 0.01f;

            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log("Stopped moving.");
        movementDone = true;
        yield return null;
    }

    public async Task Test(Vector3 tilePosition)
    {
        //await new WaitForSeconds(1.0f);
        //Debug.Log("Wait for coroutine.");
        //await StartCoroutine(TestCoroutine());
        await StartCoroutine(MovePlayerCoroutine(tilePosition));
        //Debug.Log("Waiting done.");
    }

    public async Task TestBox(Vector3 tilePosition, MovableTile box, Vector3 boxNewPosition)
    {
        //await new WaitForSeconds(1.0f);
        //Debug.Log("Wait for coroutine.");
        //await StartCoroutine(TestCoroutine());
        await StartCoroutine(PlayerPushesBoxCoroutine(tilePosition, box, boxNewPosition));
        //Debug.Log("Waiting done.");
    }

    public async Task MoveToAnotherTile(Vector3 tilePosition)
    {
        //transform.position = tilePosition;

        //StartCoroutine(MovePlayerCoroutine(tilePosition));

        await Test(tilePosition);
    }

    public async Task MoveBoxToAnotherTile(Vector3 tilePosition, MovableTile box, Vector3 boxNewPosition)
    {
        //transform.position = tilePosition;

        //StartCoroutine(MovePlayerCoroutine(tilePosition));

        await TestBox(tilePosition, box, boxNewPosition);
    }

    public bool MoveToAnotherTileBool(Vector3 tilePosition)
    {
        //transform.position = tilePosition;
        //StartCoroutine(MovePlayerCoroutine(tilePosition));
        while (!AlmostEqual(transform.position, tilePosition, 0.01f))
        {
            transform.position = Vector3.MoveTowards(
            transform.position,
            tilePosition,
            0.1f
            );
        }
        return true;
    }
}

public static class AwaitExtensions
{
    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this IEnumerator enumerator)
    {
        return Task.Delay(1).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this Coroutine coroutine)
    {
        return Task.Delay(1000).GetAwaiter();
    }
}

public static class TaskExtensions
{
    public static IEnumerator AsIEnumerator(this Task task)
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }
}
