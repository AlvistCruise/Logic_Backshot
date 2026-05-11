using UnityEngine;

public class RikaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int hp;
    public int RPSHandIdx;
    public int guessDirection;
    public bool RPSDecided;
    bool directionDecided;

    public bool winRPS;
    public bool attacker;

    void Start()
    {
        hp = 3;
        
        RPSHandIdx = 0;
        guessDirection = 0;
        directionDecided = false;
        RPSDecided = false;

        winRPS = false;
        attacker = false;
    }

    // Update is called once per frame

    public void decideHand()
    {
        if (!RPSDecided)
        {
            setRPSHand(Random.Range(1, 4));
            RPSDecided = !RPSDecided;
        }
    }

    public void setRPSHand(int handIdx)
    {
        this.RPSHandIdx = handIdx;
        Debug.Log("[DBEUG] Rika selected idx: " + handIdx);
    }
}
