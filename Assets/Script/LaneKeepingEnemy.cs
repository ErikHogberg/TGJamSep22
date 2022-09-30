using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LaneKeepingEnemy : Enemy
{
    public SpriteShapeController Lane;

    public float MaxLaneDistance = 10f;

    void FollowLane()
    {
        Vector2 currentPos = transform.position;
        Vector2 closestPosOnLane = Lane.edgeCollider.ClosestPoint(currentPos);
        if (Vector2.Distance(currentPos, closestPosOnLane) > MaxLaneDistance)
        {
            // strayed too far, move towards lane road
            transform.position = (currentPos - closestPosOnLane).normalized * speed * Time.deltaTime;
        }
        else
        {
            // close enough to lane road, move towards dragon
            // TODO: move along lane instead of going directly towards dragon
            if (Dragon.MainInstance)
                transform.position = (currentPos - (Vector2)Dragon.MainInstance.transform.position).normalized * speed * Time.deltaTime;

        }
    }

    public override void MoveTowardsDragon()
    {
        FollowLane();
    }

    public override void MoveTowardsExit()
    {
        FollowLane();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!holdingScrap && other.CompareTag("Hoard"))
        {
            holdingScrap = true;
        }
    }
}
