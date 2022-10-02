using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyEnemy : Enemy
{
    List<Vector2> points;
    int targetPoint = 0;

    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            points.Add(new Vector2(Random.Range(-25f, 25f), Random.Range(-13f, 13f)));
        }

        //points.Add(dragon.transform.position);
    }

    public override void MoveTowardsDragon()
    {
        transform.position += ((Vector3)(points[targetPoint]) - transform.position).normalized * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, points[targetPoint]) < 0.5f)
        {
            targetPoint++;
        }
    }

    public override void MoveTowardsExit()
    {
        // transform.position += new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f)).normalized * speed * Time.deltaTime;
    }
}
