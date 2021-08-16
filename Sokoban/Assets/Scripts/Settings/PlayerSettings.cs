using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private const string MUSIC_LEVEL_KEY = "MusicLevel";
    private const string EFFECT_LEVEL_KEY = "EffectsLevel";

    private float musicLevel;
    private float effectsLevel;

    private static PlayerSettings instance;

    public static PlayerSettings Instance { get { return instance; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMusicLevel()
    {
        if (PlayerPrefs.HasKey(MUSIC_LEVEL_KEY))
        {
            return PlayerPrefs.GetFloat(MUSIC_LEVEL_KEY);
        }
        else
        {
            return 0.5f;
        }
    }

    public void SetMusicLevel(float musicLevelValue)
    {
        PlayerPrefs.SetFloat(MUSIC_LEVEL_KEY, musicLevelValue);
        PlayerPrefs.Save();
    }

    public float GetEffectsLevel()
    {
        if (PlayerPrefs.HasKey(EFFECT_LEVEL_KEY))
        {
            return PlayerPrefs.GetFloat(EFFECT_LEVEL_KEY);
        }
        else
        {
            return 1.0f;
        }
    }

    public void SetEffectsLevel(float effectsLevelValue)
    {
        PlayerPrefs.SetFloat(EFFECT_LEVEL_KEY, effectsLevelValue);
        PlayerPrefs.Save();
    }
}
