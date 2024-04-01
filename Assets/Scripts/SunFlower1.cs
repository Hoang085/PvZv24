using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SunFlower1 : MonoBehaviour
{
    [SerializeField] private GameObject sunObject;
    private float coolDown = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSun), coolDown, coolDown);
    }

    void SpawnSun()
    {
        GameObject mySun = ObjectPoolManager.SpawnObject(sunObject,transform.position,Quaternion.identity,ObjectPoolManager.PoolType.Sun);
        mySun.GetComponent<Sun>().droptoYPos = transform.position.y - 1;
    }
    
}
