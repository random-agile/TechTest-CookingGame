using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip cut, finish, grabknife;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Awake()
    {
        cut = Resources.Load<AudioClip>("Cut");
        finish = Resources.Load<AudioClip>("Finish");
        grabknife = Resources.Load<AudioClip>("GrabKnife");
        audioSrc = GetComponent<AudioSource>();
    }       

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Cut":
                audioSrc.PlayOneShot(cut, 0.75f);
                break;
            case "Finish":
                audioSrc.PlayOneShot(finish, 0.5f);
                break;
            case "GrabKnife":
                audioSrc.PlayOneShot(grabknife, 1f);
                break;
        }
    }

    public static void StopSound()
    {
        audioSrc.Stop();
    }
}
