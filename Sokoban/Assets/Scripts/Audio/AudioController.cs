using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the playing of audio files. Subscribes to events sent by MapController and PlayerTile.
/// </summary>
public class AudioController : MonoBehaviour
{
    public AudioSource levelPlayMusic;

    public AudioSource levelCompletedMusic;

    public AudioSource playerMovedEffect;

    private void Awake()
    {
        //Get audio volumes from PlayerPrefs
        levelPlayMusic.volume = PlayerSettings.Instance.GetMusicLevel();
        levelCompletedMusic.volume = PlayerSettings.Instance.GetMusicLevel();
        playerMovedEffect.volume = PlayerSettings.Instance.GetEffectsLevel();

        MapController.OnLevelStarted += LevelStarted;
        MapController.OnLevelCompleted += LevelCompleted;
        PlayerTile.OnMoveMade += PlayerMoved;
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
        PlayerTile.OnMoveMade -= PlayerMoved;
    }

    private void LevelStarted(PlayerTile player)
    {
        //Get audio source from player tile.
        playerMovedEffect = player.GetComponent<AudioSource>();

        levelCompletedMusic.Stop();
        levelPlayMusic.Play();
    }

    private void LevelCompleted(int levelID)
    {
        levelPlayMusic.Stop();
        levelCompletedMusic.time = 4.0f;//Sound starts from 0:04
        levelCompletedMusic.Play();
    }

    private void PlayerMoved()
    {
        playerMovedEffect.Play();
    }
}
