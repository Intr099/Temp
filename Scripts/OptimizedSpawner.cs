using System.Collections.Generic;
using UnityEngine;

// Q5 (optimization): object pooling + cap on active objects instead of Instantiate/Destroy spam
public class OptimizedSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 200;
    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(prefab, transform); go.SetActive(false); pool.Enqueue(go);
        }
    }
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.hKey.wasPressedThisFrame)
            for (int i = 0; i < poolSize; i++)
            {
                var go = pool.Dequeue();
                go.transform.position = new Vector3(Random.Range(-40, 40), Random.Range(5, 25), Random.Range(-40, 40));
                go.SetActive(true); pool.Enqueue(go);   // recycle oldest
            }
    }
}
