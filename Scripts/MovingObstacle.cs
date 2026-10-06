using UnityEngine;

// Q4: Kinematic moving platform/obstacle (Rigidbody is Kinematic, moved in FixedUpdate)
[RequireComponent(typeof(Rigidbody))]
public class MovingObstacle : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(4, 0, 0);
    public float speed = 1f;
    Vector3 start; Rigidbody rb;

    void Awake() { rb = GetComponent<Rigidbody>(); rb.isKinematic = true; start = transform.position; }
    void FixedUpdate() => rb.MovePosition(start + moveOffset * Mathf.PingPong(Time.time * speed, 1f));
}
