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
        GetComponent<Button>().onClick.AddListener(selectSholve);
    }

    void selectSholve()
    {
        SOAssetReg.Instance.shovelStringName.Raise(shovelSprite.name + "_" + shovelObject.name);
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
