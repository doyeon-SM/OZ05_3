using UnityEngine;

public class TurretPitch : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float minPitch = -45f;
    [SerializeField] private float maxPitch = 20f;

    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 localDir = transform.parent.InverseTransformDirection(target.position - transform.position);

        if (localDir.sqrMagnitude < 0.001f)
        {
            return;
        }

        float targetPitch = -Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;
        targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);

        Quaternion targetRotation = Quaternion.Euler(targetPitch, 0f, 0f);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}