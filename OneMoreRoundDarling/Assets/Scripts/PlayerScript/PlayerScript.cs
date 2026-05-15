using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public TMP_Text textScore;

    public int hp;
    public int score;

    public int RPSHandIdx;
    public int headDirection;
    public int handDirection;

    public bool winRPS;
    public bool attacker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        RPSHandIdx = handDirection = headDirection = 0;
        score = 0;
        hp = 3;
        winRPS = false;
        attacker = false;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + score.ToString();
        
    }


}
