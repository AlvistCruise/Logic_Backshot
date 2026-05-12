using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static float timer = 0f;

    public TMP_Text timerText;

    void Start()
    {
   
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // update UI text
        timerText.text = "Time : " + timer.ToString("F2");
    }

    void OnDestroy()
    {
        timer = 0f;
    }
}