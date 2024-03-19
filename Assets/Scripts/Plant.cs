using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public float health;

    public Tile tile;

    private void Start()
    {
        gameObject.layer = 9;
    }
    public void ReceiveDamage(float Damage)
    {
        health -= Damage;
        if(health <= 0)
        {
            tile.HasPlant = false;
            Destroy(gameObject);
        }
    }

}
