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
    private GameObject currentPlant;
    private Sprite currentPlantSprite;
    private int PricePlant;
    private GameObject currentShovel;

    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private LayerMask tileMask;
    [SerializeField]
    private LayerMask plantMask;
    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private TextMeshProUGUI sunText;

    private GameObject curPlant;
    private GameObject curShovel;

    [SerializeField] private List<GameObject> listPlant;
    [SerializeField] private List<Sprite> listSpritePlant;
    [SerializeField] private List<GameObject> listShovel;


    private void Start()
    {
        SOAssetReg.Instance.MainSaveData.Value.SunAmount = 75;
        SOAssetReg.Instance.updateSun.Raise();
        AudioManager1.Instance.PlayMusic("Theme");
    }
    private void OnEnable()
    {
        SOAssetReg.Instance.updateSun.AddListener(UpdateSunAmount);
        SOAssetReg.Instance.winEvent.AddListener(WinGame);
        SOAssetReg.Instance.loseEvent.AddListener(LoseGame);
        SOAssetReg.Instance.stringName.AddListener(receiData);
        SOAssetReg.Instance.shovelStringName.AddListener(receiShovel);
    }

    private void OnDisable()
    {
        SOAssetReg.Instance.updateSun.RemoveListener(UpdateSunAmount);
        SOAssetReg.Instance.winEvent.RemoveListener(WinGame);
        SOAssetReg.Instance.loseEvent.RemoveListener(LoseGame);
        SOAssetReg.Instance.stringName.RemoveListener(receiData);
        SOAssetReg.Instance.shovelStringName.RemoveListener(receiShovel);
    }

    public void BuyPlant()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 plantPos = Camera.main.ScreenToWorldPoint(mousePos);
        curPlant = Instantiate(currentPlant, plantPos, Quaternion.identity);
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
                AudioManager1.Instance.PlaySFX("setPlant");
                hit.collider.GetComponent<Tile>().HasPlant = true;
                curPlant.GetComponent<Plant>().tile = hit.collider.GetComponent<Tile>();
                curPlant = null;
                currentPlantSprite = null;
                currentPlant = null;
                SOAssetReg.Instance.MainSaveData.Value.SunAmount -= PricePlant;
                SOAssetReg.Instance.updateSun.Raise();

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
        if(hit.collider && curShovel)
        {
            curShovel.transform.position = hit.collider.gameObject.transform.position;
            if(Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().HasPlant)
            {
                Destroy(curShovel);
            }
        }
        RaycastHit2D hitShovel = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, plantMask);
        if(hitShovel.collider && curShovel != null)
        {
            curShovel.transform.position = hitShovel.collider.gameObject.transform.position;
            if (Input.GetMouseButtonDown(0) && hitShovel.collider.gameObject.layer == 9 )
            {
                Destroy(curShovel);
                Destroy(hitShovel.collider.gameObject);
                hit.collider.GetComponent<Tile>().HasPlant = false;
            }
        }
    }

    private void UpdateSunAmount()
    {
        sunText.text = SOAssetReg.Instance.MainSaveData.Value.SunAmount.ToString();
    } 
    private void WinGame()
    {
        AudioManager1.Instance.musicSource.Stop();
        AudioManager1.Instance.PlaySFX("winSound");
        winScreen.SetActive(true);
    }
    private void LoseGame()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    private void receiData(string namedata)
    {
        var data = namedata.Split("_");
        PricePlant = int.Parse(data[0]);
        currentPlant = listPlant.Find(s => s.name == data[1]);
        currentPlantSprite = listSpritePlant.Find(s => s.name == data[2]);
        BuyPlant();
    }
    private void receiShovel(string nameShovel)
    {
        var data = nameShovel.Split("_");
        currentShovel = listShovel.Find(s => s.name == data[1]);
        selectSholve();
    }


}
