using UnityEngine;

// Q5: Creates the "High-Density" scenario by spawning many physics cubes. Press H to spawn.
public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int count = 1000;
    public float area = 40f;

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.hKey.wasPressedThisFrame) Spawn();
    }
    void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 p = new Vector3(Random.Range(-area, area), Random.Range(5, 25), Random.Range(-area, area));
            Instantiate(prefab, p, Random.rotation);
        }
    }
}
