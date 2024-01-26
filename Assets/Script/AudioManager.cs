using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct CustomMapItem<Key, Value>
{
    public Key key;

    public Value[] Values;
}

[Serializable]
public class CustomMap<Key, Value>
{
    [SerializeField]
    private CustomMapItem<Key, Value>[] items;

    public List<Value> GetValues(Key key)
    {
        foreach (CustomMapItem<Key, Value> item in items)
        {
            if (item.key.Equals(key))
            {
                return item.Values.ToList();
            }
        }

        return new List<Value>(){};
    }
}

public enum EAudioSourceType
{
    SFX,
    ENVIRONEMENT,
    NONE,
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private CustomMap<EAudioSourceType, AudioSource> audioSources = new CustomMap<EAudioSourceType, AudioSource>();
    [SerializeField] private CustomMap<String, AudioClip> audioList;

    void Awake()
    {
        base.Awake();
    }

    List<AudioClip> GetSongsByType(String type)
    {
      return audioList.GetValues(type.ToLower());
    }

    public void PlaySongByTypeAndTag(string type, string tag, EAudioSourceType audioSourceType = 0)
    {
        List<AudioClip> audios = GetSongsByType(type);
        audioSources.GetValues(audioSourceType)[0].clip = audios.Find(song => song.name.ToLower().Contains(tag.ToLower()));
        audioSources.GetValues(audioSourceType)[0].Play();
    }

    public void PlayRandomSongByType(string type, EAudioSourceType audioSourceType = 0)
    {
        List<AudioClip> audios = GetSongsByType(type);
        audioSources.GetValues(audioSourceType)[0].clip = audios[Random.Range(0, audios.Count)];
        audioSources.GetValues(audioSourceType)[0].Play();
    }

    public void PauseSong(EAudioSourceType audioSourceType = 0)
    {
        AudioSource audio = audioSources.GetValues(audioSourceType)[0];
        if (audio.isPlaying)
        {
            audioSources.GetValues(audioSourceType)[0].Pause();
        }
    }

    public void ResumeSong(EAudioSourceType audioSourceType = 0)
    {
        AudioSource audio = audioSources.GetValues(audioSourceType)[0];
        if (!audio.isPlaying)
        {
            audioSources.GetValues(audioSourceType)[0].Play();
        }
    }
}