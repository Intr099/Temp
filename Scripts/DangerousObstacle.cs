using UnityEngine;

// Q4: Attach to a hazard (spikes, lava, moving block). Works with Trigger colliders and normal collisions.
public class DangerousObstacle : MonoBehaviour
{
    public int damage = 25;
    public float damageCooldown = 1f;
    float lastHit = -10f;

    void OnTriggerEnter(Collider other) => TryDamage(other.gameObject);
    void OnCollisionEnter(Collision c) => TryDamage(c.gameObject);
    // CharacterController does not generate OnCollisionEnter on the obstacle -> use trigger volumes for hazards.

    void TryDamage(GameObject go)
    {
        if (!go.CompareTag("Player") || Time.time - lastHit < damageCooldown) return;
        lastHit = Time.time;
        go.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }
}
