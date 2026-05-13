using UnityEngine;

public class guessTimer : MonoBehaviour
{
    public float timer = 0f;

    public PlayerChoice playerChoiceScript;
    public ComparePlayerAndRikaChoice compareScript;

    bool roundFinished = false;

    int currentCount = 0;

    void Start()
    {
        timer = 0f;

        roundFinished = false;

        currentCount = 0;

        Debug.Log("Timer Started");
    }

    void Update()
    {
        if (roundFinished)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= 0.75f && currentCount == 0)
        {
            currentCount = 1;
            Debug.Log("1");
        }

        if (timer >= 1.5f && currentCount == 1)
        {
            currentCount = 2;
            Debug.Log("2");
        }

        if (timer >= 2.25f && currentCount == 2)
        {
            currentCount = 3;
            Debug.Log("3");
        }

        if (timer >= 2.25f)
        {
            roundFinished = true;

            Debug.Log("Time Over");

            if (playerChoiceScript.playerChoice == null || playerChoiceScript.playerChoice == "")
            {
                playerChoiceScript.AssignRandomChoice();
            }

            compareScript.StartCompare();
        }
    }

    public void RestartTimer()
    {
        timer = 0f;

        roundFinished = false;

        currentCount = 0;

        Debug.Log("Timer Restarted");
    }
}