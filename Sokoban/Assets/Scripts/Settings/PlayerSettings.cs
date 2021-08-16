using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player settings (audio levels).
/// </summary>
public class PlayerSettings : MonoBehaviour
{
    private const string MUSIC_LEVEL_KEY = "MusicLevel";
    private const string EFFECT_LEVEL_KEY = "EffectsLevel";

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

    /// <summary>
    /// Sets default value if keys don't exist.
    /// </summary>
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

    /// <summary>
    /// Gets music audio level.
    /// </summary>
    /// <returns></returns>
    public float GetMusicLevel()
    {
        return PlayerPrefs.GetFloat(MUSIC_LEVEL_KEY);
    }

    /// <summary>
    /// Sets music audio level.
    /// </summary>
    /// <param name="musicLevelValue"></param>
    public void SetMusicLevel(float musicLevelValue)
    {
        PlayerPrefs.SetFloat(MUSIC_LEVEL_KEY, musicLevelValue);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Gets effects audio level.
    /// </summary>
    /// <returns></returns>
    public float GetEffectsLevel()
    {
        return PlayerPrefs.GetFloat(EFFECT_LEVEL_KEY);
    }

    /// <summary>
    /// Sets effects audio level.
    /// </summary>
    /// <param name="effectsLevelValue"></param>
    public void SetEffectsLevel(float effectsLevelValue)
    {
        PlayerPrefs.SetFloat(EFFECT_LEVEL_KEY, effectsLevelValue);
        PlayerPrefs.Save();
    }
}
