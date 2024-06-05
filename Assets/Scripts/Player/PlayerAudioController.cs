using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private List<GameSFX> gameSfxes = new();
    
    private readonly Dictionary<SFXType, AudioClip> _sfxDictionary = new();

    private void Awake()
    {
        foreach (GameSFX gameSfx in gameSfxes)
        {
            _sfxDictionary.Add(gameSfx.sfxType, gameSfx.sfxClip);
        }
        
        gameSfxes.Clear();
    }

    private void OnEnable()
    {
        GameActions.PlaySfxAction += PlaySFX;
    }

    private void OnDestroy()
    {
        GameActions.PlaySfxAction -= PlaySFX;
    }

    public void PlaySFX(SFXType sfxType)
    {
        if (!_sfxDictionary.ContainsKey(sfxType))
        {
            Debug.LogError("No such sfx file!");
            return;
        }
        
        AudioClip clipToPlay = _sfxDictionary[sfxType];
        _playerAudioSource.clip = clipToPlay;
        _playerAudioSource.Play();
    }
}

public enum SFXType
{
    Mine,
    PutCollectible,
    IngotCrafted,
    SwordCrafted,
    Swoosh,
    Hit,
    GameWon
}

[Serializable]
public struct GameSFX
{
    public SFXType sfxType;
    public AudioClip sfxClip;
}
