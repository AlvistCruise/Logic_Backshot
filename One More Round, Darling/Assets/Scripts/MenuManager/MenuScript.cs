using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Cinemachine;

public class MenuScript : MonoBehaviour
{
    public bool start;
    public Camera playerCamera;
    public CinemachineCamera gameCamera;

    public GameObject mainMenu;
    public GameObject creditMenu;
    public GameObject tutorialMenu;

    public GameObject gameMenu;
    public GameObject RPSMenu;
    public GameObject RPSWorld;
    public GameObject lookGuessMenu;
    public GameObject shootTargetMenu;

    public Stack<GameObject> menuStack;

    [SerializeField] private GameObject updateMenu ;
    [SerializeField] private GameObject curMenu;

    void Start()
    {
        (creditMenu.GetComponent<Canvas>()).enabled = false;
        (tutorialMenu.GetComponent<Canvas>()).enabled = false;
        (RPSMenu.GetComponent<Canvas>()).enabled = false;
        (RPSWorld.GetComponent<Canvas>()).enabled = false;
        (lookGuessMenu.GetComponent<Canvas>()).enabled = false;
        (shootTargetMenu.GetComponent<Canvas>()).enabled = false;
        (gameMenu.GetComponent<Canvas>()).enabled = false;

        menuStack = new Stack<GameObject>();
        start = false;

        //Debug.Log(GameObject.FindGameObjectWithTag("MainMenu"));
        menuStack.Push(mainMenu);
        updateMenu = curMenu = menuStack.Peek();

        Canvas curScreen = updateMenu.GetComponent<Canvas>();
        curScreen.enabled = true;

        gameCamera.enabled = false;

    }

    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            debugStack();
        }
        if(updateMenu != curMenu)
        {
            Debug.Log("[DEBUG} Move Menu: ");
            Debug.Log("[DEBUG] -> Last menu: " + curMenu);
            Debug.Log("[DEBUG] -> New menu: " + updateMenu);

            Canvas lastMenu = curMenu.GetComponent<Canvas>();
            Canvas newMenu = updateMenu.GetComponent<Canvas>();

            lastMenu.enabled = false;
            newMenu.enabled = true;

            curMenu = updateMenu;
        } 
    }

    public void startGame()
    {
        //awal position : X-0.72 Y4.35 Z-11.2 | Rotation: X0 Y27.359 
        //playerCamera.transform.position = new Vector3(0, 4.35f, -9.79f);
        //playerCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameCamera.enabled = true;
        (gameMenu.GetComponent<Canvas>()).enabled = true;
        start = true;
    }

    public void back()
    {
        if (menuStack.Peek() == mainMenu) return;
        menuStack.Pop();
        updateMenu = menuStack.Peek();
        Debug.Log("[DEBUG] Back: " + updateMenu);
    }

    public void credit()
    {
        if (menuStack.Peek() == creditMenu) return;
        menuStack.Push(creditMenu);
        updateMenu = menuStack.Peek();
        Debug.Log("[DEBUG] Credit: " + updateMenu);
    }

    public void tutorial()
    {
        if (menuStack.Peek() == tutorialMenu) return;
        menuStack.Push(tutorialMenu);
        updateMenu = menuStack.Peek();
        Debug.Log("[DEBUG] Tutorial: " + updateMenu);
    }

    public void exit()
    {
        //TODO: Rika mental saat exit, delay selama 2 detik sebelum quit untuk menyaksikan penngangkatan Rika ke surga :)

        EditorApplication.isPaused = true;
        Application.Quit();
    }

    private void debugStack()
    {

    }


}
