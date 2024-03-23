using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PopupSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    private void Start()
    {
        List<string> options = new List<string>();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("music", volume);
    }
    public void SetSFX(float sfx)
    {
        audioMixer.SetFloat("sfx", sfx);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
