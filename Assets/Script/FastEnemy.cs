using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FastEnemy : Enemy
{
    float fastSpeed;
    GameObject treasure;
    Vector2 startPos;
    public Animator animator;
    //speed = 2*reuglar enemy speed
    public override void Start()
    {
        Debug.Log("Here");
        fastSpeed = speed * 2f;
        treasure = GameObject.FindGameObjectWithTag("Hoard");
        HoldingScrap = false; 
        alive = true;
        startPos = transform.position;
    }
  
    void MoveTowardsTreasure()
    {
        transform.position += (treasure.transform.position - transform.position).normalized * fastSpeed * Time.deltaTime;
    }
    //   void FollowLane()
    // {
    //     if (!alive || !Lane) return;

    //     Vector2 currentPos = transform.position;
    //     Vector2 closestPosOnLane = Lane.GetWorldPoint(laneCounter);//Lane.edgeCollider.ClosestPoint(currentPos);
    //     if (Vector2.Distance(currentPos, closestPosOnLane) < MaxLaneDistance)
    //     {
    //         laneCounter++;
    //         if (laneCounter >= Lane.points.Count)
    //         {
    //             alive = false;
    //             return;
    //         }
    //     }
    //     // transform.position = ( closestPosOnLane - currentPos).normalized * speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(transform.position, closestPosOnLane, speed * Time.deltaTime);
    //     // if (Dragon.MainInstance)
    //     //     transform.position = (currentPos - (Vector2)Dragon.MainInstance.transform.position).normalized * speed * Time.deltaTime;
    // }


    public override void MoveTowardsDragon(){
            MoveTowardsTreasure();
    }
    public override void MoveTowardsExit(){
            transform.position += ((Vector3)startPos - transform.position).normalized * speed * Time.deltaTime;

            if(transform.position.x >= 35f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hoard"))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            holdingScrap = true;
            Destroy(collision.gameObject);
        }
    }
}
