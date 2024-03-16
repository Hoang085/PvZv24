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
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void QuitlV()
    {
        SceneManager.LoadScene(0);
    }


}