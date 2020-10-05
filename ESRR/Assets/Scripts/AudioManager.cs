using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
  [System.NonSerialized][HideInInspector]
  public static AudioManager Instance;
  public AudioMixer mixer;

  public const string FXVolumeKey = "FXVolume";
  public const string MusicVolumeKey = "MusicVolume";

  void Awake()
  {
    if(Instance != null)
    {
      Destroy(gameObject);
      return;
    }else{
      DontDestroyOnLoad(gameObject);
      Instance = this;

    }
    
  }

  void Start()
  {
    mixer.SetFloat(FXVolumeKey, FXVolume);
    mixer.SetFloat(MusicVolumeKey, MusicVolume);
  }

  public float FXVolume
  {
    get{
      return  PlayerPrefs.GetFloat(FXVolumeKey, 0.0f);
    }
    set{
      Debug.Log("[AudioManager] setting to: " +  value);
      PlayerPrefs.SetFloat(FXVolumeKey, value);
      PlayerPrefs.Save();
      mixer.SetFloat(FXVolumeKey, value);
    }
  }

  public float MusicVolume
  {
    get{
      return  PlayerPrefs.GetFloat(MusicVolumeKey, 0.0f);
    }
    set{
      PlayerPrefs.SetFloat(MusicVolumeKey, value);
      PlayerPrefs.Save();
      mixer.SetFloat(MusicVolumeKey, value);
    }
  }
}
