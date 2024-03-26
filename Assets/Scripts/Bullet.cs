using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask shootMask;
    [SerializeField] float _InitialVeclocity;
    [SerializeField] float _Angle;
    [SerializeField] bool isProjectTile;
    public float Damage;
    public bool freeze;

    private Transform target;
    public float throwForce = 10f; // Lực ném
    public float heightOffset = 2f;// Độ cao mà đối tượng ném sẽ đạt được trước khi di chuyển tới mục tiêu

    private float Speed =1.2f;
    private float destroyTime = 10f;
    private Rigidbody2D rb;

    private Coroutine _returnToPoolTimerCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private void Update()
    {
        if (gameObject.transform.position.x > 9.7f)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        if (isProjectTile)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 15, shootMask);
            if (hit.collider)
            {
                target = hit.collider.gameObject.transform;
                Vector3 toTarget = target.position - transform.position;
                float distance = toTarget.magnitude;
                float g = Physics.gravity.y;

                // Tính toán vận tốc cần thiết để đạt được mục tiêu
                float verticalSpeed = Mathf.Sqrt(2 * (heightOffset + (toTarget.y / -g)) * Mathf.Abs(g));
                float horizontalSpeed = Mathf.Sqrt(distance / (1 / ((Mathf.Abs(g) / 2) * distance / throwForce) + 1));

                // Áp dụng lực ném
                Vector3 velocity = new Vector3(horizontalSpeed * Mathf.Sign(toTarget.x), verticalSpeed, horizontalSpeed * Mathf.Sign(toTarget.z));
                rb.velocity = velocity;

                // Điều chỉnh hướng của đối tượng
                transform.LookAt(target);
            }
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
    /*void ThrowToTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 15, shootMask);
        Vector3 toTarget = hit.collider.gameObject.transform.position - transform.position;
        float distance = toTarget.magnitude;
        float g = Physics.gravity.y;

        // Tính toán vận tốc cần thiết để đạt được mục tiêu
        float verticalSpeed = Mathf.Sqrt(2 * (heightOffset + (toTarget.y / -g)) * Mathf.Abs(g));
        float horizontalSpeed = Mathf.Sqrt(distance / (1 / ((Mathf.Abs(g) / 2) * distance / throwForce) + 1));

        // Áp dụng lực ném
        Vector3 velocity = new Vector3(horizontalSpeed * Mathf.Sign(toTarget.x), verticalSpeed, horizontalSpeed * Mathf.Sign(toTarget.z));
        rb.velocity = velocity;

        // Điều chỉnh hướng của đối tượng
        transform.LookAt(target);
    }*/

}
 