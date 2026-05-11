using UnityEngine;

public class InputScript : MonoBehaviour
{
    PlayerScript playerScript;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    void Update()
    {
        
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

    }

    public void lookDown()
    {

    }

    public void lookLeft()
    {

    }

    public void lookRight()
    {

    }
}
