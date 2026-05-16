using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Canvas[] canvasPages;
    public TMP_Text pageText;

    public Image nextButton;
    public Image prevButton;

    public int page;
    public int prevPage;
    void Start()
    {
        prevPage = page = 0;
        for(int i = 0; i < canvasPages.Length; i++)
        {
            canvasPages[i].enabled = false;
        }
        showPage();
    }

    private void showPage()
    {
        canvasPages[prevPage].enabled = false;
        canvasPages[page].enabled = true;
        pageText.text = "PAGE " + (page + 1).ToString();
        if (page <= 0)
        {
            prevButton.enabled = false;
        } else if (page >= canvasPages.Length - 1)
        {
            nextButton.enabled = false;
        } else
        {
            prevButton.enabled = true;
            nextButton.enabled = true;
        }
    }

    public void next()
    {
        if (page >= canvasPages.Length - 1) return;
        prevPage = page;
        page++;
        showPage();
    }

    public void prev()
    {
        if (page <= 0) return;
        prevPage = page;
        page--;
        showPage();
    }
}
