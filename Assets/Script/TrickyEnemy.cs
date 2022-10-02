using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyEnemy : Enemy
{
    List<Vector2> points;
    int targetPoint = 0;

    private void Start()
    {
        points = new List<Vector2>();

        for(int i = 0; i < 1000; i++)
        {
            points.Add(new Vector2(Random.Range(-25f, 25f), Random.Range(-13f, 13f)));
        }

        alive = true;
        Debug.Log(points[0]);

        //points.Add(dragon.transform.position);
    }

    public override void MoveTowardsDragon()
    {
        transform.position += ((Vector3)(points[targetPoint]) - transform.position).normalized * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, points[targetPoint]) < 0.5f)
        {
            targetPoint++;
            Debug.Log(points[targetPoint]);

            if(targetPoint == points.Count)
            {
                holdingScrap = true;
            }
        }
    }

    public override void MoveTowardsExit()
    {
        // transform.position += new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f)).normalized * speed * Time.deltaTime;
    }
}
