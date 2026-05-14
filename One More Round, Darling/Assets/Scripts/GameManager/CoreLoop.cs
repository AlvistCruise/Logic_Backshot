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
    public GameObject LookGuessWorld;
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
    private bool isGangsuitRunning = false;
    
    
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
        GuessCurrentTime = GuessStartingTime; //guess fisrt timer setter
        drawCanvas = true;
        countdownSession = intervalSession = false; //count still paused at the start of the game || Interval Start when countdown end
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

    // CEK GEMBOK: Hanya jalankan timer dan proses gangsuit jika gembok terbuka (false)
    if (!isGangsuitRunning)
    {
        RPSCurrentTime -= 1 * Time.deltaTime;
        RSPTimerText.text = "Timer: " + Mathf.Round(RPSCurrentTime).ToString();

        if (RPSCurrentTime <= 0 && playerScript.RPSHandIdx == 0)
        {
            //TODO: RANDOM SELECT + ANIMASI GANGSUIT
            playerScript.RPSHandIdx = Random.Range(1, 4);
            Debug.Log($"[DEBUG] Auto select: {playerScript.RPSHandIdx}");
        }

        // 1. KUNCI GEMBOKNYA! 
        // Ini akan mencegah frame berikutnya masuk ke blok ini lagi.
        isGangsuitRunning = true;

        // 2. TUNGGU HASIL. 
        // Jika belum ada yang milih, ini mengembalikan 0 secara instan.
        // Jika sudah milih, ini akan pause selama 2 detik di baris ini.
        int result = await RPSScript.gangsuit();

        // draw
        if (result == 1) 
        {
            RPSCurrentTime = RPSStartingTime;
        }
        // valid
        else if (result == 2) // Gunakan else if agar lebih aman
        {
            stage = 2;
            RPSWorld.GetComponent<Canvas>().enabled = false;
            drawCanvas = true;
            RPSCurrentTime = RPSStartingTime;
        }

        // 3. BUKA GEMBOKNYA!
        // Setelah delay 2 detik selesai (atau langsung jika return 0), gembok dibuka
        // supaya timer dan pengecekan ronde selanjutnya bisa berjalan lagi.
        isGangsuitRunning = false;
    }
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
                LookGuessWorld.GetComponent<Canvas>().enabled = true;
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
                    // MATIKAN UI LOOK GUESS WORLD DI SINI
                    LookGuessWorld.GetComponent<Canvas>().enabled = false;
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
