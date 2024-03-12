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
    private GameObject curPlant;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    public void BuyPlant()
    {
        gameManager.BuyPlant(plantObject, plantSprite);
        print("Buy plant");

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

    /*public void OnDrag(PointerEventData eventData)
    {
        curPlant.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("a");
        Vector2 mousePos = new Vector2(-4, 4.5f);
        curPlant = Instantiate(plantObject,mousePos, Quaternion.identity);
        curPlant.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Destroy(curPlant);
    }*/
}

   
