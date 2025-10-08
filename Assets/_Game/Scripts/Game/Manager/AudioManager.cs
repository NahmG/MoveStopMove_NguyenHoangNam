using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    AudioData audioData;
    [SerializeField]
    AudioMixer mixer;

    SettingData settingData;

    void Awake()
    {
        settingData = DataManager.Ins.Get<SettingData>();
    }

    public void PlaySFXClip(SFX_TYPE type, Transform spawnTF, float volume)
    {
        AudioObject source = HBPool.Spawn<AudioObject>(PoolType.AUDIO, spawnTF.position, Quaternion.identity);
        source.SetAudio(GetSFXAudio(type));

        //set clip volume
        source.SetVolume(volume);

        //play clip
        source.Play();

        //Get clip length
        float clipLength = source.ClipLength();

        //Destroy clip after done
        DOVirtual.DelayedCall(clipLength, () => HBPool.Despawn(source));
    }

    public bool IsSFXMute()
    {
        return settingData.IsSFXMute;
    }

    public void ToggleSFXVolume(bool isMute)
    {
        float volume = isMute ? -80 : 0;
        mixer.SetFloat("SFX", volume);

        settingData.IsSFXMute = isMute;
    }

    AudioClip GetSFXAudio(SFX_TYPE type)
    {
        return audioData.SfxAudioDict[type];
    }
}