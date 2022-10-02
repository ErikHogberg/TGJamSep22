using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LaneKeepingEnemy : Enemy
{
    // public SpriteShapeController Lane;
    public LanePoints Lane;
    int laneCounter = 0;

    public float MaxLaneDistance = .1f;

    void FollowLane()
    {
        if (!alive || !Lane) return;

        Vector2 currentPos = transform.position;
        Vector2 closestPosOnLane = Lane.GetWorldPoint(laneCounter);//Lane.edgeCollider.ClosestPoint(currentPos);
        if (Vector2.Distance(currentPos, closestPosOnLane) < MaxLaneDistance)
        {
            laneCounter++;
            if (laneCounter >= Lane.points.Count)
            {
                alive = false;
                return;
            }
        }
        // transform.position = ( closestPosOnLane - currentPos).normalized * speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, closestPosOnLane, speed * Time.deltaTime);
        // if (Dragon.MainInstance)
        //     transform.position = (currentPos - (Vector2)Dragon.MainInstance.transform.position).normalized * speed * Time.deltaTime;
    }

    public override void MoveTowardsDragon()
    {
        FollowLane();
        // transform.position += (dragon.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }

    public override void MoveTowardsExit()
    {
        FollowLane();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.CompareTag("Hoard"))
        {
            //holdingScrap = true;
            Debug.Log("Yes");
            Destroy(collision.gameObject);
        }
    }
}
