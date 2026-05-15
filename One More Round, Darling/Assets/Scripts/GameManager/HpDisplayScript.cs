using TMPro;
using UnityEngine;

public class HpDisplayScript : MonoBehaviour
{

    public PlayerScript player;
    public RikaScript rika;

    public GameObject playerHpContainer;
    public GameObject rikaHpContainer;

    public GameObject hpObj;

    public TMP_Text playerHpText;
    public TMP_Text rikaHpText;

    private int prevRikaHp = 0;
    private int prevPlayerHp = 0;

    void Start()
    {
        player = GetComponent<PlayerScript>();
        rika = GetComponent<RikaScript>();
        hideText();
    }
    // Update is called once per frame

    public void showText()
    {
        playerHpText.enabled = true;
        rikaHpText.enabled = true;
    }

    public void hideText() 
    { 
        playerHpText.enabled = false;
        rikaHpText.enabled = false;
    }

    public void updateHp()
    {
        if(prevPlayerHp != player.hp || prevRikaHp != rika.hp)
        {
            hpDisplayUpdate();
        }
    }

    void hpDisplayUpdate() {

        float hpObjGap = 0.5f;
        destroyAll();

        for(int i = 0; i < player.hp; i++)
        {
            Vector3 hpLoc = new Vector3(playerHpContainer.transform.position.x + (i * hpObjGap), playerHpContainer.transform.position.y, playerHpContainer.transform.position.z);
            //Debug.Log($"Player hp cube {i+1} : {hpLoc}");
            Instantiate(hpObj, hpLoc, playerHpContainer.transform.rotation);
        }

        for (int i = 0; i < rika.hp; i++)
        {
            Vector3 hpLoc = new Vector3(rikaHpContainer.transform.position.x - (i * hpObjGap), rikaHpContainer.transform.position.y, rikaHpContainer.transform.position.z);
            //Debug.Log($"Rika hp cube {i + 1} : {hpLoc}");
            Instantiate(hpObj, hpLoc, rikaHpContainer.transform.rotation);
        }

        prevPlayerHp = player.hp;
        prevRikaHp = rika.hp;
    }

    public void destroyAll()
    {
        GameObject[] prevHpObj= GameObject.FindGameObjectsWithTag("HpObject");
        for(int i = 0; i < prevHpObj.Length; i++)
        {
            Destroy(prevHpObj[i]);
        }
    }
}
