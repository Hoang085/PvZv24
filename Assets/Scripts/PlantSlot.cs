using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantSlot : MonoBehaviour
{
    [SerializeField] private Sprite plantSprite;
    [SerializeField] private GameObject plantObject;
    [SerializeField] private int price;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    private void BuyPlant()
    {
        if(SOAssetReg.Instance.MainSaveData.Value.SunAmount >= price)
        {
            SOAssetReg.Instance.stringName.Raise($"{price}_{plantObject.name}_{plantSprite.name}");
        }
        else
        {
            return;
        }
    }

    private void OnValidate()
    {
        if (plantSprite)
        {
            Icon.enabled = true;
            Icon.sprite = plantSprite;
            priceText.text = price.ToString();
        }
        else
        {
            Icon.enabled=false;
        }
        
    }

   
}

   
