using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float droptoYPos;
    private float speed = .25f;
    private GameManager gameManager;
    private bool isOnTheMove;
    private float destroyTime = 5f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isOnTheMove = true;
        StartCoroutine(CheckMoving());
    }
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = gameObject.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 finalPos = gameObject.transform.position;

        if (startPos != finalPos)
            isOnTheMove = true;

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
        SOAssetReg.Instance.MainSaveData.Value.UpdateSun.Raise();
        AudioManager1.Instance.PlaySFX("sunSound");
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    
}
