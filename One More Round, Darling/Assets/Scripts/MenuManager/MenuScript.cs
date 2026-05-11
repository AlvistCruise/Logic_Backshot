using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public bool start;
    public Camera playerCamera;

    void Start()
    {
        start = false;
    }

    void Update()
    {
        
    }

    public void startGame()
    {
        playerCamera.transform.position = new Vector3(0, 4.35f, -9.79f);
        playerCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
        
        start = true;
    }
}
