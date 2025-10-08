using UnityEngine;

public class AudioObject : GameUnit
{
    [SerializeField]
    AudioSource source;

    AudioClip currentAudio;

    public void SetAudio(AudioClip audio)
    {
        if (audio == null) return;
        source.clip = audio;
        currentAudio = audio;
    }

    public void SetVolume(float value) => source.volume = value;
    public float ClipLength() => currentAudio.length;

    public void Play()
    {
        source.Play();
    }

    public void Pause(bool isPause)
    {
        if (isPause)
            source.Pause();
        else
            source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }

}