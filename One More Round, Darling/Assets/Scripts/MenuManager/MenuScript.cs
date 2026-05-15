using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Cinemachine;
using System.Threading.Tasks; // WAJIB TAMBAH INI UNTUK DELAY EXIT

public class MenuScript : MonoBehaviour
{
    RPSScript RPSScript; // added reference to RPSScript to control animation when game ends
    public GameObject gameManager;

    [Header("Pengaturan BGM (Background Music)")]
    public AudioSource bgmAudioSource; // Corong speaker utama
    public AudioClip mainMenuBGM;
    public AudioClip inGameBGM;
    public AudioClip winBGM;
    public AudioClip loseBGM;

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
    //public GameObject winLoseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;

    public Stack<GameObject> menuStack;

    public GameObject rikaObj;

    [SerializeField] private GameObject updateMenu ;
    [SerializeField] private GameObject curMenu;

    // FUNGSI BANTUAN UNTUK GANTI MUSIK
    private void PlayBGM(AudioClip newClip)
    {
        // Cegah error kalau audio source/clip kosong
        if (bgmAudioSource == null || newClip == null) return; 
        
        // Jangan putar ulang kalau lagunya sudah sama (biar gak putus-putus)
        if (bgmAudioSource.clip == newClip) return; 

        bgmAudioSource.Stop();
        bgmAudioSource.clip = newClip;
        bgmAudioSource.Play();
    }

    void Start()
    {
        // 1. WAJIB DITAMBAHKAN: Ambil komponen RPSScript dari gameManager
        if (gameManager != null)
        {
            RPSScript = gameManager.GetComponent<RPSScript>();
        }
        (creditMenu.GetComponent<Canvas>()).enabled = false;
        (tutorialMenu.GetComponent<Canvas>()).enabled = false;
        (RPSMenu.GetComponent<Canvas>()).enabled = false;
        (RPSWorld.GetComponent<Canvas>()).enabled = false;
        (lookGuessMenu.GetComponent<Canvas>()).enabled = false;
        (shootTargetMenu.GetComponent<Canvas>()).enabled = false;
        (gameMenu.GetComponent<Canvas>()).enabled = false;
        (winMenu.GetComponent<Canvas>()).enabled = false;
        (loseMenu.GetComponent<Canvas>()).enabled = false;

        menuStack = new Stack<GameObject>();
        start = false;

        //Debug.Log(GameObject.FindGameObjectWithTag("MainMenu"));
        menuStack.Push(mainMenu);
        updateMenu = curMenu = menuStack.Peek();

        Canvas curScreen = updateMenu.GetComponent<Canvas>();
        curScreen.enabled = true;

        gameCamera.enabled = false;

        // PUTAR MUSIK MAIN MENU SAAT GAME BARU DIBUKA
        PlayBGM(mainMenuBGM);

    }

    void Update()
    { 
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
        initGame();
        
        // 2. HAPUS SEMUA COMMENT DI BAWAH INI AGAR ANIMASI BENAR-BENAR RESET
        if (RPSScript != null && RPSScript.targetAnimator != null)
        {
            RPSScript.targetAnimator.SetBool("isFinish", false); 
            RPSScript.targetAnimator.SetBool("isStage1", true); 
            RPSScript.targetAnimator.SetBool("isStage2", false);
            RPSScript.targetAnimator.SetBool("isStage3", false);
            RPSScript.targetAnimator.SetBool("isRock", false);
            RPSScript.targetAnimator.SetBool("isPaper", false);
            RPSScript.targetAnimator.SetBool("isScissors", false);
            RPSScript.targetAnimator.SetBool("isDamage", false);
        }
        
        gameCamera.enabled = true;
        (gameMenu.GetComponent<Canvas>()).enabled = true;
        (rikaObj.GetComponent<Animator>()).enabled = true;
        start = true;

        // GANTI MUSIK JADI IN-GAME SAAT MULAI MAIN
        PlayBGM(inGameBGM);
    }

    public void back()
    {
        if (menuStack.Peek() == mainMenu) return;
        menuStack.Pop();
        updateMenu = menuStack.Peek();
        Debug.Log("[DEBUG] Back: " + updateMenu);

        // JIKA KEMBALI KE MAIN MENU (MISAL DARI WIN/LOSE), PUTAR LAGI MUSIK MAIN MENU
        if (updateMenu == mainMenu)
        {
            PlayBGM(mainMenuBGM);
        }
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
    public void win()
    {
        if (menuStack.Peek() == winMenu) return;
        start = false;
        gameCamera.enabled = false;
        (gameMenu.GetComponent<Canvas>()).enabled = false;
        // (rikaObj.GetComponent<Animator>()).enabled = false;
        menuStack.Push(winMenu);
        updateMenu = menuStack.Peek();
        Debug.Log("[DEBUG] Win: " + winMenu);

        // GANTI MUSIK JADI MENANG
        PlayBGM(winBGM);
    }

    public void lose()
    {
        if (menuStack.Peek() == loseMenu) return;
        start = false;
        gameCamera.enabled = false;
        (gameMenu.GetComponent<Canvas>()).enabled = false;
        // (rikaObj.GetComponent<Animator>()).enabled = false;
        menuStack.Push(loseMenu);
        updateMenu = menuStack.Peek();
        Debug.Log("[DEBUG] Lose: " + loseMenu);

        // GANTI MUSIK JADI KALAH
        PlayBGM(loseBGM);
    }

    public void initGame()
    {
        PlayerScript player = gameManager.GetComponent<PlayerScript>();
        RikaScript rika = gameManager.GetComponent<RikaScript>();
        player.Start();
        rika.Start();
    }

    // UBAH JADI ASYNC UNTUK PENGANGKATAN RIKA KE SURGA
    public async void exit()
    {
        // TODO: Rika mental saat exit, delay selama 2 detik sebelum quit untuk menyaksikan penngangkatan Rika ke surga :)
        Debug.Log("Mengirim Rika ke surga...");
        
        // TODO: Nyalakan animasi Rika mental di sini
        // Misalnya: RPSScript.targetAnimator.SetTrigger("isMental");

        // Tunggu 2 detik (2000 milidetik)
        await Task.Delay(100);

        // EditorApplication.isPaused = true;
        // Application.Quit();

        // Tutup aplikasinya
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
