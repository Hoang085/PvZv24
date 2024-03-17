using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    private float speed=0.006f;

    private float Health;
    private float damage;
    private float range;
    private float eatCooldown;
    private AudioSource source;
    private bool isStop = false;

    public Plant targetPlant;
    public LayerMask plantMask;
    public ZombieType type;
    

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCooldown = type.eatCooldown;

        GetComponent<SpriteRenderer>().sprite = type.sprite;
    }

    private void Update()
    {
        if (Health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
        }
        if(!targetPlant)
        {
            isStop = false;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 9)
        {
            targetPlant = other.gameObject.GetComponent<Plant>();
            StartCoroutine(Eat(other));
            isStop = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 14)
        {
            Time.timeScale = 0;
            AudioManager1.Instance.musicSource.Stop();
            AudioManager1.Instance.PlaySFX("loseSound");
            SOAssetReg.Instance.MainSaveData.Value.LoseEvent.Raise();
        }
    }
    IEnumerator Eat(Collision2D other)
    {
        targetPlant.ReceiveDamage(damage);
        yield return new WaitForSeconds(eatCooldown);
        StartCoroutine(Eat(other));
    }
    private void FixedUpdate()
    {
        if(!isStop)
            transform.position -= new Vector3(speed, 0, 0); 
    }

    public void ReceiveDamge(float Damage, bool freeze)
    {
        source.PlayOneShot(type.hitClips[Random.Range(0, type.hitClips.Length)]);
        Health -= Damage;
        if (freeze)
        {
            Freeze();
        }
        if(Health <= 0) 
        {
            Destroy(gameObject);
        }
    }
    void Freeze()
    {
        CancelInvoke("UnFreeze");
        GetComponent<SpriteRenderer>().color = Color.blue;
        speed = type.speed / 2;
        Invoke("UnFreeze", 5);
    }
    void unFreeze()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        speed = type.speed;
    }

}
