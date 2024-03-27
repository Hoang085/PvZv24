using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask shootMask;
    [SerializeField] bool isProjectTile;
    public float Damage;
    public bool freeze;

    private float Speed =1.2f;
    private float destroyTime = 10f;
    private Coroutine _returnToPoolTimerCoroutine;

    private float tParam;
    private Vector2 objPos;
    private GameObject target;

    void Start()
    {
        tParam = 0;
    }

    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 15, shootMask);
        if (hit.collider)
        {
           target = hit.collider.gameObject;
        }
        if (gameObject.transform.position.x > 9.7f)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        if (isProjectTile)
        {
            StartCoroutine(GoByTheRoute());
        }
        else
        {
            transform.position += new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
        }
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while(elapsedTime <destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombies>(out Zombies zombie))
        {
            zombie.ReceiveDamge(Damage,freeze);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private IEnumerator GoByTheRoute()
    {

        Vector2 p0 = gameObject.transform.position;
        Vector2 p1 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y+0.2f);
        Vector2 p2 = new Vector2(target.transform.position.x, target.transform.position.y+0.2f );
        Vector2 p3 = target.transform.position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * 0.0005f;
            objPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = objPos;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
    }
}
 