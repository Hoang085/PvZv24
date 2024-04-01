using PVZ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    bool adsInitialized;
    bool analyticsInitialized;
    bool LaunchCondition => adsInitialized && analyticsInitialized;

    private void Awake()
    {
        analyticsInitialized = true;
        adsInitialized = true;
    }

    void Start()
    {
        //UIManager.Instance.gameObject.SetActive(false);
        //LoadingScreen.Instance.LoadScene("Main Menu",()=>LaunchCondition);
    }

}
