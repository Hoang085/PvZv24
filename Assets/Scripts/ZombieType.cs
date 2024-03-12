using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New ZombieType",menuName ="Zombie")]
public class ZombieType : ScriptableObject
{
    public float health;

    public float damage;

    public float speed;
    
    public float range = .5f;

    public float eatCooldown = 1f;

    public Sprite sprite;

    public Sprite deathSprite;

    public ZombieType type;
    public int probability;

    public AudioClip[] hitClips;

}
