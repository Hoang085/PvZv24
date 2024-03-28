using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantSlot : MonoBehaviour
{
    public Sprite plantSprite;
    public GameObject plantObject; 
    public int price;

    public Image Icon;
    public TextMeshProUGUI priceText;
    [SerializeField] private string nameObj;
    [SerializeField] private string spriteObj;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    public void BuyPlant()
    {
        if(SOAssetReg.Instance.MainSaveData.Value.SunAmount >= price)
        {
            SOAssetReg.Instance.stringName.Raise(price + "_" + nameObj + "_" + spriteObj);
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

   
