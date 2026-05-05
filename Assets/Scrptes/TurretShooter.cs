using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform target;
    [SerializeField] private float fireAngleThreshold = 5f;
    [SerializeField] private float fireInterval = 0.5f;
    [SerializeField] private float projectileSpeed = 12f;
    [SerializeField] private float projectileLifeTime = 3f;

    private float fireTimer;

    void Update()
    {
        if (muzzlePoint == null || target == null)
        {
            return;
        }

        fireTimer -= Time.deltaTime;

        if (!IsAimed())
        {
            return;
        }

        if (fireTimer > 0f)
        {
            return;
        }

        Fire();
        fireTimer = fireInterval;
    }

    private bool IsAimed()
    {
        Vector3 toTarget = target.position - muzzlePoint.position;
        float angle = Vector3.Angle(muzzlePoint.forward, toTarget);
        return angle <= fireAngleThreshold;
    }

    private void Fire()
    {
        GameObject projectileObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        projectileObj.transform.position = muzzlePoint.position;
        projectileObj.transform.rotation = muzzlePoint.rotation;
        projectileObj.transform.localScale = Vector3.one * 0.2f;

        Projectile projectile = projectileObj.AddComponent<Projectile>();
        projectile.Init(projectileSpeed, projectileLifeTime);
    }
}