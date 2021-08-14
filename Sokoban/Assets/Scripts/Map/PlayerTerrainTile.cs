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
            yield return null;
        }
        //Debug.Log("Stopped moving.");
        movementDone = true;
        yield return null;
    }

    IEnumerator PlayerPushesBoxCoroutine(Vector3 position, MovableTile box, Vector3 boxNewPosition, float moveTime = 2.0f)
    {
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
            //StartCoroutine(MovePlayerCoroutine(position));

            box.transform.localPosition = Vector3.Lerp(
            box.transform.localPosition,
            boxNewPosition,
            (elapsedTime / moveTime)
            );
            elapsedTime += 0.01f;

            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        //Debug.Log("Stopped moving.");
        movementDone = true;
        yield return null;
    }

    //public async Task MovePlayer(Vector3 tilePosition)
    //{
    //    await StartCoroutine(MovePlayerCoroutine(tilePosition));
    //}

    //public async Task MoveBoxWithPlayer(Vector3 tilePosition, MovableTile box, Vector3 boxNewPosition)
    //{
    //    await StartCoroutine(PlayerPushesBoxCoroutine(tilePosition, box, boxNewPosition));
    //}

    public async Task MoveToAnotherTile(Vector3 tilePosition)
    {
        //transform.position = tilePosition;

        //await MovePlayer(tilePosition);
        await StartCoroutine(MovePlayerCoroutine(tilePosition));
    }

    public async Task MoveBoxToAnotherTile(Vector3 tilePosition, MovableTile box, Vector3 boxNewPosition)
    {
        //transform.position = tilePosition;

        //await MoveBoxWithPlayer(tilePosition, box, boxNewPosition);
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
        return Task.Delay(600).GetAwaiter();
    }
}
