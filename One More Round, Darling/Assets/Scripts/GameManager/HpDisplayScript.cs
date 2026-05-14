using UnityEngine;

public class HpDisplayScript : MonoBehaviour
{

    public PlayerScript player;
    public RikaScript rika;

    public GameObject playerHpContainer;
    public GameObject rikaHpContainer;

    public GameObject hpObj;

    private int prevRikaHp = 0;
    private int prevPlayerHp = 0;

    void Start()
    {
        player = GetComponent<PlayerScript>();
        rika = GetComponent<RikaScript>();
    }
    // Update is called once per frame
    public void updateHp()
    {
        if(prevPlayerHp != player.hp || prevRikaHp != rika.hp)
        {
            hpDisplayUpdate();
        }
    }

    void hpDisplayUpdate() {

        float hpObjGap = 0.4f;
        //destroy all prev hp obj
        GameObject[] prevHpObj= GameObject.FindGameObjectsWithTag("HpObject");
        for(int i = 0; i < prevHpObj.Length; i++)
        {
            Destroy(prevHpObj[i]);
        }


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
}
