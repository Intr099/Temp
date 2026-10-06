using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public float minX = -10f; // The minimum x coordinate of the boundary
    public float maxX = 10f; // The maximum x coordinate of the boundary
    public float minZ = -10f; // The minimum z coordinate of the boundary
    public float maxZ = 10f; // The maximum z coordinate of the boundary
    private Vector3 lastplace;

    void OnTriggerEnter(Collider other)
{
    // Do something when an object enters the boundary
    
}

void OnTriggerExit(Collider other)
{
    // Do something when an object exits the boundary
    //lastplace=other.transform.position;
    //Debug.Log(lastplace);
}
    
    
    void Update()
    {
        // Clamp the X and Z values of the position to keep it inside the specified range
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );
    }
}