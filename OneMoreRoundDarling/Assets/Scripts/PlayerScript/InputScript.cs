using UnityEngine;

public class InputScript : MonoBehaviour
{
    PlayerScript playerScript;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    public void rock()
    {
        Debug.Log("Player choose rock");
        playerScript.RPSHandIdx = 1;
    }
    public void paper()
    {
        Debug.Log("Player choose paper");
        playerScript.RPSHandIdx = 2;
    }
    public void scissors()
    {
        Debug.Log("Player choose scissors");
        playerScript.RPSHandIdx = 3;
    }

    public void lookUp()
    {
        if (playerScript.winRPS)
        {
            playerScript.handDirection = 1;
            Debug.Log("Hand: " + playerScript.handDirection);
        } else
        {
            playerScript.headDirection = 1;
            Debug.Log("Head: " + playerScript.headDirection);
        }
    }

    public void lookDown()
    {
        if (playerScript.winRPS)
        {
            playerScript.handDirection = 3;
            Debug.Log("Hand: " + playerScript.handDirection);

        }
        else
        {
            playerScript.headDirection = 3;
            Debug.Log("Head: " + playerScript.headDirection);

        }
    }

    public void lookLeft()
    {
        if (playerScript.winRPS)
        {
            playerScript.handDirection = 4;
            Debug.Log("Hand: " + playerScript.handDirection);

        }
        else
        {
            playerScript.headDirection = 4;
            Debug.Log("Head: " + playerScript.headDirection);

        }
    }

    public void lookRight()
    {
        if (playerScript.winRPS)
        {
            playerScript.handDirection = 2;
            Debug.Log("Hand: " + playerScript.handDirection);

        }
        else
        {
            playerScript.headDirection = 2;
            Debug.Log("Head: " + playerScript.headDirection);

        }
    }
}
