using System.Threading;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CoreLoop : MonoBehaviour
{
    [Header("Menus")]
    private const int TOTAL_MENU = 4;
    public MenuScript menuScript;
    public GameObject RPSWorld;
    public Canvas[] menuList;
    public bool drawCanvas;

    [Header("Audio Scripts RPS")]
    RPSScript RPSScript;
    GuessingScript guessingScript;
    ShootScript shootScript;

    HpDisplayScript hpScript;

    PlayerScript playerScript;
    RikaScript rikaScript;

    public TMP_Text RSPTimerText;
    public TMP_Text GuessCountdownText;
    public TMP_Text GuessIntervalText;

    public float RPSStartingTime = 10f;
    public float GuessStartingTime = 5f;
    public bool countdownSession;
    public bool intervalSession;

    float RPSCurrentTime;
    float GuessCurrentTime;

    public int stage = 0;
    
    
    void Start()
    {

        RPSScript       = GetComponent<RPSScript>();
        playerScript    = GetComponent<PlayerScript>();
        rikaScript      = GetComponent<RikaScript>();
        shootScript     = GetComponent<ShootScript>();
        guessingScript  = GetComponent<GuessingScript>();
        hpScript        = GetComponent<HpDisplayScript>();

        //menuList        = new Canvas[TOTAL_MENU];
        RPSCurrentTime = RPSStartingTime;
        GuessCurrentTime = GuessStartingTime;
        drawCanvas = true;
        countdownSession = intervalSession = false;
        stage = 1;
    }

    async Task Update()
    {
        if (!menuScript.start)return;
        hpScript.updateHp();

        //TODO: win lose condition 

        //gangsuit
        if(stage == 1)
        {
            if (drawCanvas)
            {
                Debug.Log("Canvas drawed: 1");
                showCanvas(1);
                RPSWorld.GetComponent<Canvas>().enabled = true;
                drawCanvas = !drawCanvas;
            }
            RPSCurrentTime -= 1 * Time.deltaTime;

            //Debug.Log("Sec: " + RPSCurrentTime);

            RSPTimerText.text = "Timer: " + Mathf.Round(RPSCurrentTime).ToString();

            if (RPSCurrentTime <= 0 && playerScript.RPSHandIdx == 0)
            {
                //TODO: RANDOM SELECT + ANIMASI GANGSUIT

                playerScript.RPSHandIdx = Random.Range(1, 4);
                Debug.Log($"[DEBUG] Auto select: {playerScript.RPSHandIdx}");

                //RPSWorld.GetComponent<Canvas>().enabled = false;
                //stage = 3;
                //drawCanvas = true;

            }

            int result = await RPSScript.gangsuit();

            //draw
            if (result == 1) RPSCurrentTime = RPSStartingTime;
            //valid
            if (result == 2)
            {
                stage = 2;
                RPSWorld.GetComponent<Canvas>().enabled = false;
                drawCanvas = true;
                RPSCurrentTime = RPSStartingTime;
            }

            //Debug.Log("[DEBUG] Start gangsuit
        }

        // nebak
            //gangsuit == draw -> kembali gangsuit
            //ganguit != draw -> lanjut (menang = arah tangan | kalah = arah kepala
        if(stage == 2) {
            if (drawCanvas)
            {
                drawCanvas = !drawCanvas;
                Debug.Log("Canvas drawed 2");
                showCanvas(2);
                GuessCountdownText.enabled = true;
                GuessIntervalText.enabled = false;
                GuessCurrentTime = GuessStartingTime;
                GameObject[] objList = GameObject.FindGameObjectsWithTag("ToggleableGuessMenu");
                for(int i = 0; i < objList.Length; i++)
                {
                    (objList[i].GetComponent<Image>()).enabled = false;
                }
                countdownSession = true;
            }

            if (countdownSession)
            {
                GuessCurrentTime -= 1 * Time.deltaTime;
                GuessCountdownText.text = Mathf.Round(GuessCurrentTime).ToString();
                if (GuessCurrentTime <= 0)
                {
                    GuessCurrentTime = 3f;
                    GuessIntervalText.enabled = true;
                    GuessCountdownText.enabled = false;
                    countdownSession = false;
                    GameObject[] objList = GameObject.FindGameObjectsWithTag("ToggleableGuessMenu");
                    for (int i = 0; i < objList.Length; i++)
                    {
                        (objList[i].GetComponent<Image>()).enabled = true;

                    }
                }
            }
            
            if(!countdownSession){
                GuessCurrentTime -= 1 * Time.deltaTime;                
                GuessIntervalText.text = ((Mathf.Round(GuessCurrentTime * 100)) / 100.0f).ToString();

                if (GuessCurrentTime <= 0) 
                {
                    if (playerScript.winRPS)
                    {
                        playerScript.handDirection = Random.Range(1, 5);
                        Debug.Log($"Player random hand: " + playerScript.handDirection);
                    } else
                    {
                        playerScript.headDirection = Random.Range(1, 5);
                        Debug.Log($"Player random head: " + playerScript.headDirection);
                    }
                }

                if (guessingScript.guess(playerScript, rikaScript))
                {
                    //Debug.Log("Continue to shooting");
                    stage = 3;
                    drawCanvas = true;
                    //GuessCurrentTime = GuessStartingTime;
                }
            }
        }


        // arah kepala == arah tangan -> arah kepala jadi korban, arah tangan jadi pelaku (pegang senjata)
        // arah kepala != arah tangan -> kembali gangsuit
        // nembak
        if(stage == 3)
        {
            if (drawCanvas)
            {
                drawCanvas = !drawCanvas;
                Debug.Log("Canvas drawed: 3");
                showCanvas(3);
                shootScript.shoot(playerScript, rikaScript);
            }
            // Debug.Log("[DEBUG] Start shooting");


            // if shooting session selesai, cek hp, kalau ada yang hp nya 0, game over, kalau gak, kembali gangsuit
        }
    }

    public void showCanvas(int canvasIdx)
    {
        for(int i = 0; i < 4; i++)
        {
            if (i == canvasIdx) menuList[i].enabled = true;
            else menuList[i].enabled = false;
        }
    }
    
}
