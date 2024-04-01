using PVZ.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : ManualSingletonMono<UIManager>
{
    [SerializeField] private Slider ZombieBar;
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    private void OnEnable()
    {
        SOAssetReg.Instance.ZombieSpawnCountEvent.AddListener(ProcessZombie);
    }
    private void OnDisable()
    {
        SOAssetReg.Instance.ZombieSpawnCountEvent.RemoveListener(ProcessZombie);
    }

    void ProcessZombie(float count)
    {
        ZombieBar.maxValue = SOAssetReg.Instance.MainSaveData.Value.ZombieMax;
        print(ZombieBar.maxValue);
        ZombieBar.value += count;
    }
    public void OpenMenu()
    {
        popupMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void BacktoGame()
    {
        popupMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ResetLV()
    {
        AudioManager.Instance.musicSource.Play();
        SOAssetReg.Instance.MainSaveData.Value.ZombieMax = ZombieSpawner.Instance.zombieMax;
        ZombieBar.value = 0;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
        Time.timeScale = 1;
    }
    public void QuitlV()
    {
        ZombieBar.value = 0;
        popupMenu.SetActive(false);
        UIManager.Instance.gameObject.SetActive(false);
        AudioManager.Instance.musicSource.Stop();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void NextLV()
    {
        AudioManager.Instance.musicSource.Play();
        SOAssetReg.Instance.MainSaveData.Value.ZombieMax = ZombieSpawner.Instance.zombieMax;
        ZombieBar.value = 0; 
        winScreen.SetActive(false);
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent += 1;
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
    }
}
