using UnityEngine;
using UnityEngine.SceneManagement;

// Q4: Health + respawn
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Transform respawnPoint;

    void Start() => health = maxHealth;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Player health: {health}");
        if (health <= 0) Die();
    }

    void Die()
    {
        // Option A: reload the scene  |  Option B: teleport to start (CharacterController must be disabled first)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnGUI() => GUI.Label(new Rect(10, 10, 200, 30), "Health: " + health);
}
