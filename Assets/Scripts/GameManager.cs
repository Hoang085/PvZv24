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
    public LayerMask sunMask;

    public int suns ;
    public TextMeshProUGUI sunText;

    private GameObject curPlant;

    public void BuyPlant(GameObject plant, Sprite sprite, int pricePlant)
    {
        currentPlant = plant;
        currentPlantSprite = sprite;
        PricePlant = pricePlant;

        Vector2 mousePos = Input.mousePosition;
        Vector2 plantPos = Camera.main.ScreenToWorldPoint(mousePos);
        curPlant = Instantiate(currentPlant,plantPos,Quaternion.identity);
    }

    private void Update()
    {
        sunText.text = suns.ToString();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        if (hit.collider && currentPlant)
        {
            curPlant.transform.position = hit.collider.gameObject.transform.position;
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlantSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;
            
            print("do");

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().HasPlant)
            {
                FindObjectOfType<AudioManager>().Play("setPlant");
                curPlant = null;
                //Instantiate(currentPlant, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().HasPlant = true;
                currentPlantSprite = null;
                currentPlant = null;
                suns -= PricePlant;

            }
            else if(Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Tile>()) 
            {
                Destroy(curPlant);
            }
        } 
    }

}
