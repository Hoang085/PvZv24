using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private LayerMask shootMask;
    [SerializeField] private float lauchForce = 10f;

    private bool canShoot;
    private GameObject target;


    private void Start()
    {
        Invoke(nameof(ResetCooldown), cooldown);
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
        Invoke(nameof(ResetCooldown), cooldown);
        ObjectPoolManager.SpawnObject(bullet, shootOrigin.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);
 
    }

}
