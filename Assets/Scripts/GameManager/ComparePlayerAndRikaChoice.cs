using UnityEngine;

public class ComparePlayerAndRikaChoice : MonoBehaviour
{
    // 1 = player win suit
    // 0 = rika win suit
    private int suitCondition = 1;

    public PlayerChoice playerChoiceScript;
    public initializeRikaChoice rikaChoiceScript;
    public guessTimer timerScript;

    public void StartCompare()
    {
        // PLAYER WIN CONDITION
        if (suitCondition == 1)
        {
            if (playerChoiceScript.playerChoice == rikaChoiceScript.RikaChoice)
            {
                Debug.Log("Choice Same");

                // go to shoot scene
            }
            else
            {
                Debug.Log("Choice Not Same");

                // go back to suit scene
            }
        }

        // RIKA WIN CONDITION
        else if (suitCondition == 0)
        {
            if (playerChoiceScript.playerChoice == rikaChoiceScript.RikaChoice)
            {
                Debug.Log("Choice Same");

                // bot can shoot
            }
            else
            {
                Debug.Log("Choice Not Same");

                // go back to suit scene
            }
        }

        // PREPARE NEXT ROUND
        NextRound();
    }

    void NextRound()
    {
        // reset player choice
        playerChoiceScript.ResetChoice();

        // generate new rika choice
        rikaChoiceScript.GenerateNewChoice();

        // restart timer
        timerScript.RestartTimer();

        Debug.Log("Next Round Started");
    }
}