using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public void shoot(PlayerScript player, RikaScript rika)
    {
        Debug.Log("Start shoot session");
        //TODO: ganti kondisi ke nebak, bukan dari gangsuit

        if(player.winRPS && !rika.winRPS)
        {
            
        } else if (!player.winRPS && rika.winRPS)
        {

        }
    }
}
