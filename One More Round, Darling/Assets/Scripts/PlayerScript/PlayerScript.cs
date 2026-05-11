using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public int hp;
    public int score;
    public int RPSHandIdx;
    public int guessDirection;

    public bool winRPS;
    public bool attacker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RPSHandIdx = 0;
        guessDirection = 0;
        score = 0;
        hp = 3;
        winRPS = false;
        attacker = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
