using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{

    public GameObject sunObject;

    private void Start()
    {
        SpawnSun();
    }
    public void SpawnSun()
    {
        Instantiate(sunObject);
        Invoke("SpawnSun", Random.Range(4, 10));
    }

}
