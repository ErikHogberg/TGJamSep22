using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    public static PlayAudioSource mainInstance = null;

    public AudioSource dragonBite;
    public AudioSource menuPause;
    //public AudioSource myAudioSource;
    //public AudioSource myAudioSource;
    //public AudioSource myAudioSource;
    public float volume = 1.0f;

    public void Awake(){
        mainInstance = this;

    }

    public void OnDestroy(){
        mainInstance = null;

    }


}
