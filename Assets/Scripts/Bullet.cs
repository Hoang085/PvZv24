using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    private float Speed =0.8f;

    private void Update()
    {
        transform.position += new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombies>(out Zombies zombie))
        {
            zombie.ReceiveDamge(Damage);
            Destroy(gameObject);
        }
    }
}
