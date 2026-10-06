using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  added to opoNetgoal and playerNetgoal
/// </summary>

public class backtoplace : MonoBehaviour
{
    
    public GameObject ball;
    //public Vector3 positionOffset;
    public Transform players_reset;
    public Transform ball_reset;
    [SerializeField]private float delayTime = 2f;
    public float positionOffset=2f ;
      // The amount to offset the position by
    public float positionOffsetdis=1f ;
    //public new string tag = "onfield";

    public new string tag_players = "players";
    public new string tag_ball = "ball";
    // tag added to opo, player, ball
    // 

    private float positionOffseton ;
    [SerializeField]private GameObject oponent;
    [SerializeField]private GameObject player;
    ballplay playerScript;
    MoveToTarget oposcript;
    OpponentAI opoai;
    public int playerPoints = 0;
    public int opponentPoints = 0;
    
    
    //public bool newstart=false;
    void Awake(){
        playerScript=player.GetComponent<ballplay>();
        positionOffseton=positionOffset;
        oposcript=oponent.GetComponent<MoveToTarget>();
        opoai=oponent.GetComponent<OpponentAI>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (transform.name == "opoNetgoal" || transform.name=="playerNetgoal"){
        Debug.Log("onon sport");
        positionOffseton=positionOffset;
        if (other.gameObject == ball)
        {
            playerScript.carryingBallplayer = false;
            oposcript.carryingBallopo=false;

            //OpponentAI oopnent=GetComponent<OpponentAI>();
            //Debug.Log("on sport");
            //newstart=true;
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag_players);
            oposcript.freezGoleOpo=true;
            opoai.opponentaai=false;
            Invoke(nameof(taskk), delayTime);

                //reseting ball position
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                ball.transform.position = ball_reset.position + new Vector3(0, 1, 0);    
        // Iterate through the objects and transform their position
        foreach (GameObject obj in objects)
        {
           // Debug.Log(obj);
           // positionOffseton=positionOffset;
            //obj.transform.position = new Vector3(positionOffset, 0, 0);
            //obj.transform.position = destination.position + positionOffset;
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            obj.transform.position = players_reset.position + new Vector3(positionOffseton, 2.4f, 0);
            //Debug.Log(obj.transform.position);
            //Debug.Log(positionOffseton);
            
            //positionOffset++;
            //positionOffset=positionOffset+positionOffsetdis;
            positionOffseton=positionOffseton+positionOffsetdis;
            
        }
        
            
          //  onfiels.transform.position = new Vector3(0,2,0);
        if (transform.name == "opoNetgoal"){
            //Debug.Log("playerrrrrrrrrr");
            playerPoints++;
            
        }
        else if (transform.name=="playerNetgoal"){
            //Debug.Log("opppooooo");
           opponentPoints++;
        }
        
        }
        
    }}
    void taskk(){
        //newstart=true;
        //Debug.Log("kokooo");
        //OpponentAI oopnent=GetComponent<OpponentAI>();
        oposcript.freezGoleOpo=false;
        opoai.opponentaai=true;
    }
}
