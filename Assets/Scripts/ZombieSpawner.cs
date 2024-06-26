using PVZ.Utils;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawner : ManualSingletonMono<ZombieSpawner>
{
    public IntGameEvent ZombieMaxEvent;
    public GameEventBase<int> ZombieDeath;
    public FloatGameEvent ZombieSpawnCountEvent;
    public GameEventBase WinEvent;

    [SerializeField] private Transform[] SpawnPoint;
    [SerializeField] private GameObject zombie;

    public ZombieTypeProb[] zombieTypes;
    public int zombieMax=0;

    private List<ZombieType> probList = new List<ZombieType>();
    private int zombiesSpawned;
    private float zombieDelay = 5;
    private int count;

    private void OnEnable()
    {
        ZombieDeath.AddListener(GetZombieDeath);
    }
    private void OnDisable()
    {
        ZombieDeath.RemoveListener(GetZombieDeath);
    }
    private void Start()
    {
        ZombieDeath.Raise(0);
        ZombieMaxEvent.Raise(zombieMax);
        InvokeRepeating(nameof(SpawnZombie), 15, zombieDelay);

        foreach (ZombieTypeProb zom in zombieTypes)
        {
            for(int i = 0; i< zom.probability;i++)
            {
                probList.Add(zom.type);
            }
        }
    }
    private void Update()
    {
        if (count == zombieMax)
            WinEvent.Raise();
    }

    public void SpawnZombie()
    {
        if (zombiesSpawned >= zombieMax)
        {
            return;
        }
        ZombieSpawnCountEvent.Raise(1);
        zombiesSpawned++;
        int r = Random.Range(0,SpawnPoint.Length);
        GameObject myZombie = ObjectPoolManager.SpawnObject(zombie, SpawnPoint[r].position,Quaternion.identity,ObjectPoolManager.PoolType.GameObject);
        myZombie.GetComponent<Zombies>().type = probList[Random.Range(0,probList.Count)];
    }

    private void GetZombieDeath(int data)
    {
        count = data;
    }
}

[System.Serializable]
public class ZombieTypeProb
{
    public ZombieType type;
    public int probability;
}