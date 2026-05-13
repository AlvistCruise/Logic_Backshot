using System.Threading;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

public class CoreLoop : MonoBehaviour
{
    private const int TOTAL_MENU = 4;
    public MenuScript menuScript;
    public GameObject RPSWorld;

    RPSScript RPSScript;
    ShootScript shootScript;
    HpDisplayScript hpScript;

    PlayerScript playerScript;
    RikaScript rikaScript;

    public TMP_Text RSPTimerText;

    public float RPSStartingTime = 10f;

    float RPSCurrentTime;
    public int stage = 0;
    
    public Canvas[] menuList;
    public bool drawCanvas;

    void Start()
    {

        RPSScript       = GetComponent<RPSScript>();
        playerScript    = GetComponent<PlayerScript>();
        rikaScript      = GetComponent<RikaScript>();
        shootScript     = GetComponent<ShootScript>();
        hpScript = GetComponent<HpDisplayScript>();

        //menuList        = new Canvas[TOTAL_MENU];
        RPSCurrentTime = RPSStartingTime;
        drawCanvas = true;
        stage = 1;
    }

    void Update()
    {
        if (!menuScript.start)return;
        hpScript.updateHp();
        //gangsuit
        if(stage == 1)
        {
            if (drawCanvas)
            {
                Debug.Log("Canvas drawed: 1");
                showCanvas(1);
                // showCanvas(4);
                RPSWorld.GetComponent<Canvas>().enabled = true;
                drawCanvas = !drawCanvas;
            }
            RPSCurrentTime -= 1 * Time.deltaTime;

            //Debug.Log("Sec: " + RPSCurrentTime);

            RSPTimerText.text = "Timer: " + Mathf.Round(RPSCurrentTime).ToString();

            int result = RPSScript.gangsuit();
            //draw
            if (result == 1) RPSCurrentTime = RPSStartingTime;
           
            //valid
            if (result == 2)
            {
                stage = 3;
                RPSWorld.GetComponent<Canvas>().enabled = false;
                drawCanvas = true;
                RPSCurrentTime = -1;
            }


            if(RPSCurrentTime <= 0)
            {
                RPSCurrentTime = RPSStartingTime;
                //TODO: RANDOM SELECT
                RPSWorld.GetComponent<Canvas>().enabled = false;
                stage = 3;
                drawCanvas = true;

            }
        }

        if(stage == 2) { 
            // nebak
            // arah kepala == arah tangan -> arah kepala jadi korban, arah tangan jadi pelaku (pegang senjata)
            // arah kepala != arah tangan -> kembali gangsuit
        }


        //Debug.Log("[DEBUG] Start gangsuit");
        //gangsuit == draw -> kembali gangsuit
        //ganguit != draw -> lanjut (menang = arah tangan | kalah = arah kepala)



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
