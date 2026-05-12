using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class ShootScript : MonoBehaviour
{
    [SerializeField] private const int BASE_SCORE = 100; 
    public PlayerScript currentPlayer;
    public RikaScript currentRika;
    public CoreLoop coreLoop;

    public GameObject shootMenu;

    [SerializeField] private Animator targetAnimator;

    // Variabel state untuk sesi tembak
    private int selfShootCount = 0;
    private int currentScoreMultiplier = 1;

    void Start()
    {
        coreLoop = GetComponent<CoreLoop>();
    }

    public void shoot(PlayerScript player, RikaScript rika)
    {
        Debug.Log("SHOOTING START MTFKR");
        Debug.Log($"[DEBUG] Player: {player.winRPS}");
        Debug.Log($"[DEBUG] Rika: {rika.winRPS}");

        currentPlayer = player;
        currentRika = rika;
        
        // Reset state setiap kali masuk sesi tembak dari gangsuit
        selfShootCount = 0;
        currentScoreMultiplier = 1;

        //targetAnimator.SetBool("isStage1", true);

        if (player.winRPS && !rika.winRPS)
        {
            shootMenu.GetComponent<Canvas>().enabled = true;
            Debug.Log("[DEBUG] Player menang gangsuit. Giliran Player memilih target.");

            //TODO: Player shoot
        }
        else if (!player.winRPS && rika.winRPS)
        {
            Debug.Log("[DEBUG] Rika menang gangsuit. Rika otomatis menembak Player!");
            shootMenu.GetComponent<Canvas>().enabled = false;
            BotShootPlayer();
        }
    }

    private void BotShootPlayer()
    {
        bool hasBullet = Random.value > 0.5f; 
        
        if (hasBullet)
        {
            Debug.Log("DOR! Rika menembak player dan ADA PELURU! Health player berkurang.");
            currentPlayer.hp-=1;
            // TODO: currentPlayer.TakeDamage();
        }
        else
        {
            Debug.Log("KLIK! Rika menembak player tapi KOSONG! Player hoki, selamat.");
        }

        EndShootSession();
    }

    // Dipanggil saat player klik tombol "Tembak Diri Sendiri"
    public void ShootSelf()
    {
        if (selfShootCount >= 2)
        {
            // Jika sudah 2x hoki nembak diri sendiri, paksa player untuk tembak Rika
            Debug.Log("Batas nembak diri sendiri habis (Maks 2x)! Sekarang wajib tembak Rika.");
            return; 
        }

        bool hasBullet = Random.value > 0.5f; 

        if (hasBullet) 
        {
            // SIAL: Peluru meledak ke diri sendiri
            Debug.Log("DOR! Nembak diri sendiri dan ADA PELURU! Health & Score berkurang.");
            currentPlayer.score -= BASE_SCORE;
            currentPlayer.hp -= 1;
            // TODO: currentPlayer.TakeDamage(); 
            // TODO: Kurangi score player

            EndShootSession(); // Giliran hangus
        }
        else 
        {
            // HOKI: Peluru kosong
            selfShootCount++;

            currentPlayer.score += BASE_SCORE * currentScoreMultiplier;

            currentScoreMultiplier *= 2; 

            Debug.Log($"KLIK! Peluru KOSONG! (Hoki ke-{selfShootCount}/2). Multiplier skor sekarang x{currentScoreMultiplier}. Dikasih kesempatan lagi! [Current score: {currentPlayer.score}]");
            // Sesi belum berakhir. Player bisa tekan tombol UI lagi (ShootSelf atau ShootRika).
        }
    }

    // Dipanggil saat player klik tombol "Tembak Rika"
    public void ShootRika()
    {
        bool hasBullet = Random.value > 0.5f; 

        if (hasBullet)
        {
            // KENA RIKA
            Debug.Log($"DOR! Nembak Rika dan ADA PELURU! Rika -1 HP. Player dapat score (Multiplier x{currentScoreMultiplier}).");
            currentRika.hp -= 1;
            // TODO: currentRika.TakeDamage(); 
            // TODO: Tambah Score * currentScoreMultiplier
        }
        else
        {
            // MELeset / KOSONG
            // Sesuai intruksi: score tetap sama saja.
            Debug.Log("KLIK! Nembak Rika tapi KOSONG! Rika selamat. Score tetap sama saja.");
        }

        EndShootSession();
    }

    private void EndShootSession()
    {
        Debug.Log("[DEBUG] Sesi tembak selesai. Kembali ke stage Gangsuit.");
        
        // TODO: Cek kondisi HP Player dan Rika di sini untuk trigger Game Over
        
        if (coreLoop != null)
        {
            Debug.Log("Reset: " + coreLoop.drawCanvas);
            coreLoop.stage = 1; // Kembali ke fase gangsuit
            coreLoop.drawCanvas = true;
        }
    }
}