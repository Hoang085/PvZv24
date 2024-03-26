using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField]
    private LayerMask zombieMask;

    public float delay = 3f;
    public float radius = 0.5f;
    public GameObject explosionEffect;

    float coutdown;
    bool hasExploded = false;

    private List<Zombies> objectToBomb = new List<Zombies>(); 

    // Start is called before the first frame update
    void Start()
    {
        coutdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        coutdown -= Time.deltaTime;
        if (coutdown <= 0f && !hasExploded)
        {
            hasExploded = true;
            Explode();
        }
        Vector2 objPos = gameObject.transform.position;
    }
    void Explode()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(explosionEffect, gameObject.transform);

        Collider2D[] col = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, zombieMask);

        foreach (Collider2D nearbyObject in col)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            Zombies zombies = nearbyObject.GetComponent<Zombies>();
            //objectToBomb.Add(zombies);
            zombies.ReceiveDamge(1000f, false);
        }

        StartCoroutine(waittoBomb());
    }
    IEnumerator waittoBomb()
    {
        yield return new WaitForSeconds(1f);
        Plant plant = GetComponent<Plant>();
        plant.ReceiveDamage(10000f);
    }
}
