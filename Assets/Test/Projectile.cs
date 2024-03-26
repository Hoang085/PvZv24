using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform target; // Mục tiêu cần ném tới
    public float launchAngle = 45f; // Góc ném
    public float projectileSpeed = 10f; // Tốc độ của vật thể

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        Vector3 projectileDirection = CalculateProjectileDirection();
        Vector3 launchVelocity = projectileDirection * projectileSpeed;
        GetComponent<Rigidbody2D>().velocity = launchVelocity;
    }

    Vector3 CalculateProjectileDirection()
    {
        Vector3 targetPosition = target.position;
        Vector3 launchPosition = transform.position;

        float distance = Vector3.Distance(targetPosition, launchPosition);
        float gravity = Physics.gravity.y;

        float angle = launchAngle * Mathf.Deg2Rad;

        float sinTheta = Mathf.Sin(angle);
        float cosTheta = Mathf.Cos(angle);

        float initialVelocity = Mathf.Sqrt((distance * gravity) / (Mathf.Sin(2 * angle)));

        Vector3 direction = (targetPosition - launchPosition).normalized;
        direction.y = distance * Mathf.Tan(angle);
        return direction.normalized;
    }
}