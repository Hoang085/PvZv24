using PVZ.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManualSingletonMono<UIManager>
{
    [SerializeField] private Slider ZombieBar;
    private void OnEnable()
    {
        SOAssetReg.Instance.ZombieSpawnCountEvent.AddListener(ProcessZombie);
    }
    private void OnDisable()
    {
        SOAssetReg.Instance.ZombieSpawnCountEvent.RemoveListener(ProcessZombie);
    }
    private void Start()
    {
        ZombieBar.maxValue = SOAssetReg.Instance.MainSaveData.Value.ZombieMax;
    }

    void ProcessZombie(float count)
    {
        ZombieBar.value += count;
    }
}
