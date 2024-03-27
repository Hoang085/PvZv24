using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootOrigin;
    public float cooldown;

    public float range;
    public LayerMask shootMask;

    public float lauchForce = 10f;

    private bool canShoot;
    private GameObject target;


    private void Start()
    {
        Invoke("ResetCooldown", cooldown);
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);
        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot();
        }
    }
    void ResetCooldown()
    {
        canShoot = true;
    }
    void Shoot()
    {
        if (!canShoot)
            return;
        canShoot = false;
        Invoke("ResetCooldown", cooldown);
        ObjectPoolManager.SpawnObject(bullet, shootOrigin.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);
 
    }

}