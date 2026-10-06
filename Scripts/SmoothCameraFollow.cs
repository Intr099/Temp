using UnityEngine;

// Q3: Smooth third-person follow camera (no instant snapping)
public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3f, -6f);
    public float positionSmoothTime = 0.2f;
    public float lookHeight = 1.2f;

    Vector3 velocity;

    void LateUpdate()
    {
        if (!target) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, positionSmoothTime);
        Quaternion lookRot = Quaternion.LookRotation((target.position + Vector3.up * lookHeight) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 8f * Time.deltaTime);
    }
}
