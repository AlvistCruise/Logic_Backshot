using UnityEngine;

public class GuessingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CoreLoop coreLoop;
    public Animator[] arrows;
    // public Image IMGRandomArrow;
    
    void Start()
    {
        coreLoop = GetComponent<CoreLoop>();

    }

    public bool guess(PlayerScript player, RikaScript rika)
    {
        //ganguit != draw -> lanjut (menang = arah tangan | kalah = arah kepala
        if (player.winRPS && !rika.winRPS)
        {
            //shootMenu.GetComponent<Canvas>().enabled = true;
            //Debug.Log("[DEBUG] Player menang gangsuit. Player yang menentukan arah tangan!");
            return PlayerDecide(player, rika);
            // resetAnimation();
        }
        else if (!player.winRPS && rika.winRPS)
        {
            //Debug.Log("[DEBUG] Rika menang gangsuit. Rika yang menentukan arah tangan!");
            //shootMenu.GetComponent<Canvas>().enabled = false;
            return RikaDecide(player, rika);
        }
        return false;
    }

    private bool PlayerDecide(PlayerScript player, RikaScript rika)
    {

        //player dulu nunjuk baru si Rika gerak kepala

        rika.decideHeadDirection();
        if (player.handDirection == 0 || rika.headDirection == 0) return false;
        // StopAnimAndGetArrow();

        Debug.Log($"[DEBUG] Player Hand: {player.handDirection} | Rika Head: {rika.headDirection}");
        if (player.handDirection == rika.headDirection)
        {
            Debug.Log("[DEBUG] Same direction! continue to shoot");
            player.attacker = true;
            rika.attacker = false;
            resetDirection(player, rika);
            return true;
        } else
        {
            Debug.Log("[DEBUG] Different direction! back to gangsuit");
            coreLoop.stage = 1;
            coreLoop.drawCanvas = true;
            coreLoop.LookGuessWorld.GetComponent<Canvas>().enabled = false;
            resetDirection(player, rika);
            return false;
        }

    }

    private bool RikaDecide(PlayerScript player, RikaScript rika)
    {
        rika.decideHandDirection();
        if (player.headDirection == 0 || rika.handDirection == 0) return false;
        // StopAnimAndGetArrow();

        Debug.Log($"[DEBUG] Player Hand: {player.headDirection} | Rika Head: {rika.handDirection}");
        if (rika.handDirection == player.headDirection)
        {
            Debug.Log("[DEBUG] Same direction! continue to shoot");
            player.attacker = false;
            rika.attacker = true;
            rika.headDecided = rika.handDecided = false;
            resetDirection(player, rika);
            return true;
        }
        else
        {
            Debug.Log("[DEBUG] Different direction! back to gangsuit");
            coreLoop.stage = 1;
            coreLoop.drawCanvas = true;
            coreLoop.LookGuessWorld.GetComponent<Canvas>().enabled = false;
            resetDirection(player, rika);
            return false;
        }

    }

    private void resetDirection(PlayerScript player, RikaScript rika)
    {
        player.handDirection = rika.handDirection = 0;
        player.headDirection = rika.headDirection = 0;
        rika.headDecided = rika.handDecided = false;
    }

    public void StopAnimAndGetArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].enabled = false;
            // tranform quaternion 0,0, (nilai dari rika decide) 1 == up 0 | 2 == right -90 | 3 == down -180 | 4 == left -270
        }
    }
}
