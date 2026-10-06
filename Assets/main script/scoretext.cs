using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class scoretext : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [SerializeField]private GameObject playergoal;
    [SerializeField]private GameObject oponentgoal;
    
    backtoplace backtoplaceplayer;
    backtoplace backtoplaceopo;
    // Start is called before the first frame update
     void Awake()
    {
        backtoplaceplayer=playergoal.GetComponent<backtoplace>();
        backtoplaceopo=oponentgoal.GetComponent<backtoplace>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Player: " + backtoplaceopo.playerPoints + "  Opponent: " + backtoplaceplayer.opponentPoints ;
    }
}
