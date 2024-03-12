using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    private float speed=0.006f;

    private float Health;
    private float damage;
    private float range;
    private float eatCooldown;
    private bool canEat = true;
    private AudioSource source;

    public Plant targetPlant;
    public LayerMask plantMask;
    public ZombieType type;
    

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCooldown = type.eatCooldown;

        GetComponent<SpriteRenderer>().sprite = type.sprite;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, plantMask);

        if (hit.collider)
        {
            targetPlant = hit.collider.GetComponent<Plant>();
            Eat();
        }
        if (Health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
        }
    }
    void Eat()
    {
        if (!canEat || !targetPlant)
            return;
        canEat = false;
        Invoke("resetEatCooldown", eatCooldown);

        targetPlant.ReceiveDamage(damage);
    }
    void resetEatCooldown()
    {
        canEat = true;
    }

    private void FixedUpdate()
    {
        if(!targetPlant)
            transform.position -= new Vector3(speed, 0, 0); 
    }

    public void ReceiveDamge(float Damage)
    {
        Health -= Damage;
        if(Health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
