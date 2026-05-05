using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;

    public void Init(float speed, float lifeTime)
    {
        this.speed = speed;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}