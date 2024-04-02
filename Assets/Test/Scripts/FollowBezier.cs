using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBezier : MonoBehaviour
{

    private float tParam;
    private Vector2 objPos;
    private float Speed;
    private bool coroutineAllowed;

    void Start()
    {
        tParam = 0;
        Speed = 0.5f;
        coroutineAllowed = true;
    }

    void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute());
    }

    private IEnumerator GoByTheRoute()
    {
        coroutineAllowed = false;

        Vector2 p0 = new Vector2(-6f, 0);
        Vector2 p1 = new Vector2(-6f, 2);
        Vector2 p2 = new Vector2(2f, 2);
        Vector2 p3 = new Vector2(2f, 0);

        while (tParam < 1)
        {
            tParam += Time.deltaTime * Speed;

            objPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2+
                Mathf.Pow(tParam, 3) *p3;

            transform.position = objPos;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        coroutineAllowed = true;
    }
}
