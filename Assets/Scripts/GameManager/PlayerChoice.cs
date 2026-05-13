using UnityEngine;

public class PlayerChoice : MonoBehaviour
{
    public string playerChoice; // player choice

    bool hasChosen = false;
    public void SetChoice(string direction)
    {
        playerChoice = direction;

        hasChosen = true;

        Debug.Log("Player Choice: " + playerChoice);
    }
    public void AssignRandomChoice()
    {
        if (hasChosen)
        {
            return;
        }

        int randomChoice = Random.Range(0, 4);

        if (randomChoice == 0)
        {
            playerChoice = "UP";
        }

        else if (randomChoice == 1)
        {
            playerChoice = "DOWN";
        }

        else if (randomChoice == 2)
        {
            playerChoice = "LEFT";
        }

        else if (randomChoice == 3)
        {
            playerChoice = "RIGHT";
        }

        Debug.Log("Random Player Choice: " + playerChoice);
    }
    public void ResetChoice()
    {
        hasChosen = false;
        playerChoice = "";
    }
}