using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SOUND
{
    CLICK = 0,
    REWARD,
    SLIDE,
    SLOT,
    ANSWER,
    WRONGANSWER,
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource>  soundList;   

    public void PlaySound( SOUND sound )
    {
        soundList[(int)sound].Play();
    }    
}
