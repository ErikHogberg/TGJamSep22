using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FastEnemy : Enemy
{
    float fastSpeed;
    //speed = 2*reuglar enemy speed
    public override void Start()
    {
        fastSpeed = speed * 5f;
    }

    public override void MoveTowardsDragon()
    {
        // transform.position += (dragon.transform.position - transform.position).normalized * fastSpeed * Time.deltaTime; 
        
    }
    public override void MoveTowardsExit()
    {
        // transform.position += new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f)).normalized * fastSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hoard"))
        {
            holdingScrap = true;
            fastSpeed = speed;
        }
    }
}
