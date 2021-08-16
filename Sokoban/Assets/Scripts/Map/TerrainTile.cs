using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Terrain tile. Minimal number of behaviour in addition to MapTile.
/// </summary>
public class TerrainTile : MapTile
{
    //public void PrepareTile(MapTileData tileData)
    //{
    //    base.PrepareTile(tileData);
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// A very ugly solution... :/
    /// </summary>
    public void PlayParticles()
    {
        var particles = GetComponent<ParticleSystem>();
        if (particles != null)
        {
            if (!particles.isPlaying)
            {
                particles.Play();
            }
        }
    }
}
