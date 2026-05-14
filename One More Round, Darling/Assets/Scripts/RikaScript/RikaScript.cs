using NUnit.Framework;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using System.Collections.Generic;

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

    public int headPercentageIncrease;

    GuessingScript guessingScript;

    void Start()
    {
        hp = 3;

        RPSHandIdx = handDirection = headDirection = 0;
        RPSDecided = headDecided = handDecided = false;

        winRPS = false;
        attacker = false;
        headPercentageIncrease = 0;
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

    public void decideHeadDirection(int playerIdx)
    {
        if (!headDecided)
        {

            //Debug.Log($"[DEBUG] Player percent: {playerPercent} | Other percent: {otherPercent}");

            List<int> temp = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if (i == playerIdx - 1) continue;
                temp.Add(i + 1);
            }

            foreach (int i in temp)
            {
                Debug.Log($"Item: {i}");
            }

            if (Random.Range(0, 101) < Random.Range(headPercentageIncrease, 51) + 25)
            {
                Debug.Log("rika pick player hand ");
                headDirection = playerIdx;
                headPercentageIncrease = 0;
            }
            else
            {
                Debug.Log("rika pick other hand");
                headDirection = temp[Random.Range(0, temp.Count)];
                headPercentageIncrease += 10;
                if (headPercentageIncrease > 50) headPercentageIncrease = 50;
            }

            //headDirection = Random.Range(1, 5);
            Debug.Log($"[DEBUG] Rika head selected idx: {headDirection}");
            headDecided = !headDecided;
            //guessingScript.StopAnimAndGetArrow();
        }
    }
}
