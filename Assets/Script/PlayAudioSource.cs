using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    public static PlayAudioSource mainInstance = null;

    //Music jingles
    public AudioSource victoryMusic;
    public AudioSource deafeatMusic;

    //SFX
    public AudioSource dragonBite;
    public AudioSource dragonDies;
    public AudioSource dragonFire;
    public AudioSource dragonFireExplode;
    public AudioSource enemyDeath;
    public AudioSource pickupScrap;
    public AudioSource menuPause;
    public AudioSource towerElectricity;

    public void Awake(){
        mainInstance = this;

    }

    public void OnDestroy(){
        mainInstance = null;

    }


}
