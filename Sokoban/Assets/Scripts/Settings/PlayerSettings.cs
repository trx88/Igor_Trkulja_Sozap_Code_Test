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

        //PlayerPrefs.DeleteAll();
        SetInitialSettingValues();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetInitialSettingValues()
    {
        if (!PlayerPrefs.HasKey(MUSIC_LEVEL_KEY))
        {
            PlayerPrefs.SetFloat(MUSIC_LEVEL_KEY, 1.0f);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey(EFFECT_LEVEL_KEY))
        {
            PlayerPrefs.SetFloat(EFFECT_LEVEL_KEY, 1.0f);
            PlayerPrefs.Save();
        }
    }

    public float GetMusicLevel()
    {
        return PlayerPrefs.GetFloat(MUSIC_LEVEL_KEY);
    }

    public void SetMusicLevel(float musicLevelValue)
    {
        PlayerPrefs.SetFloat(MUSIC_LEVEL_KEY, musicLevelValue);
        PlayerPrefs.Save();
    }

    public float GetEffectsLevel()
    {
        return PlayerPrefs.GetFloat(EFFECT_LEVEL_KEY);
    }

    public void SetEffectsLevel(float effectsLevelValue)
    {
        PlayerPrefs.SetFloat(EFFECT_LEVEL_KEY, effectsLevelValue);
        PlayerPrefs.Save();
    }
}
