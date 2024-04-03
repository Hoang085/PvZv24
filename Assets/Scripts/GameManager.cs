using PVZ.Utils;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : ManualSingletonMono<GameManager>
{
    public GameEventBase<int> updateSun;
    public GameEventBase<int> SunPrice;
    public GameEventBase<string> GameObjectName;
    public GameEventBase<string> ShovelName;

    [SerializeField]
    private LayerMask tileMask;
    [SerializeField]
    private LayerMask plantMask;

    private GameObject currentPlant;
    private Sprite currentPlantSprite;
    private GameObject currentShovel;
    private GameObject curPlant;
    private GameObject curShovel;
    private int PricePlant;

    [SerializeField] private List<GameObject> listPlant;
    [SerializeField] private List<Sprite> listSpritePlant;
    [SerializeField] private List<GameObject> listShovel;

    private void OnEnable()
    {
        SunPrice.AddListener(ReceiPrice);
        GameObjectName.AddListener(ReceiPlant);
        ShovelName.AddListener(ReceiShovel);
    }

    private void OnDisable()
    {
        SunPrice.RemoveListener(ReceiPrice);
        GameObjectName.RemoveListener(ReceiPlant);
        ShovelName.RemoveListener(ReceiShovel);
    }

    public void MakeObject()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        curPlant = Instantiate(currentPlant, objPos, Quaternion.identity);
    }
    private void ReceiPlant(string data)
    {
        currentPlant = listPlant.Find(s => s.name == data);
        MakeObject();
    }
    private void ReceiShovel(string data)
    {
        currentShovel = listShovel.Find(s => s.name == data);
        selectSholve();
    }
    public void selectSholve()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 shovelPos = Camera.main.ScreenToWorldPoint(mousePos);
        curShovel = Instantiate(currentShovel, shovelPos, Quaternion.identity);
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
                AudioManager.Instance.PlaySFX("setPlant");
                hit.collider.GetComponent<Tile>().HasPlant = true;
                curPlant.GetComponent<Plant>().tile = hit.collider.GetComponent<Tile>();
                curPlant = null;
                currentPlantSprite = null;
                currentPlant = null;
                updateSun.Raise(-PricePlant);
            }
            else if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Tile>())
            {
                Destroy(curPlant);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(curPlant);
                currentPlantSprite = null;
                currentPlant = null;
            }
        }
        if (hit.collider && curShovel)
        {
            curShovel.transform.position = hit.collider.gameObject.transform.position;
            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().HasPlant)
            {
                Destroy(curShovel);
            }
        }
        RaycastHit2D hitShovel = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, plantMask);
        if (hitShovel.collider && curShovel != null)
        {
            curShovel.transform.position = hitShovel.collider.gameObject.transform.position;
            if (Input.GetMouseButtonDown(0) && hitShovel.collider.gameObject.layer == 9)
            {
                Destroy(curShovel);
                Destroy(hitShovel.collider.gameObject);
                hit.collider.GetComponent<Tile>().HasPlant = false;
            }
        }
    }
    private void ReceiPrice(int price)
    {
        PricePlant = price;
    }


}
