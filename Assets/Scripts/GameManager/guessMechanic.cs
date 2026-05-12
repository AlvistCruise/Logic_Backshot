

using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMechanic : MonoBehaviour
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public Direction enemyDirection;
    public Direction playerGuess;

    void Start()
    {
        GenerateDirection();
    }

    void Update()
    {
        HandleInput();
    }

    void GenerateDirection()
    {
        enemyDirection = (Direction)Random.Range(0, 4);

        Debug.Log("Direction: " + enemyDirection);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerGuess = Direction.LEFT;
            CheckGuess();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerGuess = Direction.RIGHT;
            CheckGuess();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerGuess = Direction.UP;
            CheckGuess();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerGuess = Direction.DOWN;
            CheckGuess();
        }
    }

    void CheckGuess()
    {
        if (enemyDirection == playerGuess)
        {
            Debug.Log("correct");

            SceneManager.LoadScene("NextScene");
        }
        else
        {
            Debug.Log("wrong");

            GenerateDirection();
        }
    }
}