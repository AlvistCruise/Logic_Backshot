using UnityEngine;
using UnityEngine.EventSystems; // Wajib ditambahkan untuk mendeteksi kursor pada UI

public class SuaraTombol : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Pengaturan Speaker")]
    [Tooltip("Masukkan objek yang memiliki komponen Audio Source ke sini")]
    public AudioSource speakerSFX; 

    [Header("Kaset Suara (Audio Clip)")]
    public AudioClip suaraHover;
    public AudioClip suaraKlik;

    // Fungsi ini otomatis berjalan saat kursor mouse MASUK ke area tombol (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (speakerSFX != null && suaraHover != null)
        {
            speakerSFX.PlayOneShot(suaraHover);
        }
    }

    // Fungsi ini otomatis berjalan saat tombol DIKLIK
    public void OnPointerClick(PointerEventData eventData)
    {
        if (speakerSFX != null && suaraKlik != null)
        {
            speakerSFX.PlayOneShot(suaraKlik);
        }
    }
}