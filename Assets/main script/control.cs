using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public float jumpForce = 2f;
    public float speed = 8f; // The speed at which the object moves
    private Rigidbody rb;
    public GameObject ball;
    //[SerializeField]private Transform child_pos;
    private Quaternion rotationMove;
    ballplay playerscr;
    Animator animator;


    void Start()
    {
        // Assign the player's Rigidbody component to the variable
        rb = GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();
        playerscr=GetComponent<ballplay>();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y+90, transform.rotation.z);
        //rotationMove =Quaternion.Euler(transform.rotation.x, transform.rotation.y+90, transform.rotation.z);
        
    }

    void Update()
    {
        // Get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        ///transform.Translate(new Vector3(horizontalInput, 0, verticalInput)* speed * Time.deltaTime);
        //transform.Translate(new Vector3(verticalInput, 0,-horizontalInput)* speed * Time.deltaTime);
        //transform.Translate(new Vector3(horizontalInput, 0, verticalInput)* speed * Time.deltaTime);
        rb.velocity=new Vector3(verticalInput, 0, -horizontalInput)* speed * Time.deltaTime;
        //Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 movement = new Vector3(verticalInput, 0,-horizontalInput).normalized;
        
        //Quaternion rotationMove = Quaternion.LookRotation(movement);
        
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement), rotateSpeed * Time.deltaTime);
        
        //
        if (movement == Vector3.zero)
        {
            rotationMove  = transform.rotation;
        }
        //rotationMove= Quaternion.slerp( transform.rotation,rotationMove,360* Time.fixedDeltaTime);
        else{
        Quaternion rotationMove = Quaternion.LookRotation(movement);    
        rotationMove= Quaternion.RotateTowards( transform.rotation,rotationMove,360* Time.fixedDeltaTime);
        

        rb.MoveRotation(rotationMove);   
        
         }
        
        if (horizontalInput!=0 || verticalInput!=0)
        {
            // Set the "isRunning" parameter to true
            animator.SetBool("toRun", true);
            //Debug.Log(horizontalInput);
        }
        else
        {
            // Set the "isRunning" parameter to false
            animator.SetBool("toRun", false);
        }
        if (playerscr.carryingBallplayer && Input.GetKey("left shift")){
            //ball.transform.SetParent(this.transform);
            animator.SetBool("spinSkill",true);
            
            //transform.position=transform.position + new Vector3(0.0003332f,0.84914f,3.4038f);
        }
        else{
            //ball.transform.SetParent(null);
            //ball.transform.SetParent(this.transform,false);
            animator.SetBool("spinSkill",false);
        }

        
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        //rb.AddForce(movement * speed);
        //rb.velocity(movement * speed);

        //if (Input.GetKeyDown(KeyCode.Space))
    //{
        
        // Apply an upward force to the player
        //rb.AddForceAtPosition(transform.up * jumpForce, ForceMode.Impulse);
        //transform.Translate(Vector3.up * jumpForce, ForceMode.Impulse);
   // }
    }
    
}
