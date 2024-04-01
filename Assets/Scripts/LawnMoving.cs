using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMoving : MonoBehaviour
{
    private float speed = 7f;
    private bool m_IsMoving;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 10)
        {
            AudioManager.Instance.PlaySFX("lawnSound");
            m_IsMoving = true;
            other.GetComponent<Zombies>().ReceiveDamge(1000,false);
            Destroy(this.gameObject, 8);
        }
    }
    private void Update()
    {
        if (m_IsMoving)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (gameObject.transform.position.x >= 18)
            Destroy(gameObject);
    }
}
