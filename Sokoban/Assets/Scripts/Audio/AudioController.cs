using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource levelPlayMusic;

    public AudioSource levelCompletedMusic;

    public AudioSource playerMovedEffect;

    private void Awake()
    {
        levelPlayMusic.volume = PlayerSettings.Instance.GetMusicLevel();
        levelCompletedMusic.volume = PlayerSettings.Instance.GetMusicLevel();
        playerMovedEffect.volume = PlayerSettings.Instance.GetEffectsLevel();

        MapController.OnLevelStarted += LevelStarted;
        MapController.OnLevelCompleted += LevelCompleted;
        PlayerTerrainTile.OnMoveMade += PlayerMoved;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        MapController.OnLevelStarted -= LevelStarted;
        MapController.OnLevelCompleted -= LevelCompleted;
        PlayerTerrainTile.OnMoveMade -= PlayerMoved;
    }

    private void LevelStarted(PlayerTerrainTile player)
    {
        playerMovedEffect = player.GetComponent<AudioSource>();

        levelCompletedMusic.Stop();
        levelPlayMusic.time = 2.0f;
        levelPlayMusic.Play();
    }

    private void LevelCompleted(int levelID)
    {
        levelPlayMusic.Stop();
        levelCompletedMusic.Play();
    }

    private void PlayerMoved()
    {
        playerMovedEffect.Play();
    }
}
