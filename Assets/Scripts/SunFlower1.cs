using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SunFlower1 : MonoBehaviour
{
    public GameObject sunObject;
    public float coolDown;

    private void Start()
    {
        InvokeRepeating("SpawnSun", coolDown, coolDown);
    }

    void SpawnSun()
    {
        GameObject mySun = ObjectPoolManager.SpawnObject(sunObject,transform.position,Quaternion.identity,ObjectPoolManager.PoolType.Sun);
        mySun.GetComponent<Sun>().droptoYPos = transform.position.y - 1;
    }
    
}
