using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    //public AnimationClip ani;
    
    public GameObject death;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 10)
        {
            Time.timeScale = 0;
            death.SetActive(true);
            FindObjectOfType<AudioManager>().Play("loseSound");
        }
    }

}
