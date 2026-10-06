using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballplay : MonoBehaviour
{
    // Drag and drop the ball prefab in the Inspector
    public GameObject ball;
    //public float force = 10f;
    public GameObject oponent;
    public float defenceForce = 100f;
    [SerializeField]private float delayTime = 2f;
    [SerializeField]private Rigidbody rbb;
    public Transform foot;
    public float kickForceplayer = 120f;
    public Transform playerback;
    public Transform playerfrount;
    Animator animator;
    bool kickAni=false;


    // The distance at which the player can pick up the ball
    public float pickupDistance = 2.0f;


    // A flag to track whether the player is carrying the ball
    public bool carryingBallplayer = false;
    //private bool nottackled = true;
    private bool nottackled = false;

    void Start(){
        animator=GetComponent<Animator>();
    }
    void Update()
    {   
        Debug.Log("st");
        Debug.Log(nottackled);
         Debug.Log("st2");
        Debug.Log(carryingBallplayer);
         Debug.Log("st3");
        // Check if the player is carrying the ball
        if (nottackled){
        if (carryingBallplayer)
        {

            Debug.Log("in leg");
            Vector3 kickDirection=playerfrount.position - playerback.position;
                //kickDirection = ball.transform.TransformDirection(kickDirection);
            kickDirection = kickDirection.normalized;
            // Update the ball's position to be the same as the player's
            //ball.transform.position = transform.position;    //
            //ball.transform.parent = transform;
            //ball.transform.parent=new Vector3(transform.position.x - 0.5f,  ball.transform.position.y,transform.position.z);
            //ball.transform.position = new Vector3(transform.position.x -1.0f,  transform.position.y-1.0f,transform.position.z);
           
            ball.transform.position= foot.position;//+ kickDirection ;//(-0.2f, 0.2f, 0.4f);//(-0.2f, -0.2f, 0.2f)///+ new Vector3(-0.2f, 0.1f, 0.4f)

            //ball.transform.SetParent(this.transform);
            if (Input.GetKey(KeyCode.Space)){
                animator.SetBool("toKick", true);

                //removing the ball form foot pos
                nottackled=false;
                

                Vector3 directionv = kickDirection + new Vector3(0,0.5f,0f);
                rbb.AddForce(directionv  * kickForceplayer, ForceMode.Impulse);;

                Invoke("KickFalse",1f);
               }
               
            
            }
                
            
            }
        else
        {
            Debug.Log("not on leg");
           // kickAni=false;
            // Check if the player is close enough to the ball to pick it up
            float distanceToBall = Vector3.Distance(transform.position, ball.transform.position);
            if (distanceToBall <= pickupDistance)
            {
                // Pick up the ball
                //Invoke("ballPick",0.1f);
                //StartCoroutine(ball_pick());
                nottackled=true;
                carryingBallplayer = true;
                
            }
        }


    
    if (!carryingBallplayer){
        if (Input.GetKey(KeyCode.Space)){
            animator.SetBool("toslide", true);

            Invoke("stopslide", delayTime); 
        }
    }
    }

    void OnCollisionEnter(Collision collision)
    {
         if (carryingBallplayer)
        {
        // Check if the player collided with another object
        if (collision.gameObject == oponent)
        {
            Debug.Log("yesssssssssss");
            // Drop the ball
            
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(transform.forward * defenceForce, transform.position, ForceMode.Force);
            //rb.AddForce(transform.forward * defenceForce, ForceMode.Impulse);
            nottackled=false;
            carryingBallplayer = false;  
            Invoke("Task", delayTime);   
                   
            
            
            
        }
        
    }}
    void Task()
    {
        // This code will be executed after 2 seconds
        
        nottackled=true;
    }
    void ballKick(){
        
        kickAni=true;      
    }
    void stopslide(){
        animator.SetBool("toslide", false);
    }
    void KickFalse(){
        
        kickAni=false;
        animator.SetBool("toKick", false);
        carryingBallplayer = false;

    }

    void ballPick(){
        Debug.Log("ball");
        nottackled=true;
        carryingBallplayer = true;
    }
    IEnumerator ball_pick()
{
    yield return new WaitForSeconds(0.1f);
    Debug.Log("ball");
        nottackled=true;
        carryingBallplayer = true;
}
}