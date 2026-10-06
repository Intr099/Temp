using UnityEngine;

// Q4: Interactive object - press E near it to open a door (disable door collider + move it)
public class InteractiveButton : MonoBehaviour
{
    public GameObject door;
    public float openHeight = 3f;
    bool playerNear, opened;

    void OnTriggerEnter(Collider o) { if (o.CompareTag("Player")) playerNear = true; }
    void OnTriggerExit(Collider o)  { if (o.CompareTag("Player")) playerNear = false; }

    void Update()
    {
        if (playerNear && !opened && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
            opened = true;
        if (opened && door.transform.position.y < openHeight)
            door.transform.Translate(Vector3.up * 2f * Time.deltaTime);
    }
}
