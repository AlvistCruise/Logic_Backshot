using UnityEngine;

public class ArrowInput : MonoBehaviour
{
    public string direction;

    public PlayerChoice playerChoiceScript;

    void OnMouseDown()
    {
        playerChoiceScript.SetChoice(direction);
    }
}