using PVZ.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : ManualSingletonMono<LoadingScreen>
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image progress;
    private float minLoadTime = 2f;

    public bool Loading { get; private set; }
     
    public void Start()
    {
        if (GlobalSetting.Instance.isTest)
        {
            LoadScene("test2");
        }
        else
        {
            LoadScene("Main Menu");
        }
    }
    public void LoadScene(string sceneName, Func<bool> launchCondition = null)
    {
        StartCoroutine(LoadSceneRoutine(sceneName, launchCondition));
    }

    IEnumerator LoadSceneRoutine(string sceneName, Func<bool> launchCondition)
    {
        UIManager.Instance.OnActive(false);
        if (Loading) yield break;
            Loading = true;
            canvas.gameObject.SetActive(true);

            var ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            ao.allowSceneActivation = false;

            var t = 0f;

            while (t < minLoadTime || ao.progress < 0.9f)
            {
                t += Time.unscaledDeltaTime;
                progress.fillAmount = Mathf.Min(t / minLoadTime, ao.progress / 0.9f);

                yield return null;
            }

            if (launchCondition != null)
            {
                yield return new WaitUntil(launchCondition);
            }

            ao.allowSceneActivation = true;
        OnLoadingFinished();
    }

    public void OnLoadingFinished()
    {
        Loading = false;
        gameObject.SetActive(false);
        //UIManager.Instance.OnActive(true);
    }
}
