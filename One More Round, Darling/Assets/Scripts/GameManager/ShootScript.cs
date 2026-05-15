using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;


public class ShootScript : MonoBehaviour
{
    [SerializeField] private const int BASE_SCORE = 100; 
    public PlayerScript currentPlayer;
    public RikaScript currentRika;
    public CoreLoop coreLoop;

    public GameObject shootMenu;

    [SerializeField] private Animator targetAnimator;
    [SerializeField] private Animator gunAnimator;

    // Variabel state untuk sesi tembak
    [SerializeField]private int selfShootCount = 0;
    private int currentScoreMultiplier = 1;

    private bool isShootingActionRunning = false;

    void Start()
    {
        coreLoop = GetComponent<CoreLoop>();
    }

    public void shoot(PlayerScript player, RikaScript rika)
    {

        //TODO: DECIDE DEPENDS ON GUESSING

        Debug.Log("SHOOTING START MTFKR");
        Debug.Log($"[DEBUG] Player: {player.winRPS}");
        Debug.Log($"[DEBUG] Rika: {rika.winRPS}");

        currentPlayer = player;
        currentRika = rika;
        
        // Reset state setiap kali masuk sesi tembak dari gangsuit
        selfShootCount = 0;
        currentScoreMultiplier = 1;
        isShootingActionRunning = false;

        // Reset Animator Senjata
        gunAnimator.SetBool("PlayerWin", false);
        gunAnimator.SetBool("RikaWin", false);

        //targetAnimator.SetBool("isStage1", true);
        
        // arah kepala == arah tangan -> arah kepala jadi korban, arah tangan jadi pelaku (pegang senjata)

        if (player.attacker && !rika.attacker)
        {
            Debug.Log("[DEBUG] Player menjadi penembak.");
            shootMenu.GetComponent<Canvas>().enabled = true;
            // resetAnimation();

            //TODO: Player shoot
            // Pindahkan pistol ke Player
            // gunAnimator.SetBool("PlayerWin", true);
        }
        else if (!player.attacker && rika.attacker)
        {
            Debug.Log("[DEBUG] Rika menjadi penembak");
            shootMenu.GetComponent<Canvas>().enabled = false;

            // Pindahkan pistol ke Rika
            gunAnimator.SetBool("RikaWin", true);
            BotShootPlayer();
        }
    }

