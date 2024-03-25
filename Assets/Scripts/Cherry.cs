using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField]
    private LayerMask zombieMask;
    public float delay = 3f;
    public float radius = 5f;
    public GameObject explosionEffect;

    float coutdown;
    bool hasExploded = false;

    private List<Collider2D> colliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        coutdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        coutdown -= Time.deltaTime;
        if (coutdown <= 0f && !hasExploded)
        {
            hasExploded = true;
            Explode();
        }
    }
    void Explode()
    {
        Instantiate(explosionEffect, gameObject.transform);
        if(Physics2D.OverlapCircle(gameObject.transform.position, radius))
        {

        }
        colliders.Add(Physics2D.OverlapCircle(gameObject.transform.position, radius,zombieMask));

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }
        //Destroy(gameObject);
    }
}
