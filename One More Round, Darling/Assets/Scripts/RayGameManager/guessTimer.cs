using UnityEngine;
using System.Collections;

public class guessTimer : MonoBehaviour
{
    int i = 0;
    public PlayerChoice playerChoiceScript;
    public ComparePlayerAndRikaChoice compareScript;
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (i < 3)
        {
            yield return new WaitForSeconds(0.75f);

            i++;

            Debug.Log(i);
        }

        Debug.Log("Time Over");
        playerChoiceScript.AssignRandomChoice(); // if within the period player dont choose, assign randomly
        compareScript.StartCompare();
    }
}