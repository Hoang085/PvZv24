using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject zombie;
    public ZombieTypeProb[] zombieTypes;
    private List<ZombieType> probList = new List<ZombieType>();
    public int zombieMax;
    public int zombiesSpawned;
    public float zombieDelay = 5;

    public void Start()
    {
        SOAssetReg.Instance.MainSaveData.Value.ZombieDeath = 0;
        SOAssetReg.Instance.MainSaveData.Value.ZombieMax = zombieMax;
        InvokeRepeating("SpawnZombie", 15, zombieDelay);

        foreach (ZombieTypeProb zom in zombieTypes)
        {
            for(int i = 0; i< zom.probability;i++)
            {
                probList.Add(zom.type);
            }
        }
    }
    public void SpawnZombie()
    {
        if (zombiesSpawned >= zombieMax)
        {
            return;
        }
        SOAssetReg.Instance.ZombieSpawnCountEvent.Raise(1);
        zombiesSpawned++;
        int r = Random.Range(0,SpawnPoint.Length);
        GameObject myZombie = ObjectPoolManager.SpawnObject(zombie, SpawnPoint[r].position,Quaternion.identity,ObjectPoolManager.PoolType.GameObject);
        myZombie.GetComponent<Zombies>().type = probList[Random.Range(0,probList.Count)];
    }
}

[System.Serializable]
public class ZombieTypeProb
{
    public ZombieType type;
    public int probability;
}