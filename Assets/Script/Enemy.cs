using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float hp;
    public float currentHp;
    public float speed;
    bool holdingScrap;
    bool alive;
    int scrapHoldingCap;
    int reward;
    // Start is called before the first frame update
    void Start()
    {
        this.hp = this.currentHp;
        this.holdingScrap = false;
        if (this.currentHp > 0)
            this.alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.alive == true)
        {
            if (this.currentHp <= 0)
            {
                this.alive = false;
                Destroy(this.gameObject);
            }
            if (holdingScrap == false)
            {
                //movetowards dragon
            }
           /* if (transform.position == dragon.position && this.holdingScrap == false)
            {
                this.holdingScrap = true;
                this.reward += this.scrapHoldingCap;
            } */
            if (this.holdingScrap == true)
            {
                // movetowards exit
            }
        }
       
    }
}
