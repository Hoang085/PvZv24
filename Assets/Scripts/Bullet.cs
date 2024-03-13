using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public bool freeze;
    private float Speed =0.8f;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    private void Update()
    {
        transform.position += new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombies>(out Zombies zombie))
        {
            zombie.ReceiveDamge(Damage,freeze);
            Destroy(gameObject);
        }
    }
}
 