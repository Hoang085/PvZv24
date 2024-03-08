using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private float droptoYPos;
    private float speed = .25f;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-5f,8f),6,0);
        droptoYPos = Random.Range(-4.27f,3.34f);
        Destroy(gameObject, Random.Range(6f, 12f));
    }
    private void Update()
    {
        if(transform.position.y >= droptoYPos)
        {
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
        }
    }
}
