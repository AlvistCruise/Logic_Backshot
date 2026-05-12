using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private PlayerScript currentPlayer;
    private RikaScript currentRika;
    private CoreLoop coreLoop;

    // Variabel state untuk sesi tembak
    private int selfShootCount = 0;
    private int currentScoreMultiplier = 1;

    void Start()
    {
        coreLoop = GetComponent<CoreLoop>();
    }

    public void shoot(PlayerScript player, RikaScript rika)
    {
        currentPlayer = player;
        currentRika = rika;
        
        // Reset state setiap kali masuk sesi tembak dari gangsuit
        selfShootCount = 0;
        currentScoreMultiplier = 1;

        if (player.winRPS && !rika.winRPS)
        {
            Debug.Log("[DEBUG] Player menang gangsuit. Giliran Player memilih target.");
        }
        else if (!player.winRPS && rika.winRPS)
        {
            Debug.Log("[DEBUG] Rika menang gangsuit. Rika otomatis menembak Player!");
            BotShootPlayer();
        }
    }

    private void BotShootPlayer()
    {
        bool hasBullet = Random.value > 0.5f; 
        
        if (hasBullet)
        {
            Debug.Log("DOR! Rika menembak player dan ADA PELURU! Health player berkurang.");
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
            // TODO: currentPlayer.TakeDamage(); 
            // TODO: Kurangi score player
            
            EndShootSession(); // Giliran hangus
        }
        else 
        {
            // HOKI: Peluru kosong
            selfShootCount++;
            currentScoreMultiplier *= 2; 

            Debug.Log($"KLIK! Peluru KOSONG! (Hoki ke-{selfShootCount}/2). Multiplier skor sekarang x{currentScoreMultiplier}. Dikasih kesempatan lagi!");
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
            coreLoop.stage = 1; // Kembali ke fase gangsuit
        }
    }
}