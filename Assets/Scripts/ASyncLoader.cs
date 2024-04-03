using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using ScriptableObjectArchitecture;

public class ASyncLoader : MonoBehaviour
{
    public GameEventBase<int> ZombieDeath;

    private void LoadLevelBtn()
    {
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
        AudioManager.Instance.PlayMusic("Theme");
        ZombieDeath.Raise(0);
        UIManager.Instance.OnActive(true);
    }
    private void Start()
    {
        LoadingScreen.Instance.OnLoadingFinished();
    }
    private void NewGame()
    {
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent = 1;
        LoadLevelBtn();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
