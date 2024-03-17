using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    public static AudioManager1 Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds,x => x.nameSound == name);

        if(s == null)
        {
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.nameSound == name);

        if (s == null)
        {
            return;
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }


}
