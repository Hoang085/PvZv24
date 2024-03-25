using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popupMenu;
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
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
        Time.timeScale = 1;
    }
    public void QuitlV()
    {
        AudioManager1.Instance.musicSource.Stop();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void NextLV()
    {
        SOAssetReg.Instance.MainSaveData.Value.LevelCurrent += 1;
        SceneManager.LoadSceneAsync($"Level {SOAssetReg.Instance.MainSaveData.Value.LevelCurrent}");
    }

}
