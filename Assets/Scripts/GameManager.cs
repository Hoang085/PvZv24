using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlant;
    public Sprite currentPlantSprite;
    public Transform tiles;
    public int PricePlant;

    public LayerMask tileMask;

    public TextMeshProUGUI sunText;

    private GameObject curPlant;
    private GameObject curShovel;

    public GameEvent UpdateSun;

    private void Start()
    {
        UpdateSun.AddListener(UpdateSunAmount);
    }

    private void OnDisable()
    {
        UpdateSun.RemoveListener(UpdateSunAmount);
    }

    public void BuyPlant(GameObject plant, Sprite sprite, int pricePlant)
    {
        currentPlant = plant;
        currentPlantSprite = sprite;
        PricePlant = pricePlant;

        Vector2 mousePos = Input.mousePosition;
        Vector2 plantPos = Camera.main.ScreenToWorldPoint(mousePos);
        curPlant = Instantiate(currentPlant,plantPos,Quaternion.identity);
    }
    
    public void selectSholve(GameObject shovel)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 shovelPos = Camera.main.ScreenToWorldPoint(mousePos);
        curShovel = Instantiate(shovel, shovelPos, Quaternion.identity);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);
        if (hit.collider && currentPlant)
        {
            curPlant.transform.position = hit.collider.gameObject.transform.position;
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlantSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().HasPlant)
            {
                AudioManager1.Instance.PlaySFX("setPlant");
                hit.collider.GetComponent<Tile>().HasPlant = true;
                curPlant.GetComponent<Plant>().tile = hit.collider.GetComponent<Tile>();

                curPlant = null;
                currentPlantSprite = null;
                currentPlant = null;
                SOAssetReg.Instance.MainSaveData.Value.SunAmount -= PricePlant;
                SOAssetReg.Instance.MainSaveData.Value.UpdateSun.Raise();

            }
            else if(Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Tile>()) 
            {
                Destroy(curPlant);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy (curPlant);
                currentPlantSprite = null;
                currentPlant = null;
            }
        }
        RaycastHit2D hitShovel = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);
        if(hitShovel.collider && curShovel != null)
        {
            curShovel.transform.position = hitShovel.collider.gameObject.transform.position;
            /*if(Input.GetMouseButtonDown (0) && hitShovel.collider.GetComponent<Tile>().HasPlant) 
            {
                Destroy(curShovel);
                Destroy(hitShovel.collider.gameObject);
            }
            else if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>())
            {
                Destroy(curShovel);
            }*/
        }
    }

    private void UpdateSunAmount()
    {
        sunText.text = SOAssetReg.Instance.MainSaveData.Value.SunAmount.ToString();
    } 

}
