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
    protected bool HoldingScrap
    {
        get { return holdingScrap; }
        set
        {
            holdingScrap = value;
            if (animator)
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
            
            // Get mouse position if  enemy gets hit
            if (Input.GetMouseButtonDown(0))
            {
                //Get the mouse position on the screen and send a raycast into the game world from that position.
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                //If something was hit, the RaycastHit2D.collider will not be null.
                if (hit.collider != null)
                {
                    alivecheck();
                    Debug.Log(hit.collider.name);
                    ResourceManager.MainInstance.Score += 10;
                }
            }


            if (holdingScrap == false)
            {
                MoveTowardsDragon();
            }
            if (transform.position == Dragon.MainInstance.transform.position && this.holdingScrap == false)
            {
                this.holdingScrap = true;
                this.reward += this.scrapHoldingCap;
            }
            if (this.holdingScrap == true)
            {
                MoveTowardsExit();
            }
        }

        if (Input.GetKeyDown("w"))
        {
            HoldingScrap = !HoldingScrap;
            Debug.Log($"holding scrap: {holdingScrap}");
        }
       if(!this.alive)
        {
            Dragon.MainInstance.scrap += this.reward;
            Destroy(gameObject);
        }
    }
    public virtual void alivecheck()
    {
        this.currentHp -= Dragon.MainInstance.spellDamage;
        Debug.Log("Enemy health reduced");
        Debug.Log($"Current health: {this.currentHp}");
        if (this.currentHp <= 0 && this.alive == true)
        {
            this.alive = false;
        }
    }

    public abstract void MoveTowardsDragon();
    public abstract void MoveTowardsExit();
}
