using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTile : MapTile, IMoveableTile
{
    private int currentTileID;
    public int CurrentTileID
    {
        get => currentTileID;
        set
        {
            currentTileID = value;
        }
    }

    public void PrepareTile(MapTileData tileData)
    {
        base.PrepareTile(tileData);
        currentTileID = tileData.TileID;
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

    IEnumerator MovePlayerCoroutine(Vector3 position, float moveTime = 1.5f)
    {
        float elapsedTime = 0.0f;
        while (!AlmostEqual(transform.position, position, 0.01f))
        {
            transform.position = Vector3.Lerp(
            transform.position,
            position,
            (elapsedTime / moveTime)
            );
            //elapsedTime += 0.01f;
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }
    }

    public void MoveToAnotherTile(Vector3 tilePosition)
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
