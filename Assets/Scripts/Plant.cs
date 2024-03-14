using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public float health;
    private Tile tile;

    private void Start()
    {
        tile = GetComponent<Tile>();
        gameObject.layer = 9;

    }
    public void ReceiveDamage(float Damage)
    {
        health -= Damage;
        if(health <= 0)
        {
            Destroy(gameObject);
            tile.GetComponent<SpriteRenderer>().enabled = false;
            tile.HasPlant = false;
        }
    }

}
