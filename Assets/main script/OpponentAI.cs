using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    public Transform ball; // The ball's Transform component
    public float speed = 5f; // The speed at which the opponent moves
    public bool opponentaai= true;
    
    
    

    void Update()
    {
        if (opponentaai){
            Vector3 balldis=new Vector3(ball.position.x,transform.position.y,ball.position.z);
        // Calculate the direction in which the ball is moving
        Vector3 direction = balldis - transform.position;

        // Normalize the direction vector (make it a unit vector)
        direction = direction.normalized;

        // Move the opponent in the direction of the ball
        transform.position += direction * speed * Time.deltaTime;
        //Quaternion desiredRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        desiredRotation= Quaternion.RotateTowards( transform.rotation,desiredRotation,360* Time.fixedDeltaTime);
        transform.rotation = desiredRotation;
        
    }}
}
