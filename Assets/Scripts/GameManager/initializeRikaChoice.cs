using UnityEngine;

public class initializeRikaChoice : MonoBehaviour
{
    public int rikaChoice;

    public string RikaChoice;

    public Sprite stage1Up;
    public Sprite stage1Down;
    public Sprite stage1Left;
    public Sprite stage1Right;

    public SpriteRenderer rikaRenderer;

    void Start()
    {
        GenerateNewChoice();
    }

    public void GenerateNewChoice()
    {
        rikaChoice = Random.Range(0, 4);

        SetRikaChoice();

        Debug.Log("Rika Choice: " + RikaChoice);

        ChangeSprite();
    }

    void SetRikaChoice()
    {
        if (rikaChoice == 0)
        {
            RikaChoice = "UP";
        }

        else if (rikaChoice == 1)
        {
            RikaChoice = "DOWN";
        }

        else if (rikaChoice == 2)
        {
            RikaChoice = "LEFT";
        }

        else if (rikaChoice == 3)
        {
            RikaChoice = "RIGHT";
        }
    }

    void ChangeSprite()
    {
        if (rikaChoice == 0)
        {
            rikaRenderer.sprite = stage1Up;
        }

        else if (rikaChoice == 1)
        {
            rikaRenderer.sprite = stage1Down;
        }

        else if (rikaChoice == 2)
        {
            rikaRenderer.sprite = stage1Left;
        }

        else if (rikaChoice == 3)
        {
            rikaRenderer.sprite = stage1Right;
        }
    }
}