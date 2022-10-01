using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FastEnemy : Enemy
{
    float fastSpeed;
    //speed = 2*reuglar enemy speed
    public override void Start()
    {
        fastSpeed = speed;
    }
    public override void MoveTowardsDragon(){}
    public override void MoveTowardsExit(){}
}
