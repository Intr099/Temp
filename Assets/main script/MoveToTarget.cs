using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    // The target position to move towards
    //public Vector3 targetPosition;
    public Transform targetPosition;
    public GameObject ball;
    public Transform opoLeg;
    public float shootdis = 8.0f;
    public float kickForce = 50f;
    public bool carryingBallopo = false;
    [SerializeField]private Rigidbody rb;
    private Rigidbody rbb;
    private bool temp = true;
    private OpponentAI oopnent;
    public Transform playerback;
    public Transform playerfrount;
    public bool freezGoleOpo = false;
    Vector3 balldisOpo;
    float distanceToBallopo;

    private Rigidbody ballRb;



    // The speed at which to move
    public float speed = 5.0f;
    void Start()
    {
        oopnent=GetComponent<OpponentAI>();

        ballRb = ball.GetComponent<Rigidbody>();

        //rbb = GetComponent<Rigidbody>();
        // Get the ball's Rigidbody component
        //Rigidbody rb = ball.GetComponent<Rigidbody>();


    }
    void Update()
    {      
        
        //Debug.Log(distanceToBallopo);
        Vector3 ballDirection=playerfrount.position - playerback.position;
                //kickDirection = ball.transform.TransformDirection(kickDirection);
        ballDirection = ballDirection.normalized;
        if (carryingBallopo)
        {
        
        if (temp){
            //ball.transform.position = new Vector3(transform.position.x + 0.5f,  ball.transform.position.y,transform.position.z);
            ball.transform.position = opoLeg.position ;//+ ballDirection;//+ new Vector3(-0.2f, 0.2f, 0.2f)// new Vector3(0, 0.1f, 0.4f)
             //Debug.Log('j');

        }
        // Calculate the direction to the target position
        
        Vector3 direction = targetPosition.position - transform.position;

        // Normalize the direction so that it has a magnitude of 1
        direction.Normalize();
        
        oopnent.opponentaai=false;

        // Move the object in the direction of the target at the specified speed
        
        transform.position += direction * speed * Time.deltaTime;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        desiredRotation= Quaternion.RotateTowards( transform.rotation,desiredRotation,360* Time.fixedDeltaTime);
        transform.rotation = desiredRotation;
        float distance = Vector3.Distance(transform.position, targetPosition.position);
        //Debug.Log(distance);

        // If the distance is less than a certain threshold, kick the ball towards the target location
        if (distance <shootdis)
       //if (direction < 1.0f)
        {    
            
           // Debug.Log(distance);
            // Calculate the direction from the ball to the target location
            //Vector3 dir = (targetPosition - transform.position).normalized;
            temp=false;

            // Apply the kick force to the ball in the calculated direction
            //Vector3 forwardd = transform.forward;
            Vector3 forwardd = targetPosition.position;
            //rb.AddForce(forwardd * kickForce, ForceMode.Impulse);
            ballRb.AddForce(ballDirection * kickForce, ForceMode.Impulse);
            animator.SetBool("toKick_opo", true);
            Debug.Log("opo_kick");
                //rb.AddForce(0,0,1000);

                //oopnent.opponentaai=false;

            }
        }
        else{
            
            //if (distanceToBallopo<2f){
            //Debug.Log(distanceToBallopo);
            //carryingBallopo = true;
            //temp=true;}

            
            if (!freezGoleOpo){
            oopnent.opponentaai=true;

        }}
        
    
    
    
    
    }
    void OnCollisionEnter(Collision collision)
    {
        float distanceToBallopo = Vector3.Distance(transform.position, ball.transform.position);
        if (collision.gameObject == ball || distanceToBallopo<1.5f )
        {
            
           // Debug.Log("ffffff");
            carryingBallopo = true;
            temp=true;
        }
        else
        {
            //Debug.Log("eeeee");
            carryingBallopo = false;
        }
    }}


