using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PVZ.Utils;

public class AudioManager : ManualSingletonMono<AudioManager>
{

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
