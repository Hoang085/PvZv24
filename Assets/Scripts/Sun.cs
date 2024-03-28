using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float droptoYPos;
    private float speed = .25f;
    private bool isOnTheMove;
    private float destroyTime = 3f;

    private void Start()
    {
        isOnTheMove = true;
        StartCoroutine(CheckMoving());
    }
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = gameObject.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 finalPos = gameObject.transform.position;
        isOnTheMove = startPos != finalPos?true:false;
        
        StartCoroutine(CheckMoving());
    }

    public void Update()
    {
        if(transform.position.y > droptoYPos)
        {
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
        }
        if (isOnTheMove == false)
        {
            StartCoroutine(ReturnToPoolAfterTime());
        }
    }
    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void OnMouseDown()
    {
        SOAssetReg.Instance.MainSaveData.Value.SunAmount += 25;
        SOAssetReg.Instance.updateSun.Raise();
        AudioManager1.Instance.PlaySFX("sunSound");
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    
}
