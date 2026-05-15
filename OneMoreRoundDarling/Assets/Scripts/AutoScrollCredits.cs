using UnityEngine;

public class AutoScrollCredits : MonoBehaviour
{
    public RectTransform contentRect;
    public float scrollSpeed = 50f; // Atur kecepatan scroll di sini
    public bool isAutoScrolling = true;

    void Start()
    {
        // Pastikan kita mengambil RectTransform dari objek Content
        if (contentRect == null)
        {
            contentRect = GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (isAutoScrolling)
        {
            // Menggeser posisi Y dari Content ke atas secara perlahan
            contentRect.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
    }

    // Bisa dipanggil lewat tombol kalau pemain mau skip credits
    public void StopScrolling()
    {
        isAutoScrolling = false;
    }
}