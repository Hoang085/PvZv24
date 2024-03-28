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

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    public void BuyPlant()
    {
        if(SOAssetReg.Instance.MainSaveData.Value.SunAmount >= price)
        {
            //SOAssetReg.Instance.stringName.Raise(price.ToString());
            gameManager.BuyPlant(plantObject, plantSprite, price);
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

   
