using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Zombies : MonoBehaviour
{
    public GameEventBase LoseEvent;
    public GameEventBase WinEvent;
    public GameEventBase<int> ZombieDeath;

    private float speed = 0.006f;
    private float Health;
    private float damage;
    private float range;
    private float eatCooldown;
    private AudioSource source;
    private bool isStop = false;
    private int count = 0;

    public Plant targetPlant;
    public ZombieType type;

    private void OnEnable()
    {
        ZombieDeath.AddListener(CountZombieDeath);

    }
    private void OnDisable()
    {
        ZombieDeath.RemoveListener(CountZombieDeath);
    }

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
        if (!targetPlant)
        {
            isStop = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
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
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySFX("loseSound");
            LoseEvent.Raise();
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
        if (!isStop)
            transform.position -= new Vector3(speed, 0, 0);
    }

    public void ReceiveDamge(float Damage, bool freeze)
    {
        source.PlayOneShot(type.hitClips[Random.Range(0, type.hitClips.Length)]);
        Health -= Damage;
        if (Health >= 10)
        {
            GetComponent<SpriteRenderer>().sprite = type.sprite;
        }
        else if (Health <= 1)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
        }
        else if (Health < 10)
        {
            GetComponent<SpriteRenderer>().sprite = type.defaultSprite;
        }
        if (freeze)
        {
            Freeze();
        }
        if (Health <=0)
        {
            Health = type.health;
            ZombieDeath.Raise(++count);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void CountZombieDeath(int data)
    {
        count = data;
    }

    void Freeze()
    {
        CancelInvoke(nameof(unFreeze));
        GetComponent<SpriteRenderer>().color = Color.blue;
        speed = type.speed / 2;
        Invoke(nameof(unFreeze), 5);
    }

    void unFreeze()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        speed = type.speed;
    }
}