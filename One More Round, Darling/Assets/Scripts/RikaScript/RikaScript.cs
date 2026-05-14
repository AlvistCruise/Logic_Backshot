using UnityEngine;

public class RikaScript : MonoBehaviour
{
    public int hp;
    
    public int RPSHandIdx;
    public int headDirection;
    public int handDirection;

    public bool RPSDecided;
    public bool headDecided;
    public bool handDecided;
    //bool directionDecided;

    public bool winRPS;
    public bool attacker;

    GuessingScript guessingScript;

    void Start()
    {
        hp = 3;

        RPSHandIdx = handDirection = headDirection = 0;
        RPSDecided = headDecided = handDecided = false;

        winRPS = false;
        attacker = false;
    }

    // Update is called once per frame

    public void decideHand()
    {
        if (!RPSDecided)
        {
            RPSHandIdx = Random.Range(1, 4);
            Debug.Log($"[DEBUG] Rika RPS selected idx: {RPSHandIdx}");
            RPSDecided = !RPSDecided;
        }
    }

    public void decideHandDirection()
    {
        if (!handDecided)
        {
            handDirection = Random.Range(1, 5);
            Debug.Log($"[DEBUG] Rika hand selected idx: {handDirection}");
            handDecided = !handDecided;
            //guessingScript.StopAnimAndGetArrow();
        }
    }

    public void decideHeadDirection()
    {

        if (!headDecided)
        {
            headDirection = Random.Range(1, 5);
            Debug.Log($"[DEBUG] Rika head selected idx: {headDirection}");
            headDecided = !headDecided;
            //guessingScript.StopAnimAndGetArrow();
        }
    }
}
