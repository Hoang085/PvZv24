using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{

    public GameObject sunObject;

    private void Start()
    {
        StartCoroutine(Delay());
        
    }
    public void SpawnSun()
    {
        GameObject mySun = Instantiate(sunObject,new Vector3(transform.position.x + Random.Range(-4.5f,8), transform.position.y + 5.5f, 0),Quaternion.identity);
        mySun.GetComponent<Sun>().droptoYPos = Random.Range(-4.27f, 3.34f);
        Invoke("SpawnSun", Random.Range(4, 10));
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        SpawnSun();
    }
}
