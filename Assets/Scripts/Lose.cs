using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    //public AnimationClip ani;
    

    public Animation ani;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            //Time.timeScale = 0;
            ani.Play("DeathAni");
        }
    }

}
