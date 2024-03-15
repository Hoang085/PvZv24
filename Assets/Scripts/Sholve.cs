using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Sholve : MonoBehaviour
{
    public Sprite shovelSprite;
    public GameObject shovelObject;
    public Image Icon;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(selectSholve);
    }

    void selectSholve()
    {
        gameManager.selectSholve(shovelObject);
    }

    private void OnValidate()
    {
        if (shovelSprite)
        {
            Icon.enabled = true;
            Icon.sprite = shovelSprite;
        }
        else
        {
            Icon.enabled = false;
        }
    }


}
