using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RPSScript : MonoBehaviour
{
    public RikaScript rikaScript;
    public PlayerScript playerScript;
    [SerializeField] private Animator targetAnimator;
    //bool resultConclusded;
    //int turnIdx

    void Start()
    {
        //turnIdx = 0;
        //resultConcluded = false;
        rikaScript = GetComponent<RikaScript>();
        playerScript = GetComponent<PlayerScript>();
    }


    public int gangsuit()
    {
        rikaScript.decideHand();
        // animation
        if (rikaScript.RPSHandIdx == 1) targetAnimator.SetBool("isRock", true);
        if (rikaScript.RPSHandIdx == 2) targetAnimator.SetBool("isPaper", true);
        if (rikaScript.RPSHandIdx == 3) targetAnimator.SetBool("isScissors", true);

        if (rikaScript.RPSHandIdx != 0 && playerScript.RPSHandIdx != 0)
        {
            if (rikaScript.RPSHandIdx == playerScript.RPSHandIdx)
            {
                Debug.Log("[DEBUG] Draw!");
                resetHand();
                resetAnimation();
                return 1;
            }
            compareHand(playerScript.RPSHandIdx, rikaScript.RPSHandIdx);
            resetHand();
            return 2;
        }
        return 0;
    }

    private void compareHand(int playerHand, int rikaHand)
    {
        Debug.Log("[DEBUG] Comparing Hand");
        int result = 0;
        // 1 == batu | 2 == gunting | 3 == kertas

        if ((playerHand == 1 && rikaHand == 2) || (rikaHand == 1 && playerHand == 2))
        {
            result = 2;
        } else if((playerHand == 1 && rikaHand == 3) || (rikaHand == 1 && playerHand == 3))
        {
            result = 1;
        } else if((playerHand == 2 && rikaHand == 3) || (rikaHand == 2 && playerHand == 3))
        {
            result = 3;
        }
        Debug.Log($"[DEBUG] Result : {result} | Player Hand : {playerHand}");
        if (result == 0) return;

        if (result == playerHand)
        {
            targetAnimator.SetBool("isDamage", true);
            Debug.Log("PLAYER WIN RPS");
            playerScript.winRPS = true;
            rikaScript.winRPS = false;
        } else
        {
            resetAnimation();
            Debug.Log("RIKA WIN RPS");
            playerScript.winRPS = false;
            rikaScript.winRPS = true;
        }
        //resultConcluded = true;
    }

    private void resetHand()
    {
        rikaScript.RPSHandIdx = 0;
        playerScript.RPSHandIdx = 0;
        rikaScript.RPSDecided = false;
    }

    private void resetAnimation()
    {
        targetAnimator.SetBool("isRock", false);
        targetAnimator.SetBool("isPaper", false);
        targetAnimator.SetBool("isScissors", false);
        targetAnimator.SetBool("isDamage", false);
    }



}
