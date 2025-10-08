using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData/AudioData", order = 1)]
public class AudioData : SerializedScriptableObject
{
    [Title("SFX")]
    [SerializeField] private readonly Dictionary<SFX_TYPE, AudioClip> _sfxAudioDict;

    public Dictionary<SFX_TYPE, AudioClip> SfxAudioDict => _sfxAudioDict;
}

public enum SFX_TYPE
{
    NONE = 0,
    CLICK = 1,
    WEAPON_THROW = 2,
    WEAPON_HIT = 3,
    LVL_UP = 4,
    CHAR_DIE = 5,
    WIN = 6,
    LOSE = 7,
}