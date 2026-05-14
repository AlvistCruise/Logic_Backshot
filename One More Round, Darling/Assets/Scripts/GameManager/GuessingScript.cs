using TMPro.EditorUtilities;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class GuessingScript : MonoBehaviour
{
    public CoreLoop coreLoop;
    public GameObject[] arrowsObj;

    public Image randomArrow;
    public Image[] popUpArrow;

    private bool stopAnim = true;
    private int[] arrowRotation = { 0, -90, -180, -270 };

    // public Image IMGRandomArrow;
    public GameObject GUESS_UI;
    public Image PlayerGuessUi;
    public Image[] PlayerGuessPopUp;


    void Start()
    {
        coreLoop = GetComponent<CoreLoop>();
    }

    public async Task<int> guess(PlayerScript player, RikaScript rika)
    {
        //ganguit != draw -> lanjut (menang = arah tangan | kalah = arah kepala
        //if (!GUESS_UI.activeSelf)
        //{
        //    GUESS_UI.SetActive(true);
        //    Debug.Log("Guess UI state: " + GUESS_UI.activeSelf);
        //}

        if (PlayerGuessUi.enabled)
        {
            PlayerGuessUi.enabled = false;
        }

        if (player.winRPS && !rika.winRPS)
        {
            //shootMenu.GetComponent<Canvas>().enabled = true;
            //Debug.Log("[DEBUG] Player menang gangsuit. Player yang menentukan arah tangan!");

            //player decide dulu -> calculate persentase -> rika pilih

            if (player.handDirection == 0) return 0;


            rika.decideHeadDirection(player.handDirection);
            if (stopAnim)
            {
                StopAnimAndGetArrow(rika.headDirection);
                stopAnim = !stopAnim;
            }

            GUESS_UI.SetActive(false);
            PlayerGuessUi.enabled = true;
            PlayerGuessPopUp[player.handDirection - 1].enabled = true;
            int res = PlayerDecide(player, rika);
            
            await Task.Delay(2000);
            resetDirection(player, rika);

            return res;
        }
        else if (!player.winRPS && rika.winRPS)
        {
            //Debug.Log("[DEBUG] Rika menang gangsuit. Rika yang menentukan arah tangan!");
            //shootMenu.GetComponent<Canvas>().enabled = false;

            rika.decideHandDirection();

            if (stopAnim)
            {
                StopAnimAndGetArrow(rika.handDirection);
                stopAnim = !stopAnim;
            }

            if (player.headDirection == 0 || rika.handDirection == 0) return 0;

            GUESS_UI.SetActive(false);

            PlayerGuessUi.enabled = true;
            PlayerGuessPopUp[player.headDirection - 1].enabled = true;
            int res = RikaDecide(player, rika);
            
            await Task.Delay(2000);
            resetDirection(player, rika);

            return res;

        }
        return 0;
    }

    private int PlayerDecide(PlayerScript player, RikaScript rika)
    {

        //player dulu nunjuk baru si Rika gerak kepala
        Debug.Log($"[DEBUG] Player Hand: {player.handDirection} | Rika Head: {rika.headDirection}");
        if (player.handDirection == rika.headDirection)
        {
            Debug.Log("[DEBUG] Same direction! continue to shoot");
            player.attacker = true;
            rika.attacker = false;
            return 2;
        } else
        {
            Debug.Log("[DEBUG] Different direction! back to gangsuit");
            //coreLoop.stage = 1;
            //coreLoop.drawCanvas = true;
            //coreLoop.LookGuessWorld.GetComponent<Canvas>().enabled = false;
            //resetDirection(player, rika);
            return 1;
        }

    }

    private int RikaDecide(PlayerScript player, RikaScript rika)
    {


        Debug.Log($"[DEBUG] Player Hand: {player.headDirection} | Rika Head: {rika.handDirection}");
        if (rika.handDirection == player.headDirection)
        {
            Debug.Log("[DEBUG] Same direction! continue to shoot");
            player.attacker = false;
            rika.attacker = true;
            rika.headDecided = rika.handDecided = false;
            //resetDirection(player, rika);
            return 2;
        }
        else
        {
            Debug.Log("[DEBUG] Different direction! back to gangsuit");
            //resetDirection(player, rika);
            return 1;
        }

    }

    private void resetDirection(PlayerScript player, RikaScript rika)
    {
        player.handDirection = rika.handDirection = 0;
        player.headDirection = rika.headDirection = 0;

        rika.headDecided = rika.handDecided = false;

        PlayerGuessUi.enabled = false;
        for(int i = 0; i < PlayerGuessPopUp.Length; i++)
        {
            PlayerGuessPopUp[i].enabled = false;
        }

        stopAnim = true;
    }

    public void StopAnimAndGetArrow(int idx)
    {
        if (idx == 0) return;
        Debug.Log("Total arrows: " + arrowsObj.Length);
        randomArrow.enabled = false;
        popUpArrow[idx - 1].enabled = true;
        for (int i = 0; i < arrowsObj.Length; i++)
        {
            (arrowsObj[i].GetComponent<Animator>()).enabled = false;

            float z = arrowRotation[idx - 1];
            (arrowsObj[i].GetComponent<RectTransform>()).localEulerAngles = new Vector3(0, 0, z);
        }
    }

    public void StartAnimArrow()
    {
        for (int i = 0; i < arrowsObj.Length; i++)
        {
            (arrowsObj[i].GetComponent<Animator>()).enabled = true;
        }
        for(int i = 0; i < popUpArrow.Length; i++)
        {
            popUpArrow[i].enabled = false;
        }
        randomArrow.enabled = true;
    }
}
