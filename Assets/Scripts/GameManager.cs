//using PVZ.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public GameObject currentPlant;
    public Sprite currentPlantSprite;
    public Transform tiles;

    public LayerMask tileMask;
    public LayerMask sunMask;

    public int suns;
    public TextMeshProUGUI sunText;

    
    public void BuyPlant(GameObject plant, Sprite sprite)
    {
        currentPlant = plant;
        currentPlantSprite = sprite;    
    }

    private void Update()
    {
        sunText.text = suns.ToString();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        foreach (Transform tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (hit.collider && currentPlant)
        {
            
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlantSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;
            print("do");

            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Tile>().HasPlant)
            {
                Instantiate(currentPlant, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().HasPlant = true;
                currentPlantSprite = null;
                currentPlant = null;
            }
        }

        RaycastHit2D hitSun = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sunMask);
        if (hitSun.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                suns += 25;
                Destroy(hitSun.collider.gameObject);
            }
        }
    }

}
