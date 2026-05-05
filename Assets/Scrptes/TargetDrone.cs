using UnityEngine;

public class TargetDrone : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;

    private Transform railPivot;

    void Start()
    {
        railPivot = transform.parent;
    }

    void Update()
    {
        railPivot.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}