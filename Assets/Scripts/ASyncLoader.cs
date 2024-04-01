using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class ASyncLoader : MonoBehaviour
{
    public void LoadLevelBtn()
    {
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
    }
    private void Start()
    {
        LoadingScreen.Instance.OnLoadingFinished();
    }
    public void NewGame()
    {
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent = 1;
        SOAssetReg.Instance.MainSaveData.Value.ZombieDeath = 0;
        LoadLevelBtn();
    }
    private float WaitForSeconds(float v)
    {
        return Time.deltaTime * v;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
