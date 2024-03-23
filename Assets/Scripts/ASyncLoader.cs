using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync());
    }

    IEnumerator LoadLevelASync()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            WaitForSeconds(10f);
            yield return null;
        }
    }

    public void NewGame()
    {
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent = 1;
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
