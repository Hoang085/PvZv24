using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] SpawnPoint;

    public GameObject zombie;

    public void Start()
    {
        InvokeRepeating("SpawnZombie", 2, 1);
    }
    public void SpawnZombie()
    {
        int r = Random.Range(0,SpawnPoint.Length);
        GameObject myZombie = Instantiate(zombie, SpawnPoint[r].position,Quaternion.identity);
    }

}
