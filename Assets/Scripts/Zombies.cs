using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    private float speed=0.008f;

    public float Health;
    public float damage;
    public float range;
    public LayerMask plantMask;

    private float eatCooldown=2f;

    private bool canEat = true;
    public Plant targetPlant;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, plantMask);

        if (hit.collider)
        {
            targetPlant = hit.collider.GetComponent<Plant>();
            Eat();
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
