using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public bool freeze;
    private float Speed =0.8f;
    private float destroyTime = 10f;

    private Coroutine _returnToPoolTimerCoroutine;



    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private void Update()
    {
        transform.position += new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
        if(gameObject.transform.position.x > 9.7f)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while(elapsedTime <destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombies>(out Zombies zombie))
        {
            zombie.ReceiveDamge(Damage,freeze);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            //Destroy(gameObject);
        }
    }
}
 