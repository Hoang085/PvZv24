using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private float cooldown;
    [SerializeField] private bool isRepeat;
    [SerializeField] private float range;
    [SerializeField] private LayerMask shootMask;

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
    private void ResetCooldown()
    {
        canShoot = true;
    }
    private void Shoot()
    {
        if (!canShoot)
            return;
        canShoot = false;
        Invoke(nameof(ResetCooldown), cooldown);

        if (isRepeat)
        {
            ObjectPoolManager.SpawnObject(bullet, shootOrigin.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);
            StartCoroutine(waitForShoot());
        }
        else
        {
            ObjectPoolManager.SpawnObject(bullet, shootOrigin.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);
        }
    }
    private IEnumerator waitForShoot()
    {
        yield return new WaitForSeconds(0.3f);
        ObjectPoolManager.SpawnObject(bullet, shootOrigin.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);
    }
}
