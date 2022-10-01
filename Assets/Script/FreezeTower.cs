using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTower : MonoBehaviour
{
    public float freezeTime = 2f;
    float currFreezeTime;
    public bool isFreezing = false;
    public float freezedSpeed = 0f;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        float enemySpeed = 2f;
    //        Debug.Log("Starting freeze");
    //        StartCoroutine(StartCountdown(freezeTime, enemySpeed));

    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject targetHit = collision.gameObject;
        if (targetHit.tag == "Enemy" || !isFreezing)
        {

            print(targetHit.name + " has entered freeze zone");
            float enemySpeed = targetHit.GetComponent<Enemy>().speed;

            StartCoroutine(StartCountdown(freezeTime, enemySpeed));



        }
    }

    public IEnumerator StartCountdown(float freezeTime, float enemySpeed)
    {
        isFreezing = true;

        float origSpeed = enemySpeed;
        enemySpeed = freezedSpeed;
        Debug.Log("Current speed: " + enemySpeed);

        currFreezeTime = freezeTime;
        while (currFreezeTime > 0)
        {
            Debug.Log("Countdown: " + currFreezeTime);
            Debug.Log(isFreezing);
            yield return new WaitForSeconds(1.0f);
            currFreezeTime--;
        }
        isFreezing = false;

        Debug.Log("Ending Freeze");

        enemySpeed = origSpeed;
        Debug.Log("Current speed: " + enemySpeed);

    }

}
