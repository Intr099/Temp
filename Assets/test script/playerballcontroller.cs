using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerballcontroller : MonoBehaviour
{
    // The ball object that the player will carry
    public GameObject ball;
    private GameObject player;

    // The distance at which the player can pick up the ball
    public float pickUpDistance = 2.0f;
    private bool withplayer=false;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is close enough to the ball to pick it up
        float distanceToBall = Vector3.Distance(transform.position, ball.transform.position);
        if (distanceToBall <= pickUpDistance)
        {
            // The player is close enough to pick up the ball, so make the ball a child of the player
            bool withplayer=true;
            ball.transform.parent = transform;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        player= gameObject;
        if (withplayer == true) {
        // Check if the ball has collided with an object other than the player
        if (collision.gameObject != player)
        {
            // The ball has collided with an object other than the player, so make the ball no longer a child of the player
            transform.parent = null;
        }}
    }
}