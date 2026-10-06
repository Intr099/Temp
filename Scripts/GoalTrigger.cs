using UnityEngine;

// Q4: Goal area (Is Trigger = true) -> loads next scene
public class GoalTrigger : MonoBehaviour
{
    public string nextSceneName = "Level2";
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Goal reached!");
            SceneLoader.Load(nextSceneName);
        }
    }
}
