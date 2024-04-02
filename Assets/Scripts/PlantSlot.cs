using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantSlot : MonoBehaviour
{
    public GameEventBase<int> SunCurrent;
    public GameEventBase<int> SunPrice;
    public GameEventBase<string> GameObjectName;

    [SerializeField] private Sprite plantSprite;
    [SerializeField] private GameObject plantObject;
    [SerializeField] private int price;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI priceText;

    private int sunInStore;

    private void OnEnable()
    {
        SunCurrent.AddListener(sunCurrent);
    }
    private void OnDisable()
    {
        SunCurrent.RemoveListener(sunCurrent);
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }
    private void sunCurrent(int amount)
    {
        sunInStore = amount;
    }
    private void BuyPlant()
    {
        if(sunInStore >= price)
        {
            SunPrice.Raise(price);
            GameObjectName.Raise(plantObject.name);
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

   
