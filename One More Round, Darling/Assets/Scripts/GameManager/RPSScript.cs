using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class RPSScript : MonoBehaviour
{
    public RikaScript rikaScript;
    public PlayerScript playerScript;
    [SerializeField] public Animator targetAnimator;
    public Image IMGRock, IMGPaper, IMGScissors, IMGPopup, IMGRandom; 

    public GameObject RPS_UI;
    public Image PlayerHandUI;
    public Image[] PlayerHandPopUp;

    void Start()
    {
        rikaScript = GetComponent<RikaScript>();
        playerScript = GetComponent<PlayerScript>();
    }


    public async Task<int> gangsuit()
    {
        rikaScript.decideHand();
        // animation
        if (rikaScript.RPSHandIdx == 1) targetAnimator.SetBool("isRock", true);
        if (rikaScript.RPSHandIdx == 2) targetAnimator.SetBool("isPaper", true);
        if (rikaScript.RPSHandIdx == 3) targetAnimator.SetBool("isScissors", true);
        RPS_UI.SetActive(true);
        IMGPopup.enabled = true;
        IMGRandom.enabled = true;

        if (rikaScript.RPSHandIdx != 0 && playerScript.RPSHandIdx != 0)
        {
            PlayerHandPopUp[playerScript.RPSHandIdx - 1].enabled = true;
            PlayerHandUI.enabled = true;
            RPS_UI.SetActive(false);

            IMGRandom.enabled = false;
            if (rikaScript.RPSHandIdx == 1) IMGRock.enabled = true;
            if (rikaScript.RPSHandIdx == 2) IMGPaper.enabled = true;
            if (rikaScript.RPSHandIdx == 3) IMGScissors.enabled = true;
            if (rikaScript.RPSHandIdx == playerScript.RPSHandIdx)
            {
                Debug.Log("[DEBUG] Draw!");
                await Task.Delay(2000);
                resetHand();
                resetAnimation();
                return 1;
            }
            compareHand(playerScript.RPSHandIdx, rikaScript.RPSHandIdx);
            await Task.Delay(2000);
            resetHand();
            return 2;
        }
        return 0;
    }

    private void compareHand(int playerHand, int rikaHand)
    {
        //Debug.Log("[DEBUG] Comparing Hand");
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
            Invoke("DelayResetAnim", 0.1f);
            // targetAnimator.SetBool("isDamage", false);
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
        PlayerHandPopUp[playerScript.RPSHandIdx - 1].enabled = false;
        PlayerHandUI.enabled = false;
        rikaScript.RPSHandIdx = 0;
        playerScript.RPSHandIdx = 0;
        rikaScript.RPSDecided = false;
        IMGRock.enabled = false;
        IMGPaper.enabled = false;
        IMGScissors.enabled = false;
        IMGRandom.enabled = false;
        IMGPopup.enabled = false;
        RPS_UI.SetActive(true);

    }

    private void resetAnimation()
    {
        targetAnimator.SetBool("isRock", false);
        targetAnimator.SetBool("isPaper", false);
        targetAnimator.SetBool("isScissors", false);
        // targetAnimator.SetBool("isDamage", false);
    }
    private void DelayResetAnim()
    {
        targetAnimator.SetBool("isDamage", false);
        resetAnimation();
    }



}
