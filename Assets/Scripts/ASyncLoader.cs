using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class ASyncLoader : MonoBehaviour
{
    private void LoadLevelBtn()
    {
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
        UIManager.Instance.gameObject.SetActive(true);
        AudioManager.Instance.PlayMusic("Theme");
    }
    private void Start()
    {
        LoadingScreen.Instance.OnLoadingFinished();
    }
    private void NewGame()
    {
        SOAssetReg.Instance.MainSaveData.Value.ZombieMax = 0;
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent = 1;
        SOAssetReg.Instance.MainSaveData.Value.ZombieDeath = 0;
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
