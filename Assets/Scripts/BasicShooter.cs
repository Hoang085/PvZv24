using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter : MonoBehaviour
{

    public GameObject bullet;
    public Transform shootOrigin;
    public float cooldown;
    public bool isRepeat;

    public float range;
    public LayerMask shootMask;

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
    IEnumerator waitForShoot()
    {
        yield return new WaitForSeconds(0.3f);
        ObjectPoolManager.SpawnObject(bullet, shootOrigin.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);
    }
}