    private async void BotShootPlayer()
    {
        bool hasBullet = Random.value > 0.5f; 
        
        // 1. Beritahu Animator apakah peluru isi atau kosong
        gunAnimator.SetBool("Shoot", hasBullet);

        // 2. TUNGGU ANIMASI MENEMBAK SELESAI (Misal: 3 detik / 3000 ms)
        // Sesuaikan angka 3000 ini dengan durasi animasi aslimu!
        await Task.Delay(4600);

        // 3. BARU LAKUKAN PENGURANGAN HP SETELAH ANIMASI BERHENTI
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
    public async void ShootSelf()
    {

        if (isShootingActionRunning || selfShootCount >= 2)
        {
            return;
        }
        isShootingActionRunning = true;

        // 1. SEMBUNYIKAN UI SAAT ANIMASI BERJALAN
        shootMenu.GetComponent<Canvas>().enabled = false;

        bool hasBullet = Random.value > 0.5f; 
        
        //hasBullet = false; 

        // Catatan: Pastikan kamu punya animasi "GunPlayerToPlayer" jika player nembak diri sendiri
        // Untuk sekarang kita asumsikan pakai animasi "Shoot" yang sama
        // 2. MAIN KAN ANIMASI: Player ambil pistol & nembak

        gunAnimator.SetBool("RikaWin", true); //nembak dari Rika
        gunAnimator.SetBool("Shoot", hasBullet);

        // 3. TUNGGU ANIMASI SELESAI
        await Task.Delay(4600);

        //if (selfShootCount >= 2)
        //{
        //    // Jika sudah 2x hoki nembak diri sendiri, paksa player untuk tembak Rika
        //   //Debug.Log("Batas nembak diri sendiri habis (Maks 2x)! Sekarang wajib tembak Rika.");
        //    EndShootSession(); // Giliran hangus
        //    return;
        //}


        if (hasBullet) 
        {
            // SIAL: Peluru meledak ke diri sendiri
            Debug.Log("DOR! Nembak diri sendiri dan ADA PELURU! Health & Score berkurang.");
            currentPlayer.score -= BASE_SCORE / 2;
            if(currentPlayer.score < 0)
            {
                currentPlayer.score = 0;
            }
            currentPlayer.hp -= 1;
            // TODO: currentPlayer.TakeDamage(); 
            // TODO: Kurangi score player

            EndShootSession(); // Giliran hangus
        }
        else 
        {
            // HOKI: Peluru kosong
            selfShootCount++;

            currentPlayer.score += (BASE_SCORE * 2) * currentScoreMultiplier;

            currentScoreMultiplier *= 2; 

            Debug.Log($"KLIK! Peluru KOSONG! (Hoki ke-{selfShootCount}/2). Multiplier skor sekarang x{currentScoreMultiplier}. Dikasih kesempatan lagi! [Current score: {currentPlayer.score}]");
            // Sesi belum berakhir. Player bisa tekan tombol UI lagi (ShootSelf atau ShootRika).

            // Reset state senjata kembali ke meja (opsional, tergantung loop animasimu)
            gunAnimator.SetBool("RikaWin", false); //reset Rika animation
            
            // MUNCULKAN UI LAGI karena player dapat giliran lagi!
            shootMenu.GetComponent<Canvas>().enabled = true;

            // Buka kunci lagi karena sesi belum berakhir (player masih bisa nembak)
            isShootingActionRunning = false;

            if (selfShootCount >= 2)
            {
                //ika sudah 2x hoki nembak diri sendiri, paksa player untuk tembak Rika
                //Debug.Log("Batas nembak diri sendiri habis (Maks 2x)! Sekarang wajib tembak Rika.");
                EndShootSession(); // Giliran hangus
            }
        }
    }

    // Dipanggil saat player klik tombol "Tembak Rika"
    public async void ShootRika()
    {
        // Cegah player spam klik tombol
        if (isShootingActionRunning) return; 
        isShootingActionRunning = true;

        // 1. SEMBUNYIKAN UI SAAT ANIMASI BERJALAN
        shootMenu.GetComponent<Canvas>().enabled = false;

        bool hasBullet = Random.value > 0.5f; 

        // 2. MAIN KAN ANIMASI: Player ambil pistol & nembak
        gunAnimator.SetBool("PlayerWin", true); 
        gunAnimator.SetBool("Shoot", hasBullet);

        // 3. TUNGGU ANIMASI SELESAI
        await Task.Delay(4600);

        // 4. Kurangi HP
        if (hasBullet)
        {
            // KENA RIKA
            Debug.Log($"DOR! Nembak Rika dan ADA PELURU! Rika -1 HP. Player dapat score (Multiplier x{currentScoreMultiplier}).");
            currentRika.hp -= 1;
            currentPlayer.score += BASE_SCORE;

            // --- TAMBAHKAN KODE SUARA DI SINI ---
            // currentRika.MainkanSuaraDamage(); // Rika berteriak kesakitan

            // Logika pergantian stage HP Rika
            if (currentRika.hp == 2)
            {
                targetAnimator.SetBool("isDamage", true);
                targetAnimator.SetBool("isStage1", false);
                targetAnimator.SetBool("isStage2", true);

                // --- SUARA GANTI STAGE ---
                currentRika.MainkanSuaraMarah();
                Invoke("DelayResetAnim", 0.1f);
            } else if (currentRika.hp == 1)
            {
                targetAnimator.SetBool("isDamage", true);
                targetAnimator.SetBool("isStage2", false);
                targetAnimator.SetBool("isStage3", true);

                // --- SUARA GANTI STAGE ---
                currentRika.MainkanSuaraMarah();

                Invoke("DelayResetAnim", 0.1f);
            }
             else if (currentRika.hp <= 0)
            {
                targetAnimator.SetBool("isDamage", true);
                targetAnimator.SetBool("isStage3", false);

                // --- SUARA GANTI STAGE ---
                currentRika.MainkanSuaraMarah(); 

                Invoke("DelayResetAnim", 0.1f);
            }
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
        // Pastikan UI mati saat keluar dari sesi tembak
        shootMenu.GetComponent<Canvas>().enabled = false;

        // Reset state senjata kembali ke tengah
        gunAnimator.SetBool("PlayerWin", false);
        gunAnimator.SetBool("RikaWin", false);
        
        if (coreLoop != null)
        {
            Debug.Log("Reset: " + coreLoop.drawCanvas);
            coreLoop.stage = 1; // Kembali ke fase gangsuit
            coreLoop.drawCanvas = true;
        }
    }

    private void resetAnimation()
    {
        targetAnimator.SetBool("isRock", false);
        targetAnimator.SetBool("isPaper", false);
        targetAnimator.SetBool("isScissors", false);
        targetAnimator.SetBool("isDamage", false);
    }
    private void DelayResetAnim()
    {
        targetAnimator.SetBool("isDamage", false);
        resetAnimation();
    }
}