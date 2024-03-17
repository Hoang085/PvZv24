using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

public class DataHandler : PersistentSingleton<DataHandler>
{
    [BoxGroup("References", ShowLabel = false)]
    [TitleGroup("References/References")]
    public MainSaveDataVariable saveData;

    [BoxGroup("References", ShowLabel = false)] [TitleGroup("References/References")]
    public GlobalSetting globalSetting;

    protected override void Awake()
    {
        base.Awake();
        if (_instance == this)
        {
            saveData.Value = ES3.Load<MainSaveData>(GlobalInfo.LocalDataTags.MAIN_SAVE_DATA, saveData.Value);
        }
    }

    private void OnEnable()
    {
        saveData.AddListener(OnChangeMainData);
    }

    private void OnDisable()
    {
        saveData.RemoveListener(OnChangeMainData);
    }

    private void OnChangeMainData()
    {
        ES3.Save(GlobalInfo.LocalDataTags.MAIN_SAVE_DATA, saveData.Value);
    }
}