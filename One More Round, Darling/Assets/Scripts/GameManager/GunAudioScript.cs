using Mono.Cecil.Cil;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hasBullet;
    public AudioClip noBullet;

    public void playHasBullet()
    {
        Debug.Log("Play Has Bullet");
        if(audioSource != null && hasBullet != null)
        {
            audioSource.PlayOneShot(hasBullet, 1f);
        }
    }
    public void playNoBullet()
    {
        Debug.Log("Play Has no Bullet");
        if(audioSource != null && noBullet != null)
        {
            audioSource.PlayOneShot(noBullet, 1f);
        }
    }
}
