using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Abstract class, cannot be added to game objects as component
public abstract class Enemy : MonoBehaviour
{

    [HideInInspector]
    public float hp;
    public float currentHp;
    public float speed;
    [Space]
    public Animator animator;    
    
    protected bool holdingScrap;
    protected bool HoldingScrap {
        get{ return holdingScrap; }
        set{
            holdingScrap = value;
            if(animator)
                animator.SetBool("holdingScrap", true);
        }
    }

    protected bool alive;
    protected int scrapHoldingCap;
    protected int reward;

    public virtual void Start()
    {
        this.hp = this.currentHp;
        this.HoldingScrap = false;
        if (this.currentHp > 0)
            this.alive = true;
    }

    public virtual void Update()
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
                MoveTowardsDragon();
            }
           /* if (transform.position == dragon.position && this.holdingScrap == false)
            {
                this.holdingScrap = true;
                this.reward += this.scrapHoldingCap;
            } */
            if (this.holdingScrap == true)
            {
                // movetowards exit
                MoveTowardsExit();
            }
        }
       
        if(Input.GetKeyDown("w")){
            HoldingScrap = !HoldingScrap;
            Debug.Log($"holding scrap: {holdingScrap}");
        }

    }

    public abstract void MoveTowardsDragon();
    public abstract void MoveTowardsExit();
}
