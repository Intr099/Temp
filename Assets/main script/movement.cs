using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    public float rotationSpeed = 10f; // Rotation speed of the player
    public bool mov2 = true;


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
        // Get input from the keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 lmovement = new Vector3(moveVertical, 0,-moveHorizontal);
        

        Vector3 movement;
        if (mov2)
        {
            movement = transform.TransformDirection(lmovement);
        }
        else
        {
            movement = lmovement;
        }

        // Move the player
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Check if there is movement input
        if (movement != Vector3.zero)
        {
            // Calculate the rotation direction based on movement
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            // Rotate the player smoothly towards the target direction
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            
           // animator.SetBool("toRun", true);
        }



                if (moveHorizontal!=0 || moveVertical!=0)
        {
            // Set the "isRunning" parameter to true
            animator.SetBool("toRun", true);
           
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

    }
}
