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

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private CustomMap<String, AudioClip> audioList;

    void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    List<AudioClip> GetSongsByType(String type)
    {
      return audioList.GetValues(type.ToLower());
    }

    public void PlaySongByTypeAndTag(string type, string tag)
    {
        List<AudioClip> audios = GetSongsByType(type);
        audioSource.clip = audios.Find(song => song.name.ToLower().Contains(tag.ToLower()));
        audioSource.Play();
    }

    public void PlayRandomSongByType(string type)
    {
        List<AudioClip> audios = GetSongsByType(type);
        audioSource.clip = audios[Random.Range(0, audios.Count)];
        audioSource.Play();
    }
}