using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movement2 : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public float rotationSpeed = 120f; // Rotation speed (degrees per second)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rotation due to physics
    }

    void Update()
    {
        // Get input from the keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction in local space
        Vector3 localMovement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        Vector3 movement = transform.TransformDirection(localMovement);

        // Move the player
        Vector3 moveVelocity = movement * speed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Rotate the player in the direction of movement
        if (localMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            //rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
            //rb.rotation = Quaternion.Lerp(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
