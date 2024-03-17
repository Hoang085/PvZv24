/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 10)
        {
            Time.timeScale = 0;
            AudioManager1.Instance.musicSource.Stop();
            AudioManager1.Instance.PlaySFX("loseSound");
            SOAssetReg.Instance.MainSaveData.Value.LoseEvent.Raise();
        }
    }
}
*/