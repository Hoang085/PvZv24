using PVZ.Utils;
using System.Collections;
using ScriptableObjectArchitecture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : ManualSingletonMono<UIManager>
{
    public GameEventBase<int> SunCurrent;
    public GameEventBase<int> updateSun;
    public GameEventBase WinEvent;
    public GameEventBase LoseEvent;

    [SerializeField] private Slider ZombieBar;
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private int currentSuns;
    [SerializeField] private TextMeshProUGUI sunText;

    private void OnEnable()
    {
        updateSun.AddListener(UpdateSunAmount);
        WinEvent.AddListener(WinGame);
        LoseEvent.AddListener(LoseGame);
        SOAssetReg.Instance.ZombieSpawnCountEvent.AddListener(ProcessZombie);
    }
    private void OnDisable()
    {
        updateSun.RemoveListener(UpdateSunAmount);
        WinEvent.RemoveListener(WinGame);
        LoseEvent.RemoveListener(LoseGame);
        SOAssetReg.Instance.ZombieSpawnCountEvent.RemoveListener(ProcessZombie);
    }
    private void Start()
    {
        sunText.text = currentSuns.ToString();
    }

    private void ProcessZombie(float count)
    {
        ZombieBar.maxValue = SOAssetReg.Instance.MainSaveData.Value.ZombieMax;
        print(ZombieBar.maxValue);
        ZombieBar.value += count;
    }
    private void OpenMenu()
    {
        popupMenu.SetActive(true);
        Time.timeScale = 0;
    }
    private void BacktoGame()
    {
        popupMenu.SetActive(false);
        Time.timeScale = 1;
    }
    private void ResetLV()
    {
        AudioManager.Instance.musicSource.Play();
        SOAssetReg.Instance.MainSaveData.Value.ZombieMax = ZombieSpawner.Instance.zombieMax;
        ZombieBar.value = 0;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        popupMenu.SetActive(false);
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
        Time.timeScale = 1;
    }
    private void QuitlV()
    {
        ZombieBar.value = 0;
        popupMenu.SetActive(false);
        UIManager.Instance.gameObject.SetActive(false);
        AudioManager.Instance.musicSource.Stop();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    private void NextLV()
    {
        AudioManager.Instance.musicSource.Play();
        SOAssetReg.Instance.MainSaveData.Value.ZombieMax = ZombieSpawner.Instance.zombieMax;
        ZombieBar.value = 0; 
        winScreen.SetActive(false);
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent += 1;
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
    }
    private void UpdateSunAmount(int count)
    {
        currentSuns += count;
        SunCurrent.Raise(currentSuns);
        sunText.text = currentSuns.ToString();
    }
    private void WinGame()
    {
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("winSound");
        winScreen.SetActive(true);
    }
    private void LoseGame()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }
    public void OnActive(bool isACtive)
    {
        gameObject.SetActive(isACtive);
    }
}

